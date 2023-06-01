using AdionFA.Infrastructure.Common.AssembledBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extensions;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdionFA.Infrastructure.Common.AssembledBuilder.Services
{
    public class AssembledBuilderService : IAssembledBuilderService
    {
        // Services

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IExtractorService _extractorService;
        private readonly IStrategyBuilderService _strategyBuilderService;

        public AssembledBuilderService()
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _extractorService = IoC.Get<IExtractorService>();
            _strategyBuilderService = IoC.Get<IStrategyBuilderService>();
        }

        public AssembledBuilderModel LoadStrategyBuilderResult(string projectName)
        {
            try
            {
                var assembledBuilder = new AssembledBuilderModel();

                // Load UP Nodes

                var upDirectory = projectName.ProjectStrategyBuilderNodesUPDirectory();
                _projectDirectoryService.GetFilesInPath(upDirectory, "*.xml").ToList().ForEach(file =>
                {
                    if (file.Name.Contains("BACKTEST"))
                    {
                        var backtest = SerializerHelper.XMLDeSerializeObject<BacktestModel>(file.FullName);
                        assembledBuilder.UPBacktests.Add(backtest);
                    }
                });

                // Load DOWN Nodes

                var downDirectory = projectName.ProjectStrategyBuilderNodesDOWNDirectory();
                _projectDirectoryService.GetFilesInPath(downDirectory, "*.xml").ToList().ForEach(file =>
                {
                    if (file.Name.Contains("BACKTEST"))
                    {
                        var backtest = SerializerHelper.XMLDeSerializeObject<BacktestModel>(file.FullName);
                        assembledBuilder.DOWNBacktests.Add(backtest);
                    }
                });

                return assembledBuilder;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<AssembledBuilderService>(ex);
                throw;
            }
        }

        public void CreateExtraction(
            string projectName,
            AssembledBuilderModel assembledBuilder,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration)
        {
            try
            {
                if (assembledBuilder.UPBacktests?.Any() ?? false)
                {
                    Extractor("up");
                }

                if (assembledBuilder.DOWNBacktests?.Any() ?? false)
                {
                    Extractor("down");
                }

                void Extractor(string label)
                {
                    var backtests = label == "up"
                        ? assembledBuilder.UPBacktests
                        : assembledBuilder.DOWNBacktests;

                    var directory = label == "up"
                        ? projectName.ProjectAssembledBuilderExtractorUPDirectory()
                        : projectName.ProjectAssembledBuilderExtractorDOWNDirectory();

                    var operations = backtests
                        .SelectMany(backtest => backtest.BacktestOperations
                        .OrderBy(backtestOperation => backtestOperation.Date)).ToList();

                    if (operations.Any() && _projectDirectoryService.DeleteAllFiles(directory, option: SearchOption.AllDirectories, isBackup: false))
                    {
                        var firstOperation = operations.FirstOrDefault().Date;
                        var lastOperation = operations.LastOrDefault().Date;

                        var templates = _projectDirectoryService.GetFilesInPath(projectName.ProjectExtractorTemplatesDirectory()).ToList();

                        Parallel.ForEach(
                            templates,
                            new ParallelOptions { MaxDegreeOfParallelism = 1 },
                            file =>
                            {
                                var indicators = _extractorService.BuildIndicatorsFromCSV(file.FullName);
                                var extractions = _extractorService.DoExtraction(
                                    firstOperation,
                                    lastOperation,
                                    indicators,
                                    candles.ToList(),
                                    projectConfiguration.TimeframeId,
                                    projectConfiguration.ExtractorMinVariation);

                                foreach (var extraction in extractions)
                                {
                                    var intervals = (from il in extraction.IntervalLabels.Select((_il, _idx) => new { _idx, _il })
                                                     let backtestOperation = operations.Where(operation => operation.Date == il._il.Interval)
                                                     where backtestOperation.Any()
                                                     select new
                                                     {
                                                         idx = il._idx,
                                                         il = new IntervalLabel
                                                         {
                                                             Interval = il._il.Interval,
                                                             Label = backtestOperation.Any(operation => operation.IsWinner) ? "UP" : "DOWN"
                                                         },
                                                     }).ToList();

                                    extraction.IntervalLabels = intervals.Select(a => a.il).ToArray();

                                    var outputExtraction = new List<double>();
                                    foreach (var idx in intervals.Select(a => a.idx))
                                    {
                                        outputExtraction.Add(extraction.Output[idx]);
                                    }

                                    extraction.Output = outputExtraction.ToArray();
                                }

                                var timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss");
                                var nameSignature = file.Name.Replace(".csv", string.Empty);
                                var output = projectName.ProjectAssembledBuilderExtractorWithoutScheduleDirectory(label, $"{nameSignature}.{timeSignature}.csv");

                                _extractorService.ExtractorWrite(output, extractions, 0, 0);

                                if (!projectConfiguration.WithoutSchedule && projectConfiguration.ProjectScheduleConfigurations.Any())
                                {
                                    // America

                                    projectConfiguration.ProjectScheduleConfigurations.ToList().MarketStartTime(MarketRegionEnum.America,
                                        out DateTime fromTimeInSecondsAmerica, out DateTime toTimeInSecondsAmerica);

                                    _extractorService.ExtractorWrite(
                                        projectName.ProjectAssembledBuilderExtractorAmericaDirectory(label, $"{nameSignature}.{timeSignature}.America.csv"),
                                        indicators,
                                        fromTimeInSecondsAmerica.Hour,
                                        toTimeInSecondsAmerica.Hour);

                                    // Europe

                                    projectConfiguration.ProjectScheduleConfigurations.ToList().MarketStartTime(MarketRegionEnum.Europe,
                                        out DateTime fromTimeInSecondsEurope, out DateTime toTimeInSecondsEurope);

                                    _extractorService.ExtractorWrite(
                                        projectName.ProjectAssembledBuilderExtractorEuropeDirectory(label, $"{nameSignature}.{timeSignature}.Europe.csv"),
                                        indicators,
                                        fromTimeInSecondsEurope.Hour,
                                        toTimeInSecondsEurope.Hour);

                                    // Asia

                                    projectConfiguration.ProjectScheduleConfigurations.ToList().MarketStartTime(MarketRegionEnum.Asia,
                                        out DateTime fromTimeInSecondsAsia, out DateTime toTimeInSecondsAsia);

                                    _extractorService.ExtractorWrite(
                                        projectName.ProjectAssembledBuilderExtractorAsiaDirectory(label, $"{nameSignature}.{timeSignature}.Asia.csv"),
                                        indicators,
                                        fromTimeInSecondsAsia.Hour,
                                        toTimeInSecondsAsia.Hour);
                                }
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<AssembledBuilderService>(ex);
                throw;
            }
        }

        public StrategyBuilderModel BuildBacktestOfNode(
            string nodeLabel,
            IList<string> parentNode,
            IList<BacktestModel> childNodes,
            ProjectConfigurationDTO configuration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            var strategyBuilder = new StrategyBuilderModel();

            strategyBuilder.IS = _strategyBuilderService.ExecuteBacktest(
                EntityTypeEnum.AssembledBuilder,
                nodeLabel,
                configuration.FromDateIS.Value,
                configuration.ToDateIS.Value,
                configuration.TimeframeId,
                candles,
                parentNode,
                childNodes,
                manualResetEvent,
                cancellationToken);

            var meanPercentSuccessIS = childNodes
                .Where(backtest => backtest.Label.ToLower() == "up")
                .Select(backtest => backtest.Variation)
                .Sum() / childNodes.Count;

            var winningStrategyIS =
                ((decimal)strategyBuilder.IS.PercentSuccess - meanPercentSuccessIS) >= configuration.ABMinImprovePercent
                && strategyBuilder.IS.TotalTrades >= configuration.ABTransactionsTarget;

            if (!winningStrategyIS)
            {
                return strategyBuilder;
            }

            strategyBuilder.OS = _strategyBuilderService.ExecuteBacktest(
                EntityTypeEnum.AssembledBuilder,
                nodeLabel,
                configuration.FromDateOS.Value,
                configuration.ToDateOS.Value,
                configuration.TimeframeId,
                candles,
                parentNode,
                childNodes,
                manualResetEvent,
                cancellationToken);

            strategyBuilder.WinningStrategy =
                    strategyBuilder.VariationPercent <= (double)configuration.SBMaxTransactionsVariation
                    && (!configuration.IsProgressiveness || strategyBuilder.Progressiveness <= (double)configuration.Progressiveness);


            return strategyBuilder;
        }
    }
}
