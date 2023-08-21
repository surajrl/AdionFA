using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.Extractor.Contracts;
using AdionFA.Infrastructure.Extractor.Model;
using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.Weka.Model;
using AdionFA.TransferObject.Project;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace AdionFA.Infrastructure.Modules.Builder
{
    public class BuilderService : IBuilderService
    {
        // Services

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IExtractorService _extractorService;
        private readonly IMarketDataService _marketDataService;

        public BuilderService()
        {
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
            _extractorService = IoC.Kernel.Get<IExtractorService>();
            _marketDataService = IoC.Kernel.Get<IMarketDataService>();
        }

        // Correlation

        public IList<TNode> Correlation<TNode>(
            string projectName,
            EntityTypeEnum entityType,
            decimal maxCorrelation) where TNode : INodeModel
        {
            var directory = string.Empty;
            var winningNodes = new List<TNode>();

            FindCorrelation(Label.UP);
            FindCorrelation(Label.DOWN);

            void FindCorrelation(Label label)
            {
                if (entityType is EntityTypeEnum.NodeBuilder)
                {
                    directory = label == Label.UP
                        ? projectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.NodeBuilderNodesUP.GetDescription())
                        : projectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.NodeBuilderNodesDOWN.GetDescription());
                }
                else if (entityType is EntityTypeEnum.AssemblyBuilder)
                {
                    directory = label == Label.UP
                        ? projectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.AssemblyBuilderNodesUP.GetDescription())
                        : projectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.AssemblyBuilderNodesDOWN.GetDescription());
                }
                else if (entityType is EntityTypeEnum.CrossingBuilder)
                {
                    directory = label == Label.UP
                        ? projectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.CrossingBuilderNodesUP.GetDescription())
                        : projectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.CrossingBuilderNodesDOWN.GetDescription());
                }

                var backtests = new List<BacktestModel>();

                _projectDirectoryService.GetFilesInPath(directory, "*.xml")
                    .ToList()
                    .ForEach(file =>
                    {
                        var node = SerializerHelper.XMLDeSerializeObject<TNode>(file.FullName);

                        // Algorithm to find max correlation

                        var indexOf = IndexOfCorrelation(backtests, node.BacktestIS, maxCorrelation);

                        node.BacktestIS.CorrelationPass = indexOf != null;

                        if (node.BacktestIS.CorrelationPass)
                        {
                            winningNodes.Add(node);

                            if (indexOf >= 0)
                            {
                                backtests.Insert(indexOf.Value, node.BacktestIS);
                            }
                            if (indexOf == -1)
                            {
                                backtests.Add(node.BacktestIS);
                            }
                        }
                        else
                        {
                            _projectDirectoryService.DeleteFile(file.FullName);
                        }
                    });
            }

            return winningNodes;
        }

        private static int? IndexOfCorrelation(
            IList<BacktestModel> backtests,
            BacktestModel btModel,
            decimal maxCorrelation)
        {
            int? indexOf = !backtests.Any()
                ? -1
                : null;

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

                if ((percentBtItem + percentBtModel) / 2 <= maxCorrelation)
                {
                    indexOf = percentBtItem > percentBtModel
                        ? backtests.IndexOf(backtest)
                        : -1;
                }

                if (indexOf > -1)
                {
                    break;
                }
            }

            return indexOf;
        }

        // Backtest

        public bool BuildBacktestOfSingleNode(
            SingleNodeModel singleNode,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
            CancellationToken cancellationToken)
        {
            try
            {
                // Backtest OS

                singleNode.BacktestStatusOS = BacktestStatus.Executing;
                singleNode.BacktestOS = Backtest(
                    EntityTypeEnum.NodeBuilder,
                    projectConfiguration.FromDateOS.Value,
                    projectConfiguration.ToDateOS.Value,
                    projectConfiguration.Project.HistoricalData.TimeframeId,
                    singleNode.NodeData,
                    null,
                    candles,
                    cancellationToken);
                singleNode.BacktestStatusOS = BacktestStatus.Completed;

                // Backtest OS pass conditions

                var passBacktestOS =
                    singleNode.BacktestOS.TotalTrades >= projectConfiguration.NodeBuilderConfiguration.MinTotalTradesOS
                    && singleNode.BacktestOS.SuccessRatePercent >= projectConfiguration.NodeBuilderConfiguration.MinSuccessRatePercentOS;

                if (!passBacktestOS)
                {
                    return singleNode.WinningStrategy = passBacktestOS;
                }

                // Backtest IS

                singleNode.BacktestStatusIS = BacktestStatus.Executing;
                singleNode.BacktestIS = Backtest(
                    EntityTypeEnum.NodeBuilder,
                    projectConfiguration.FromDateIS.Value,
                    projectConfiguration.ToDateIS.Value,
                    projectConfiguration.Project.HistoricalData.TimeframeId,
                    singleNode.NodeData,
                    null,
                    candles,
                    cancellationToken);
                singleNode.BacktestStatusIS = BacktestStatus.Completed;

                // Backtest IS pass conditions

                var passBacktestIS =
                    singleNode.BacktestIS.TotalTrades >= projectConfiguration.NodeBuilderConfiguration.MinTotalTradesIS
                    && singleNode.BacktestIS.SuccessRatePercent >= projectConfiguration.NodeBuilderConfiguration.MinSuccessRatePercentIS;

                // Winning conditions

                return singleNode.WinningStrategy =
                    passBacktestOS
                    && passBacktestIS
                    && singleNode.SuccessRateVariation <= projectConfiguration.NodeBuilderConfiguration.MaxSuccessRateVariation
                    && (!projectConfiguration.IsProgressiveness || singleNode.ProgressivenessVariation <= projectConfiguration.MaxProgressivenessVariation);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public bool BuildBacktestOfAssemblyNode(
            AssemblyNodeModel assemblyNode,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
            decimal meanSuccessRatePercentIS,
            CancellationToken cancellationToken)
        {
            // Backtest IS

            assemblyNode.BacktestStatusIS = BacktestStatus.Executing;
            assemblyNode.BacktestIS = Backtest(
                EntityTypeEnum.AssemblyBuilder,
                projectConfiguration.FromDateIS.Value,
                projectConfiguration.ToDateIS.Value,
                projectConfiguration.Project.HistoricalData.TimeframeId,
                assemblyNode.ParentNodeData,
                assemblyNode.ChildNodesData,
                candles,
                cancellationToken);
            assemblyNode.BacktestStatusIS = BacktestStatus.Completed;

            // Backtest IS pass conditions

            var passBacktestIS =
                assemblyNode.BacktestIS.SuccessRatePercent - meanSuccessRatePercentIS >= projectConfiguration.AssemblyBuilderConfiguration.MinSuccessRateImprovementIS
                && assemblyNode.BacktestIS.TotalTrades >= projectConfiguration.AssemblyBuilderConfiguration.MinTotalTradesIS;

            if (!passBacktestIS)
            {
                return passBacktestIS;
            }

            // Backtest OS

            assemblyNode.BacktestStatusOS = BacktestStatus.Executing;
            assemblyNode.BacktestOS = Backtest(
                EntityTypeEnum.AssemblyBuilder,
                projectConfiguration.FromDateOS.Value,
                projectConfiguration.ToDateOS.Value,
                projectConfiguration.Project.HistoricalData.TimeframeId,
                assemblyNode.ParentNodeData,
                assemblyNode.ChildNodesData,
                candles,
                cancellationToken);
            assemblyNode.BacktestStatusOS = BacktestStatus.Completed;

            // Backtest OS pass conditions

            var passBacktestOS = assemblyNode.BacktestOS.SuccessRatePercent - meanSuccessRatePercentIS >= projectConfiguration.AssemblyBuilderConfiguration.MinSuccessRateImprovementOS;

            // Winning conditions

            return
                passBacktestIS
                && passBacktestOS
                && (!projectConfiguration.IsProgressiveness || assemblyNode.ProgressivenessVariation <= projectConfiguration.MaxProgressivenessVariation);
        }

        public bool BuildBacktestOfStrategyNode(
            StrategyNodeModel strategyNode,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
            decimal previousSuccessRatePercentIS,
            decimal previousSuccessRatePercentOS,
            CancellationToken cancellationToken)
        {
            try
            {
                // Backtest OS

                strategyNode.BacktestStatusOS = BacktestStatus.Executing;
                strategyNode.BacktestOS = BacktestStrategyNode(
                    strategyNode,
                    candles,
                    projectConfiguration.FromDateOS.Value,
                    projectConfiguration.ToDateOS.Value,
                    projectConfiguration.Project.HistoricalData.TimeframeId,
                    cancellationToken);
                strategyNode.BacktestStatusOS = BacktestStatus.Completed;

                // Backtest OS pass conditions

                var successRateImprovementOS = strategyNode.BacktestOS.SuccessRatePercent - previousSuccessRatePercentOS;

                var passBacktestOS =
                    successRateImprovementOS >= projectConfiguration.CrossingBuilderConfiguration.MinSuccessRateImprovementOS
                    && successRateImprovementOS <= projectConfiguration.CrossingBuilderConfiguration.MaxSuccessRateImprovementOS;

                if (!passBacktestOS)
                {
                    return passBacktestOS;
                }

                // Backtest IS

                strategyNode.BacktestStatusIS = BacktestStatus.Executing;
                strategyNode.BacktestIS = BacktestStrategyNode(
                    strategyNode,
                    candles,
                    projectConfiguration.FromDateIS.Value,
                    projectConfiguration.ToDateIS.Value,
                    projectConfiguration.Project.HistoricalData.TimeframeId,
                    cancellationToken);
                strategyNode.BacktestStatusIS = BacktestStatus.Completed;

                // Backtest IS pass conditions

                var successRateImprovementIS = strategyNode.BacktestIS.SuccessRatePercent - previousSuccessRatePercentIS;

                var passBacktestIS =
                    successRateImprovementIS >= projectConfiguration.CrossingBuilderConfiguration.MinSuccessRateImprovementIS
                    && successRateImprovementIS <= projectConfiguration.CrossingBuilderConfiguration.MaxSuccessRateImprovementIS;

                // Winning conditions

                return passBacktestOS && passBacktestIS;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private BacktestModel BacktestStrategyNode(
            StrategyNodeModel strategyNode,
            IEnumerable<Candle> mainCandles,
            DateTime fromDate,
            DateTime toDate,
            int timeframeId,
            CancellationToken cancellationToken)
        {
            var backtest = new BacktestModel
            {
                FromDate = fromDate,
                ToDate = toDate,
                TimeframeId = timeframeId,
                BacktestOperations = new List<BacktestOperationModel>()
            };

            var mainCandlesSample = mainCandles
                .Where(candle =>
                {
                    var dateTime = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time);
                    return dateTime >= fromDate && dateTime <= toDate;
                })
                .Select(candle => candle)
                .ToList();

            var allCrossingCandlesSample = new Dictionary<int, List<Candle>>();
            foreach (var crossingNode in strategyNode.CrossingNodesData)
            {
                allCrossingCandlesSample.Add(crossingNode.Item2,
                    _marketDataService.GetHistoricalDataCandles(crossingNode.Item2)
                    .Select(candle => new Candle
                    {
                        Date = candle.StartDate,
                        Time = candle.StartTime,

                        Open = candle.Open,
                        High = candle.High,
                        Low = candle.Low,
                        Close = candle.Close,

                        Volume = candle.Volume,
                        Spread = candle.Spread
                    })
                    .OrderBy(candle => candle.Date)
                    .ThenBy(candle => candle.Time)
                    .Where(candle =>
                    {
                        var dateTime = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time);
                        return dateTime >= fromDate && dateTime <= toDate;

                    })
                    .ToList());
            }

            for (var candleIdx = 0; candleIdx < mainCandlesSample.Count - 1; candleIdx++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                backtest.TotalOpportunity++;

                var mainCurrentCandle = new Candle
                {
                    Date = mainCandlesSample[candleIdx].Date,
                    Time = mainCandlesSample[candleIdx].Time,

                    Open = mainCandlesSample[candleIdx].Open,
                    High = mainCandlesSample[candleIdx].Open,
                    Low = mainCandlesSample[candleIdx].Open,
                    Close = mainCandlesSample[candleIdx].Open,

                    Spread = mainCandlesSample[candleIdx].Spread
                };

                if (strategyNode.ParentNodesData.Count > 0)
                {
                    var parentNodeSignal = false;
                    foreach (var parentNode in strategyNode.ParentNodesData)
                    {
                        var parentNodeIndicators = _extractorService.BuildIndicatorsFromNode(parentNode.Node);
                        parentNodeSignal = ApproveCandle(
                            parentNodeIndicators,
                            candleIdx,
                            mainCandlesSample[0],
                            mainCurrentCandle,
                            mainCandlesSample);

                        if (parentNodeSignal)
                        {
                            break;
                        }
                    }

                    if (!parentNodeSignal)
                    {
                        // Move to the next candle
                        continue;
                    }
                }

                // Check for at least one child node
                var childNodeSignal = false;
                foreach (var childNode in strategyNode.ChildNodesData)
                {
                    var childNodeIndicators = _extractorService.BuildIndicatorsFromNode(childNode.Node);
                    childNodeSignal = ApproveCandle(
                        childNodeIndicators,
                        candleIdx,
                        mainCandlesSample[0],
                        mainCurrentCandle,
                        mainCandlesSample);

                    if (childNodeSignal)
                    {
                        break;
                    }
                }

                if (!childNodeSignal)
                {
                    // Move to the next candle
                    continue;
                }

                // Check for at least one crossing node
                var crossingNodeSignal = false;
                foreach (var crossingNode in strategyNode.CrossingNodesData)
                {
                    var crossingCandlesSample = allCrossingCandlesSample.GetValueOrDefault(crossingNode.Item2);

                    var currentCrossingCandle = new Candle
                    {
                        Date = crossingCandlesSample[candleIdx].Date,
                        Time = crossingCandlesSample[candleIdx].Time,

                        Open = crossingCandlesSample[candleIdx].Open,
                        High = crossingCandlesSample[candleIdx].Open,
                        Low = crossingCandlesSample[candleIdx].Open,
                        Close = crossingCandlesSample[candleIdx].Open,

                        Spread = crossingCandlesSample[candleIdx].Spread
                    };

                    var crossingNodeIndicators = _extractorService.BuildIndicatorsFromNode(crossingNode.Item1.Node);
                    crossingNodeSignal = ApproveCandle(
                        crossingNodeIndicators,
                        candleIdx,
                        crossingCandlesSample[0],
                        currentCrossingCandle,
                        crossingCandlesSample);

                    if (crossingNodeSignal)
                    {
                        break;
                    }
                }

                if (!crossingNodeSignal)
                {
                    // Move to the next candle
                    continue;
                }

                backtest.TotalTrades++;

                var spread = mainCurrentCandle.Spread * 0.00001;

                var isWinnerTrade = strategyNode.Label == Label.UP
                    ? mainCurrentCandle.Open + spread < mainCandlesSample[candleIdx + 1].Open
                    : mainCurrentCandle.Open > mainCandlesSample[candleIdx + 1].Open + spread;

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
                    Date = DateTimeHelper.BuildDateTime(timeframeId, mainCurrentCandle.Date, mainCurrentCandle.Time),
                    IsWinner = isWinnerTrade
                });
            }

            return backtest;
        }

        private BacktestModel Backtest(
            EntityTypeEnum entityType,
            DateTime fromDate,
            DateTime toDate,
            int timeframeId,
            REPTreeNodeModel parentNode,
            IList<REPTreeNodeModel> childNodes,
            IEnumerable<Candle> candles,
            CancellationToken cancellationToken)
        {
            var parentNodeIndicators = _extractorService.BuildIndicatorsFromNode(parentNode.Node);

            var candlesRange = (from candle in candles
                                let dt = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time)
                                where dt >= fromDate && dt <= toDate
                                select candle).ToList();

            var backtest = new BacktestModel
            {
                FromDate = fromDate,
                ToDate = toDate,
                TimeframeId = timeframeId,
                BacktestOperations = new List<BacktestOperationModel>()
            };

            for (var idx = 0; idx < candlesRange.Count - 1; idx++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var firstCandle = candlesRange[0];
                var nextCandle = candlesRange[idx + 1];
                var currentCandle = new Candle
                {
                    Date = candlesRange[idx].Date,
                    Time = candlesRange[idx].Time,

                    Open = candlesRange[idx].Open,
                    High = candlesRange[idx].Open,
                    Low = candlesRange[idx].Open,
                    Close = candlesRange[idx].Open,

                    Spread = candlesRange[idx].Spread
                };

                backtest.TotalOpportunity++;

                if (ApproveCandle(parentNodeIndicators, idx, firstCandle, currentCandle, candlesRange))
                {
                    if (entityType == EntityTypeEnum.AssemblyBuilder)
                    {
                        var childNodePass = false;
                        foreach (var childNode in childNodes)
                        {
                            var childNodeIndicators = _extractorService.BuildIndicatorsFromNode(childNode.Node);
                            if (ApproveCandle(childNodeIndicators, idx, firstCandle, currentCandle, candlesRange))
                            {
                                childNodePass = true;
                                break;
                            }
                        }

                        if (!childNodePass)
                        {
                            // Move to the next candle if none of the child nodes is a pass
                            continue;
                        }
                    }

                    backtest.TotalTrades++;

                    var isWinnerTrade = false;
                    var spread = currentCandle.Spread * 0.00001;

                    isWinnerTrade = parentNode.Label.ToLowerInvariant() == "up"
                        ? currentCandle.Open + spread < nextCandle.Open
                        : currentCandle.Open > nextCandle.Open + spread;

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
                    return false;
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

        public static HashSet<BacktestOperationModel> GetBacktestOperations<T>(IList<T> nodes) where T : INodeModel
        {
            var backtestOperations = nodes
                .Select(singleNode => singleNode.BacktestIS)
                .ToList()
                .SelectMany(backtest => backtest.BacktestOperations.OrderBy(backtestOperation => backtestOperation.Date))
                .ToHashSet();

            return backtestOperations.OrderBy(dateTime => dateTime.Date).ToHashSet();
        }

    }
}
