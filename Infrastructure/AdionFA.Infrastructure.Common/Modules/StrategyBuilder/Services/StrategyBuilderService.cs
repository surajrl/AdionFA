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
            IEnumerable<Candle> candles)
        {
            try
            {
                var bkmIS = BacktestExecute(
                    nodeLabel,
                    config.FromDateIS.Value,
                    config.ToDateIS.Value,
                    node.ToList(),
                    candles,
                    config.TimeframeId);

                var bkmOS = BacktestExecute(
                    nodeLabel,
                    config.FromDateOS.Value,
                    config.ToDateOS.Value,
                    node.ToList(),
                    candles,
                    config.TimeframeId);

                //BacktestSerializeOs(bkmOS);

                var stb = new StrategyBuilderModel
                {
                    IS = bkmIS,
                    OS = bkmOS
                };

                stb.WinningStrategy = ApplyWinningStrategyRules(
                    bkmIS,
                    bkmOS,
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

        private void BacktestSerializeOs(BacktestModel backtest)
        {
            SerializerHelper.XMLSerializeObject(
                backtest,
                Path.Combine(ProjectDirectoryManager.DefaultDirectory(), RegexHelper.GetValidFileName(backtest.Name, "_")));
        }

        public BacktestModel BacktestExecute(
            string nodeLabel,
            DateTime fromDate,
            DateTime toDate,
            List<string> node,
            IEnumerable<Candle> candles,
            int timeframeId)
        {
            try
            {
                var bk = new BacktestModel
                {
                    Label = nodeLabel,
                    FromDate = fromDate,
                    ToDate = toDate,
                    PeriodId = timeframeId,
                    Node = node,

                    Backtests = new List<BacktestOperationModel>()
                };

                List<IndicatorBase> rules = ExtractorService.BuildIndicatorsFromNode(node);

                if (rules.Any())
                {
                    // Get the candles between the from and to date
                    List<Candle> candlesRange = (from c in candles
                                                 let dt = DateTimeHelper.BuildDateTime(timeframeId, c.Date, c.Time)
                                                 where dt >= fromDate && dt <= toDate
                                                 select c).ToList();

                    bk.TotalOpportunity = candlesRange.Count;

                    candlesRange.ForEach(candleToTest =>
                    {
                        // Select the candle range from the first one until the candle being tested
                        var fromDt = DateTimeHelper.BuildDateTime(timeframeId, candlesRange.FirstOrDefault().Date, candlesRange.FirstOrDefault().Time);
                        var toDt = DateTimeHelper.BuildDateTime(timeframeId, candleToTest.Date, candleToTest.Time);

                        IEnumerable<Candle> range = from c in candlesRange
                                                    let dt = DateTimeHelper.BuildDateTime(timeframeId, c.Date, c.Time)
                                                    where dt >= fromDt && dt <= toDt
                                                    select c;

                        List<IndicatorBase> extractorResult = ExtractorService.ExtractorBacktest(
                            candleToTest,
                            rules,
                            range,
                            timeframeId);

                        if (extractorResult.Any())
                        {
                            var totalRules = extractorResult.Count;
                            var temporalIndicator = extractorResult.FirstOrDefault();
                            var length = temporalIndicator.Output.Length;

                            int counter = 0;
                            while (counter < length)
                            {
                                var il = temporalIndicator.IntervalLabels[counter];
                                var upOrDown = il.Label;

                                var passed = 0;

                                foreach (var indicator in rules)
                                {
                                    var output = indicator.Output[counter];

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

                                // Rules from the node have been satisfied.
                                if (passed == totalRules)
                                {
                                    bk.TotalTrades++;

                                    // Check if the candle went UP or DOWN
                                    bool isWinner = bk.Label.ToLower() == upOrDown.ToLower();
                                    if (isWinner)
                                    {
                                        bk.WinningTrades++;
                                    }
                                    else
                                    {
                                        bk.LosingTrades++;
                                    }

                                    bk.Backtests.Add(new BacktestOperationModel
                                    {
                                        Date = il.Interval,
                                        IsWinner = isWinner
                                    });
                                }

                                counter++;
                            }
                        }
                    });
                }

                return bk;
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

        public void BacktestSerialize(string projectName, BacktestModel model)
        {
            // IS
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
