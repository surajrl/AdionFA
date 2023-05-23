using AdionFA.Infrastructure.Common.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using System.Diagnostics;
using AdionFA.Infrastructure.Common.Managements;
using System.Threading;
using AdionFA.Infrastructure.Common.Weka.Model;
using System.Timers;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Services
{
    public class StrategyBuilderService : IStrategyBuilderService
    {
        private readonly IProjectDirectoryService ProjectDirectoryService;
        private readonly IExtractorService ExtractorService;

        public StrategyBuilderService()
        {
            ProjectDirectoryService = IoC.Get<IProjectDirectoryService>();
            ExtractorService = IoC.Get<IExtractorService>();
        }

        // Strategy

        public CorrelationModel Correlation(string projectName, decimal correlation, EntityTypeEnum entityType)
        {
            try
            {
                var correlationDetail = new CorrelationModel();

                // Output Directory

                var directoryUP = projectName.ProjectStrategyBuilderNodesUPDirectory();
                var directoryDOWN = projectName.ProjectStrategyBuilderNodesDOWNDirectory();

                switch (entityType)
                {
                    case EntityTypeEnum.AssembledBuilder:
                        directoryUP = projectName.ProjectAssembledBuilderNodesUPDirectory();
                        directoryDOWN = projectName.ProjectAssembledBuilderNodesDOWNDirectory();
                        break;
                }

                // UP IS Nodes

                ProjectDirectoryService.GetFilesInPath(directoryUP, "*.xml").ToList().ForEach(file =>
                {
                    if (file.Name.Contains("BACKTEST"))
                    {
                        var backtestIS = DeserializeBacktest(file.FullName);
                        var indexOf = IndexOfCorrelation(correlationDetail.ISBacktestUP, backtestIS, correlation);

                        backtestIS.CorrelationPass = indexOf != null;

                        if (backtestIS.CorrelationPass)
                        {
                            if ((indexOf ?? -1) >= 0)
                            {
                                correlationDetail.ISBacktestUP.Insert(indexOf.Value, backtestIS);
                            }
                            if ((indexOf ?? 0) == -1)
                            {
                                correlationDetail.ISBacktestUP.Add(backtestIS);
                            }
                        }
                        else
                        {
                            ProjectDirectoryService.DeleteFile(file.FullName);                              // Delete the file with the backtest
                            ProjectDirectoryService.DeleteFile(file.FullName.Replace("BACKTEST", "NODE"));  // Delete the file with the node
                        }
                    }
                });

                // DOWN IS Nodes

                ProjectDirectoryService.GetFilesInPath(directoryDOWN, "*.xml").ToList().ForEach(file =>
                {
                    if (file.Name.Contains("BACKTEST"))
                    {
                        var backtestIS = DeserializeBacktest(file.FullName);
                        var indexOf = IndexOfCorrelation(correlationDetail.ISBacktestDOWN, backtestIS, correlation);

                        backtestIS.CorrelationPass = indexOf != null;

                        if (backtestIS.CorrelationPass)
                        {
                            if ((indexOf ?? -1) >= 0)
                            {
                                correlationDetail.ISBacktestDOWN.Insert(indexOf.Value, backtestIS);
                            }
                            if ((indexOf ?? 0) == -1)
                            {
                                correlationDetail.ISBacktestDOWN.Add(backtestIS);
                            }
                        }
                        else
                        {
                            ProjectDirectoryService.DeleteFile(file.FullName);                              // Delete the file with the backtest
                            ProjectDirectoryService.DeleteFile(file.FullName.Replace("BACKTEST", "NODE"));  // Delete the file with the node
                        }
                    }
                });

                return correlationDetail;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<IStrategyBuilderService>(ex);

                throw;
            }
        }

        private static int? IndexOfCorrelation(IList<BacktestModel> backtests, BacktestModel btModel, decimal correlation)
        {
            int? indexOf = !backtests.Any() ? -1 : null;

            foreach (var backtest in backtests)
            {
                var coincidences =
                    backtest.Backtests
                    .Where(operation => btModel.Backtests.Any(_bto => _bto.Date == operation.Date))
                    .ToList();

                var totalCoincidences = coincidences.Count;
                var btItemCount = backtest.Backtests.Count;
                var btModelCount = btModel.Backtests.Count;

                var percentBtItem = totalCoincidences * 100 / btItemCount;
                var percentBtModel = totalCoincidences * 100 / btModelCount;

                if (totalCoincidences == 0 && btModelCount > btItemCount)
                {
                    return backtests.IndexOf(backtest);
                }

                if ((percentBtItem + percentBtModel) / 2 <= correlation)
                {
                    indexOf = percentBtItem > percentBtModel ? backtests.IndexOf(backtest) : -1;
                }

                if (indexOf > -1)
                {
                    break;
                }
            }

            return indexOf;
        }

        // Backtest

        public StrategyBuilderModel BacktestBuild(
            string nodeLabel,
            List<string> node,
            ConfigurationBaseDTO config,
            IEnumerable<Candle> allCandles,
            CancellationToken cancellationToken,
            ManualResetEventSlim manualResetEvent)
        {
            try
            {
                var backtestIS = ExecuteBacktest(
                    nodeLabel,
                    config.FromDateIS.Value,
                    config.ToDateIS.Value,
                    node,
                    allCandles,
                    config.TimeframeId,
                    cancellationToken,
                    manualResetEvent);

                var backtestOS = ExecuteBacktest(
                    nodeLabel,
                    config.FromDateOS.Value,
                    config.ToDateOS.Value,
                    node,
                    allCandles,
                    config.TimeframeId,
                    cancellationToken,
                    manualResetEvent);

                var stb = new StrategyBuilderModel
                {
                    IS = backtestIS,
                    OS = backtestOS
                };

                stb.WinningStrategy = ApplyWinningStrategyRules(
                    backtestIS,
                    backtestOS,
                    config,
                    stb.VariationPercent,
                    stb.Progressiveness);

                return stb;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderService>(ex);
                throw;
            }
        }

        public BacktestModel ExecuteBacktest(
            string nodeLabel,
            DateTime fromDate,
            DateTime toDate,
            List<string> node,
            IEnumerable<Candle> allCandles,
            int timeframeId,
            CancellationToken cancellationToken,
            ManualResetEventSlim manualResetEvent)
        {
            try
            {
                var backtest = new BacktestModel
                {
                    Label = nodeLabel,
                    FromDate = fromDate,
                    ToDate = toDate,
                    PeriodId = timeframeId,
                    Node = node.ToList(),

                    Backtests = new List<BacktestOperationModel>()
                };

                var indicators = ExtractorService.BuildIndicatorsFromNode(node);

                if (indicators.Any())
                {
                    var toListStopwatch = new Stopwatch();
                    toListStopwatch.Start();

                    var candlesFromTo = (from c in allCandles
                                         let dt = DateTimeHelper.BuildDateTime(timeframeId, c.Date, c.Time)
                                         where dt >= fromDate && dt <= toDate
                                         select c)
                                         .ToList();

                    toListStopwatch.Stop();
                    Debug.WriteLine($"{toListStopwatch.Elapsed:mm\\:ss\\.ffffff}");

                    backtest.TotalOpportunity = candlesFromTo.Count;

                    var candleHistory = new List<Candle>();

                    for (var candleIdx = 0; candleIdx < candlesFromTo.Count - 1; candleIdx++)
                    {
                        manualResetEvent.Wait();
                        cancellationToken.ThrowIfCancellationRequested();

                        var firstCandle = candlesFromTo[0];
                        var currentCandle = new Candle
                        {
                            Date = candlesFromTo[candleIdx].Date,
                            Time = candlesFromTo[candleIdx].Time,

                            Open = candlesFromTo[candleIdx].Open,
                            High = candlesFromTo[candleIdx].Open,
                            Low = candlesFromTo[candleIdx].Open,
                            Close = candlesFromTo[candleIdx].Open,

                            Spread = candlesFromTo[candleIdx].Spread
                        };
                        var nextCandle = candlesFromTo[candleIdx + 1];

                        var firstCandleDt = DateTimeHelper.BuildDateTime(timeframeId, firstCandle.Date, firstCandle.Time);
                        var currentCandleDt = DateTimeHelper.BuildDateTime(timeframeId, currentCandle.Date, currentCandle.Time);

                        var extractorResult = new List<IndicatorBase>();

                        if (candleIdx == 0)
                        {
                            candleHistory.Add(currentCandle);

                            extractorResult = ExtractorService.DoBacktest(
                                currentCandle,
                                currentCandle,
                                indicators,
                                candleHistory);
                        }
                        else
                        {
                            candleHistory = (from c in candlesFromTo
                                             let dt = DateTimeHelper.BuildDateTime(timeframeId, c.Date, c.Time)
                                             where dt >= firstCandleDt && dt < currentCandleDt
                                             select c).ToList();

                            candleHistory.Add(currentCandle);

                            extractorResult = ExtractorService.DoBacktest(
                                firstCandle,
                                currentCandle,
                                indicators,
                                candleHistory);
                        }

                        if (!extractorResult.Any())
                        {
                            continue;
                        }

                        var passed = 0;

                        foreach (var indicator in extractorResult)
                        {
                            if (indicator.OutNBElement == 0)
                            {
                                continue;
                            }

                            var output = indicator.Output[indicator.OutNBElement - 1];

                            switch (indicator.Operator)
                            {
                                case MathOperatorEnum.GreaterThanOrEqual:
                                    passed += output >= indicator.Value ? 1 : 0;
                                    break;

                                case MathOperatorEnum.LessThanOrEqual:
                                    passed += output <= indicator.Value ? 1 : 0;
                                    break;

                                case MathOperatorEnum.GreaterThan:
                                    passed += output > indicator.Value ? 1 : 0;
                                    break;

                                case MathOperatorEnum.LessThan:
                                    passed += output < indicator.Value ? 1 : 0;
                                    break;

                                case MathOperatorEnum.Equal:
                                    passed += output == indicator.Value ? 1 : 0;
                                    break;
                            }
                        }

                        if (passed == extractorResult.ToList().Count)
                        {
                            backtest.TotalTrades++;

                            var isWinnerTrade = false;
                            var spread = currentCandle.Spread * 0.00001;
                            if (backtest.Label.ToLower() == "up")
                            {
                                isWinnerTrade = (currentCandle.Open + spread) < nextCandle.Open;
                            }
                            else
                            {
                                isWinnerTrade = currentCandle.Open > (nextCandle.Open + spread);
                            }

                            if (isWinnerTrade)
                            {
                                backtest.WinningTrades++;
                            }
                            else
                            {
                                backtest.LosingTrades++;
                            }

                            backtest.Backtests.Add(new BacktestOperationModel
                            {
                                Date = DateTimeHelper.BuildDateTime(timeframeId, currentCandle.Date, currentCandle.Time),
                                IsWinner = isWinnerTrade
                            });
                        }
                    }
                }

                return backtest;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<Exception>(ex);
                throw;
            }
        }

        private static bool ApplyWinningStrategyRules(
            BacktestModel bkmIS,
            BacktestModel bkmOS,
            ConfigurationBaseDTO config,
            double variationPercent,
            double progressiveness)
        {
            return bkmIS.WinningTrades >= config.MinTransactionCountIS &&
                    bkmIS.PercentSuccess >= (double)config.MinPercentSuccessIS &&
                    bkmOS.WinningTrades >= config.MinTransactionCountOS &&
                    bkmOS.PercentSuccess >= (double)config.MinPercentSuccessOS &&
                    variationPercent <= (double)config.VariationTransaction &&

                    (!config.IsProgressiveness || progressiveness <= (double)config.Progressiveness);
        }

        // Backtest Serialization

        public static void SerializeBacktest(string projectName, BacktestModel backtest)
        {
            var directory = backtest.Label.ToLower() == "up"
            ? projectName.ProjectStrategyBuilderNodesUPDirectory()
            : projectName.ProjectStrategyBuilderNodesDOWNDirectory();

            var filename = $"BACKTEST-{RegexHelper.GetValidFileName(backtest.NodeName(), "_")}.xml";

            SerializerHelper.XMLSerializeObject(backtest, string.Format(@"{0}\{1}", directory, filename));
        }

        public static void SerializeNode(string projectName, string nodeName, REPTreeNodeModel node)
        {
            var directory = node.Label.ToLower() == "up"
            ? projectName.ProjectStrategyBuilderNodesUPDirectory()
            : projectName.ProjectStrategyBuilderNodesDOWNDirectory();

            var filename = $"NODE-{RegexHelper.GetValidFileName(nodeName, "_")}.xml";

            SerializerHelper.XMLSerializeObject(node, string.Format(@"{0}\{1}", directory, filename));
        }

        public static BacktestModel DeserializeBacktest(string path)
        {
            return SerializerHelper.XMLDeSerializeObject<BacktestModel>(path);
        }
    }
}