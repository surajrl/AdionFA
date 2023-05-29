using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Services
{
    public class StrategyBuilderService : IStrategyBuilderService
    {
        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IExtractorService _extractorService;

        public StrategyBuilderService()
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _extractorService = IoC.Get<IExtractorService>();
        }

        // Correlation

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

                _projectDirectoryService.GetFilesInPath(directoryUP, "*.xml").ToList().ForEach(file =>
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
                            _projectDirectoryService.DeleteFile(file.FullName);                              // Delete the file with the backtest
                            _projectDirectoryService.DeleteFile(file.FullName.Replace("BACKTEST", "NODE"));  // Delete the file with the node
                        }
                    }
                });

                // DOWN IS Nodes

                _projectDirectoryService.GetFilesInPath(directoryDOWN, "*.xml").ToList().ForEach(file =>
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
                            _projectDirectoryService.DeleteFile(file.FullName);                              // Delete the file with the backtest
                            _projectDirectoryService.DeleteFile(file.FullName.Replace("BACKTEST", "NODE"));  // Delete the file with the node
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
                    backtest.BacktestOperations
                    .Where(operation => btModel.BacktestOperations.Any(_bto => _bto.Date == operation.Date))
                    .ToList();

                var totalCoincidences = coincidences.Count;
                var btItemCount = backtest.BacktestOperations.Count;
                var btModelCount = btModel.BacktestOperations.Count;

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

        public StrategyBuilderModel BuildBacktest(
            string nodeLabel,
            List<string> node,
            ConfigurationDTO configuration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            try
            {
                var strategyBuilder = new StrategyBuilderModel();

                strategyBuilder.OS = ExecuteBacktest(
                    nodeLabel,
                    configuration.FromDateOS.Value,
                    configuration.ToDateOS.Value,
                    configuration.TimeframeId,
                    candles,
                    node,
                    manualResetEvent,
                    cancellationToken);

                if (!ApplyWinningStrategyRulesOS(strategyBuilder.OS, configuration))
                {
                    strategyBuilder.WinningStrategy = false;
                    return strategyBuilder;
                }

                strategyBuilder.IS = ExecuteBacktest(
                    nodeLabel,
                    configuration.FromDateIS.Value,
                    configuration.ToDateIS.Value,
                    configuration.TimeframeId,
                    candles,
                    node,
                    manualResetEvent,
                    cancellationToken);

                if (!ApplyWinningStrategyRulesIS(strategyBuilder.IS, configuration))
                {
                    strategyBuilder.WinningStrategy = false;
                    return strategyBuilder;
                }

                strategyBuilder.WinningStrategy =
                    strategyBuilder.VariationPercent <= (double)configuration.SBMaxTransactionsVariation
                    && (!configuration.IsProgressiveness || strategyBuilder.Progressiveness <= (double)configuration.Progressiveness);

                return strategyBuilder;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderService>(ex);
                throw;
            }
        }

        private BacktestModel ExecuteBacktest(
            string nodeLabel,
            DateTime fromDate,
            DateTime toDate,
            int timeframeId,
            IEnumerable<Candle> candles,
            List<string> node,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            try
            {
                var nodeIndicators = _extractorService.BuildIndicatorsFromNode(node);

                var candlesRange = (from candle in candles
                                    let dt = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time)
                                    where dt >= fromDate && dt <= toDate
                                    select candle).ToList();

                var backtest = new BacktestModel
                {
                    Label = nodeLabel,
                    FromDate = fromDate,
                    ToDate = toDate,
                    TimeframeId = timeframeId,
                    Node = node.ToList(),

                    BacktestOperations = new List<BacktestOperationModel>(),
                    TotalOpportunity = candlesRange.Count
                };

                for (var candleIdx = 0; candleIdx < candlesRange.Count - 1; candleIdx++)
                {
                    manualResetEvent.Wait();
                    cancellationToken.ThrowIfCancellationRequested();

                    var firstCandle = candlesRange[0];
                    var nextCandle = candlesRange[candleIdx + 1];
                    var currentCandle = new Candle
                    {
                        Date = candlesRange[candleIdx].Date,
                        Time = candlesRange[candleIdx].Time,

                        Open = candlesRange[candleIdx].Open,
                        High = candlesRange[candleIdx].Open,
                        Low = candlesRange[candleIdx].Open,
                        Close = candlesRange[candleIdx].Open,

                        Spread = candlesRange[candleIdx].Spread
                    };

                    if (ApproveCandle(nodeIndicators, candleIdx, firstCandle, currentCandle, candlesRange))
                    {
                        backtest.TotalTrades++;

                        var isWinnerTrade = false;
                        var spread = currentCandle.Spread * 0.00001;

                        isWinnerTrade = backtest.Label.ToLower() == "up"
                            ? (currentCandle.Open + spread) < nextCandle.Open
                            : currentCandle.Open > (nextCandle.Open + spread);

                        if (isWinnerTrade)
                        {
                            backtest.WinningTrades++;
                        }
                        else
                        {
                            backtest.LosingTrades++;
                        }

                        backtest.BacktestOperations.Add(new BacktestOperationModel
                        {
                            Date = DateTimeHelper.BuildDateTime(timeframeId, currentCandle.Date, currentCandle.Time),
                            IsWinner = isWinnerTrade
                        });
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

        private bool ApproveCandle(IList<IndicatorBase> nodeIndicators, int candleIdx, Candle firstCandle, Candle currentCandle, List<Candle> candlesRange)
        {
            var tempRemovedCandle = candlesRange[candleIdx];
            candlesRange.RemoveAt(candleIdx);
            candlesRange.Insert(candleIdx, currentCandle);

            var nodeIndicatorResults = _extractorService.CalculateNodeIndicators(
                firstCandle,
                currentCandle,
                nodeIndicators,
                candlesRange.GetRange(0, candleIdx + 1));

            candlesRange.RemoveAt(candleIdx);
            candlesRange.Insert(candleIdx, tempRemovedCandle);


            for (var i = 0; i < nodeIndicatorResults.Count; i++)
            {
                var indicator = nodeIndicatorResults[i];

                if (indicator.OutNBElement == 0)
                {
                    continue;
                }

                var output = indicator.Output[indicator.OutNBElement - 1];

                switch (indicator.Operator)
                {
                    case MathOperatorEnum.GreaterThanOrEqual:
                        if (output >= indicator.Value)
                        {
                            break;
                        }
                        return false;

                    case MathOperatorEnum.LessThanOrEqual:
                        if (output <= indicator.Value)
                        {
                            break;
                        }
                        return false;

                    case MathOperatorEnum.GreaterThan:
                        if (output > indicator.Value)
                        {
                            break;
                        }
                        return false;

                    case MathOperatorEnum.LessThan:
                        if (output < indicator.Value)
                        {
                            break;
                        }
                        return false;

                    case MathOperatorEnum.Equal:
                        if (output == indicator.Value)
                        {
                            break;
                        }
                        return false;
                }
            }

            return true;
        }

        private bool ApplyWinningStrategyRulesIS(BacktestModel backtestIS, ConfigurationDTO configuration)
        {
            return backtestIS.WinningTrades >= configuration.SBMinTransactionsIS
                && backtestIS.PercentSuccess >= (double)configuration.SBMinPercentSuccessIS;
        }

        private bool ApplyWinningStrategyRulesOS(BacktestModel backtestOS, ConfigurationDTO configuration)
        {
            return backtestOS.WinningTrades >= configuration.SBMinTransactionsOS
               && backtestOS.PercentSuccess >= (double)configuration.SBMinPercentSuccessOS;
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
