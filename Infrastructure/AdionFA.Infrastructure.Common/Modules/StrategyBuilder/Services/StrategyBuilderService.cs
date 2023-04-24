using AdionFA.Infrastructure.Common.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Directories.Services;
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
using AdionFA.Infrastructure.Common.Weka.Model;
using System.Reflection.Metadata;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Diagnostics;

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

        public IList<REPTreeNodeModel> GetBacktests()
        {
            var allBacktests = new List<BacktestModel>();
            var allNodes = new List<REPTreeNodeModel>();

            var directoryOS = Path.Combine(ProjectDirectoryManager.DefaultDirectory(), "OS");
            var directoryIS = Path.Combine(ProjectDirectoryManager.DefaultDirectory(), "IS");

            ProjectDirectoryService.GetFilesInPath(directoryOS, "*.xml").ToList().ForEach(file =>
            {
                allBacktests.Add(BacktestDeserialize(file.FullName));
            });

            ProjectDirectoryService.GetFilesInPath(directoryIS, "*.xml").ToList().ForEach(file =>
            {
                allBacktests.Add(BacktestDeserialize(file.FullName));
            });

            return allNodes;
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

                ProjectDirectoryService.GetFilesInPath(directoryUP, "*.xml")
                    .ToList().ForEach(file =>
                    {
                        var backtestIS = BacktestDeserialize(file.FullName);
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
                            ProjectDirectoryService.DeleteFile(file.FullName);
                        }
                    });

                // DOWN IS Nodes

                ProjectDirectoryService.GetFilesInPath(directoryDOWN, "*.xml")
                    .ToList().ForEach(file =>
                    {
                        var backtestIS = BacktestDeserialize(file.FullName);
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
                            ProjectDirectoryService.DeleteFile(file.FullName);
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

        private int? IndexOfCorrelation(IList<BacktestModel> backtests, BacktestModel btModel, decimal correlation)
        {
            int? indexOf = !backtests.Any() ? -1 : null;

            foreach (var btItem in backtests)
            {
                var coincidences =
                    btItem.Backtests
                    .Where(_btoItem => btModel.Backtests.Any(_bto => _bto.Date == _btoItem.Date))
                    .ToList();

                var totalCoincidences = coincidences.Count;
                var btItemCount = btItem.Backtests.Count;
                var btModelCount = btModel.Backtests.Count;

                var percentBtItem = totalCoincidences * 100 / btItemCount;
                var percentBtModel = totalCoincidences * 100 / btModelCount;

                if (totalCoincidences == 0 && btModelCount > btItemCount)
                {
                    return backtests.IndexOf(btItem);
                }

                if ((percentBtItem + percentBtModel) / 2 <= correlation)
                {
                    indexOf = percentBtItem > percentBtModel ? backtests.IndexOf(btItem) : -1;
                }

                if (indexOf > -1)
                    break;
            }

            return indexOf;
        }

        // Backtest

        public StrategyBuilderModel BacktestBuild(
            string nodeLabel,
            List<string> node,
            ConfigurationBaseDTO config,
            IEnumerable<Candle> allCandles)
        {
            try
            {
                var backtestIS = ExecuteBacktest(
                    nodeLabel,
                    config.FromDateIS.Value,
                    config.ToDateIS.Value,
                    node.ToList(),
                    allCandles,
                    config.TimeframeId);

                var backtestOS = ExecuteBacktest(
                    nodeLabel,
                    config.FromDateOS.Value,
                    config.ToDateOS.Value,
                    node.ToList(),
                    allCandles,
                    config.TimeframeId);

                BacktestSerializeOs(backtestOS);
                BacktestSerializeIs(backtestIS);

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
            int timeframeId)
        {
            try
            {
                var backtest = new BacktestModel
                {
                    Label = nodeLabel,
                    FromDate = fromDate,
                    ToDate = toDate,
                    PeriodId = timeframeId,
                    Node = node,

                    Backtests = new List<BacktestOperationModel>()
                };

                var indicators = ExtractorService.BuildIndicatorsFromNode(node);

                if (indicators.Any())
                {
                    var candlesFromTo = (from c in allCandles
                                         let dt = DateTimeHelper.BuildDateTime(timeframeId, c.Date, c.Time)
                                         where dt >= fromDate && dt <= toDate
                                         select c)
                                         .ToList();

                    backtest.TotalOpportunity = candlesFromTo.Count;

                    for (var idx = 0; idx < candlesFromTo.Count - 1; idx++)
                    {
                        var firstCandle = candlesFromTo[0];
                        var nextCandle = candlesFromTo[idx + 1];

                        var currentCandle = new Candle
                        {
                            Date = candlesFromTo[idx].Date,
                            Time = candlesFromTo[idx].Time,
                            Open = candlesFromTo[idx].Open,
                            High = candlesFromTo[idx].Open,
                            Low = candlesFromTo[idx].Open,
                            Close = candlesFromTo[idx].Open,
                        };

                        var firstCandleDt = DateTimeHelper.BuildDateTime(timeframeId, firstCandle.Date, firstCandle.Time);
                        var currentCandleDt = DateTimeHelper.BuildDateTime(timeframeId, currentCandle.Date, currentCandle.Time);

                        var candlesRange = (from c in candlesFromTo
                                            let dt = DateTimeHelper.BuildDateTime(timeframeId, c.Date, c.Time)
                                            where dt >= firstCandleDt && dt < currentCandleDt
                                            select c).ToList();

                        candlesRange.Add(currentCandle);

                        var extractorResult = ExtractorService.ExtractorBacktest(
                            firstCandle,
                            currentCandle,
                            indicators,
                            candlesRange);

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

                        if (passed == extractorResult.Count)
                        {
                            backtest.TotalTrades++;

                            bool isWinnerTrade = false;
                            if (backtest.Label.ToLower() == "up")
                            {
                                isWinnerTrade = currentCandle.Open < nextCandle.Open;
                            }
                            else
                            {
                                isWinnerTrade = currentCandle.Open > nextCandle.Open;
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

        private void BacktestSerializeOs(BacktestModel model)
        {
            SerializerHelper.XMLSerializeObject(
                model,
                string.Format(@"{0}\OS\{1}.xml", ProjectDirectoryManager.DefaultDirectory(), RegexHelper.GetValidFileName(model.NodeName(), "_")));
        }

        private void BacktestSerializeIs(BacktestModel model)
        {
            SerializerHelper.XMLSerializeObject(
                model,
                string.Format(@"{0}\IS\{1}.xml", ProjectDirectoryManager.DefaultDirectory(), RegexHelper.GetValidFileName(model.NodeName(), "_")));
        }

        public void BacktestSerialize(string projectName, BacktestModel model)
        {
            var directory = model.Label.ToLower() == "up"
                ? projectName.ProjectStrategyBuilderNodesUPDirectory()
                : projectName.ProjectStrategyBuilderNodesDOWNDirectory();

            SerializerHelper.XMLSerializeObject(
                model,
                string.Format(@"{0}\{1}.xml", directory, RegexHelper.GetValidFileName(model.NodeName(), "_")));
        }

        public BacktestModel BacktestDeserialize(string path)
        {
            return SerializerHelper.XMLDeSerializeObject<BacktestModel>(path);
        }
    }
}
