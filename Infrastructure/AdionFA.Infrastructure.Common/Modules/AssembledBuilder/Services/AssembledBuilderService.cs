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
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Common;
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

        public AssembledBuilderService()
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _extractorService = IoC.Get<IExtractorService>();
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
            ProjectConfigurationDTO configuration)
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
                    var backtests = label == "up" ?
                        assembledBuilder.UPBacktests :
                        assembledBuilder.DOWNBacktests;

                    var directory = label == "up" ?
                        projectName.ProjectAssembledBuilderExtractorUPDirectory() :
                        projectName.ProjectAssembledBuilderExtractorDOWNDirectory();

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
                                    configuration.TimeframeId,
                                    configuration.ExtractorMinVariation);

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

                                if (!configuration.WithoutSchedule && configuration.ProjectScheduleConfigurations.Any())
                                {
                                    // America

                                    configuration.ProjectScheduleConfigurations.ToList().MarketStartTime(MarketRegionEnum.America,
                                        out DateTime fromTimeInSecondsAmerica, out DateTime toTimeInSecondsAmerica);

                                    _extractorService.ExtractorWrite(
                                        projectName.ProjectAssembledBuilderExtractorAmericaDirectory(label, $"{nameSignature}.{timeSignature}.America.csv"),
                                        indicators,
                                        fromTimeInSecondsAmerica.Hour,
                                        toTimeInSecondsAmerica.Hour);

                                    // Europe

                                    configuration.ProjectScheduleConfigurations.ToList().MarketStartTime(MarketRegionEnum.Europe,
                                        out DateTime fromTimeInSecondsEurope, out DateTime toTimeInSecondsEurope);

                                    _extractorService.ExtractorWrite(
                                        projectName.ProjectAssembledBuilderExtractorEuropeDirectory(label, $"{nameSignature}.{timeSignature}.Europe.csv"),
                                        indicators,
                                        fromTimeInSecondsEurope.Hour,
                                        toTimeInSecondsEurope.Hour);

                                    // Asia

                                    configuration.ProjectScheduleConfigurations.ToList().MarketStartTime(MarketRegionEnum.Asia,
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
            ConfigurationDTO configuration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            var strategyBuilder = new StrategyBuilderModel();

            strategyBuilder.IS = ExecuteBacktest(
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

            strategyBuilder.OS = ExecuteBacktest(
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

        private BacktestModel ExecuteBacktest(
            string nodeLabel,
            DateTime fromDate,
            DateTime toDate,
            int timeframeId,
            IEnumerable<Candle> candles,
            IList<string> parentNode,
            IList<BacktestModel> childNodes,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            try
            {
                var parentNodeIndicators = _extractorService.BuildIndicatorsFromNode(parentNode);

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
                    Node = parentNode.ToList(),

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


                    if (ApproveCandle(parentNodeIndicators, candleIdx, firstCandle, currentCandle, candlesRange))
                    {
                        foreach (var childNode in childNodes)
                        {
                            var childNodeIndicators = _extractorService.BuildIndicatorsFromNode(childNode.Node);
                            if (ApproveCandle(childNodeIndicators, candleIdx, firstCandle, currentCandle, candlesRange))
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

                                break;
                            }
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

        // Backtest Serialization

        public void SerializeBacktest(string projectName, BacktestModel model)
        {
            var directory = model.Label.ToLower() == "up" ?
                projectName.ProjectAssembledBuilderNodesUPDirectory() :
                projectName.ProjectAssembledBuilderNodesDOWNDirectory();


            var filename = string.Format(@"{0}\{1}.xml",
                directory,
                RegexHelper.GetValidFileName(model.NodeName(), "_"));

            SerializerHelper.XMLSerializeObject(model, filename);
        }

        public BacktestModel DeserializeBacktest(string path)
        {
            return SerializerHelper.XMLDeSerializeObject<BacktestModel>(path);
        }
    }
}
