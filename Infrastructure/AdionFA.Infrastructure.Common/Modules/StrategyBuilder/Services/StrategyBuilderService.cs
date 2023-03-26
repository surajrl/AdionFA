using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Directories.Services;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Services
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
                CorrelationModel correlationDetail = new CorrelationModel();

                // Output Directory

                string directoryUP = projectName.ProjectStrategyBuilderNodesUPDirectory();
                string directoryDOWN = projectName.ProjectStrategyBuilderNodesDOWNDirectory();
                switch (entityType)
                {
                    case EntityTypeEnum.AssembledBuilder:
                        directoryUP = projectName.ProjectAssembledBuilderNodesUPDirectory();
                        directoryDOWN = projectName.ProjectAssembledBuilderNodesDOWNDirectory();
                        break;
                }

                // UP

                ProjectDirectoryService.GetFilesInPath(directoryUP, "*.xml")
                    .ToList().ForEach(fi =>
                    {
                        BacktestModel model = BacktestDeserialize(fi.FullName);
                        int? indexOf = IndexOfCorrelation(correlationDetail.BacktestUP, model, correlation);

                        model.CorrelationPass = indexOf != null;

                        if (model.CorrelationPass)
                        {
                            if ((indexOf ?? -1) >= 0)
                                correlationDetail.BacktestUP.Insert(indexOf.Value, model);
                            if ((indexOf ?? 0) == -1)
                                correlationDetail.BacktestUP.Add(model);
                        }
                        else
                        {
                            ProjectDirectoryService.DeleteFile(fi.FullName);
                        }
                    });

                // DOWN

                ProjectDirectoryService.GetFilesInPath(directoryDOWN, "*.xml")
                    .ToList().ForEach(fi =>
                    {
                        BacktestModel model = BacktestDeserialize(fi.FullName);
                        int? indexOf = IndexOfCorrelation(correlationDetail.BacktestDOWN, model, correlation);

                        model.CorrelationPass = indexOf != null;

                        if (model.CorrelationPass)
                        {
                            if ((indexOf ?? -1) >= 0)
                                correlationDetail.BacktestDOWN.Insert(indexOf.Value, model);
                            if ((indexOf ?? 0) == -1)
                                correlationDetail.BacktestDOWN.Add(model);
                        }
                        else
                        {
                            ProjectDirectoryService.DeleteFile(fi.FullName);
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

        private int? IndexOfCorrelation(List<BacktestModel> backtests, BacktestModel btModel, decimal correlation)
        {
            int? indexOf = !backtests.Any() ? -1 : null;

            foreach (var btItem in backtests)
            {
                List<BacktestOperationModel> coincidences = btItem.Backtests.Where(
                    _btoItem => btModel.Backtests.Any(_bto => _bto.Date == _btoItem.Date)).ToList();

                int totalCoincidences = coincidences.Count();
                int btItemCount = btItem.Backtests.Count();
                int btModelCount = btModel.Backtests.Count();

                decimal percentBtItem = totalCoincidences * 100 / btItemCount;
                decimal percentBtModel = totalCoincidences * 100 / btModelCount;

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
            string label,
            List<string> node,
            ConfigurationBaseDTO config,
            IEnumerable<Candle> data)
        {
            try
            {
                BacktestModel bkmIS = BacktestExecute(
                    label,
                    config.FromDateIS.Value,
                    config.ToDateIS.Value,
                    node.ToList(),
                    data,
                    config.TimeframeId,
                    config.Variation);

                BacktestModel bkmOS = BacktestExecute(
                    label,
                    config.FromDateOS.Value,
                    config.ToDateOS.Value,
                    node.ToList(),
                    data,
                    config.TimeframeId,
                    config.Variation);

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

        public BacktestModel BacktestExecute(
            string label,
            DateTime fromDate,
            DateTime toDate,
            List<string> node,
            IEnumerable<Candle> data,
            int periodId,
            decimal variation = 0)
        {
            try
            {
                BacktestModel bk = new BacktestModel
                {
                    Label = label,
                    FromDate = fromDate,
                    ToDate = toDate,
                    PeriodId = periodId,
                    Variation = variation,
                    Node = node,

                    Backtests = new List<BacktestOperationModel>()
                };

                List<IndicatorBase> rules = ExtractorService.BuildIndicatorsFromNode(node);

                if (rules.Any())
                {
                    List<IndicatorBase> extractorResult = ExtractorService.ExtractorExecute(fromDate, toDate, rules, data, periodId, variation);
                    if (extractorResult.Any())
                    {
                        int totalRules = extractorResult.Count;
                        var temporalIndicator = extractorResult.FirstOrDefault();
                        int length = temporalIndicator.Output.Length;

                        bk.TotalOpportunity = length;

                        int counter = 0;
                        while (counter < length)
                        {
                            IntervalLabel il = temporalIndicator.IntervalLabels[counter];
                            string upOrDown = il.Label;

                            int passed = 0;

                            foreach (var indicator in rules)
                            {
                                double output = indicator.Output[counter];

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

                            if (passed == totalRules)
                            {
                                bk.TotalTrades++;
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
                }

                return bk;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<Exception>(ex);
                throw;
            }
        }

        private static bool ApplyWinningStrategyRules(BacktestModel bkmIS, BacktestModel bkmOS, ConfigurationBaseDTO config,
            double variationPercent, double progressiveness)
        {
            return bkmIS.WinningTrades >= config.MinTransactionCountIS &&
                    bkmIS.PercentSuccess >= (double)config.MinPercentSuccessIS &&
                    bkmOS.WinningTrades >= config.MinTransactionCountOS &&
                    bkmOS.PercentSuccess >= (double)config.MinPercentSuccessOS &&
                    variationPercent <= (double)config.VariationTransaction &&

                    (config.IsProgressiveness ? progressiveness <= (double)config.Progressiveness : true);
        }

        // Backtest Serialization

        public void BacktestSerialize(string projectName, BacktestModel model)
        {
            string directory = model.Label.ToLower() == "up"
                                            ? projectName.ProjectStrategyBuilderNodesUPDirectory()
                                            : projectName.ProjectStrategyBuilderNodesDOWNDirectory();

            SerializerHelper.XMLSerializeObject(model, string.Format(@"{0}\{1}.xml", directory,
                                            RegexHelper.GetValidFileName(model.NodeName(), "_")));
        }

        public BacktestModel BacktestDeserialize(string path)
        {
            return SerializerHelper.XMLDeSerializeObject<BacktestModel>(path);
        }
    }
}
