using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.AssemblyBuilder.Model;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.Extractor.Contracts;
using AdionFA.Infrastructure.Extractor.Model;
using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.Modules.Strategy;
using AdionFA.Infrastructure.NodeBuilder.Contracts;
using AdionFA.Infrastructure.NodeBuilder.Model;
using AdionFA.Infrastructure.Weka.Model;
using AdionFA.TransferObject.Base;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace AdionFA.Infrastructure.NodeBuilder.Services
{
    public class NodeBuilderService : INodeBuilderService
    {
        // Services

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IExtractorService _extractorService;
        private readonly IMarketDataService _marketDataService;

        public NodeBuilderService()
        {
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
            _extractorService = IoC.Kernel.Get<IExtractorService>();
            _marketDataService = IoC.Kernel.Get<IMarketDataService>();
        }

        // Correlation

        public void Correlation(
            string projectName,
            NodeBuilderModel strategyBuilder,
            decimal maxCorrelation)
        {
            string directory;
            IList<NodeModel> winningNodes;

            FindCorrelation("up");
            FindCorrelation("down");

            void FindCorrelation(string label)
            {
                switch (label.ToLowerInvariant())
                {
                    case "up":
                        directory = projectName.ProjectNodeBuilderNodesUPDirectory();
                        winningNodes = strategyBuilder.WinningNodesUP;
                        break;

                    case "down":
                        directory = projectName.ProjectNodeBuilderNodesDOWNDirectory();
                        winningNodes = strategyBuilder.WinningNodesDOWN;
                        break;

                    default:
                        return;
                }

                var backtests = new List<BacktestModel>();

                _projectDirectoryService.GetFilesInPath(directory, "*.xml").ToList().ForEach(file =>
                {
                    var node = SerializerHelper.XMLDeSerializeObject<NodeModel>(file.FullName);

                    // Algorithm to find maxCorrelation
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
        }

        public void Correlation(
            string projectName,
            AssemblyBuilderModel assemblyBuilder,
            decimal maxCorrelation)
        {
            string directory;
            IList<AssemblyNodeModel> winningAssemblyNodes;

            FindCorrelation("up");
            FindCorrelation("down");

            void FindCorrelation(string label)
            {
                switch (label.ToLowerInvariant())
                {
                    case "up":
                        directory = projectName.ProjectAssemblyBuilderNodesUPDirectory();
                        winningAssemblyNodes = assemblyBuilder.WinningAssemblyNodesUP;
                        break;

                    case "down":
                        directory = projectName.ProjectAssemblyBuilderNodesDOWNDirectory();
                        winningAssemblyNodes = assemblyBuilder.WinningAssemblyNodesDOWN;
                        break;

                    default:
                        return;
                }

                var backtests = new List<BacktestModel>();

                _projectDirectoryService.GetFilesInPath(directory, "*.xml").ToList().ForEach(file =>
                {
                    var assemblyNode = SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName);

                    // Algorithm to find correlation
                    var indexOf = IndexOfCorrelation(backtests, assemblyNode.BacktestIS, maxCorrelation);
                    assemblyNode.BacktestIS.CorrelationPass = indexOf != null;

                    if (assemblyNode.BacktestIS.CorrelationPass)
                    {
                        winningAssemblyNodes.Add(assemblyNode);

                        if (indexOf >= 0)
                        {
                            backtests.Insert(indexOf.Value, assemblyNode.BacktestIS);
                        }
                        if (indexOf == -1)
                        {
                            backtests.Add(assemblyNode.BacktestIS);
                        }
                    }
                    else
                    {
                        _projectDirectoryService.DeleteFile(file.FullName);
                    }
                });
            }
        }

        private static int? IndexOfCorrelation(IList<BacktestModel> backtests, BacktestModel btModel, decimal correlation)
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

                if ((percentBtItem + percentBtModel) / 2 <= correlation)
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

        public bool BuildBacktestOfNode(
            NodeModel node,
            IEnumerable<Candle> candles,
            ConfigurationBaseDTO configuration,
            int timeframeId,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            try
            {
                // Backtest OS

                node.BacktestStatusOS = BacktestStatus.Executing;

                node.BacktestOS = Backtest(
                    EntityTypeEnum.StrategyBuilder,
                    configuration.FromDateOS.Value,
                    configuration.ToDateOS.Value,
                    timeframeId,
                    node.NodeData,
                    null,
                    candles,
                    manualResetEvent,
                    cancellationToken);

                node.BacktestStatusOS = BacktestStatus.Completed;

                // Backtest OS Pass Conditions

                var passBacktestOS =
                    node.BacktestOS.TotalTrades >= configuration.SBMinTotalTradesOS
                    && node.BacktestOS.SuccessRatePercent >= (double)configuration.SBMinSuccessRatePercentOS;

                if (!passBacktestOS)
                {
                    return node.WinningStrategy = passBacktestOS;
                }

                // Backtest IS

                node.BacktestStatusIS = BacktestStatus.Executing;

                node.BacktestIS = Backtest(
                    EntityTypeEnum.StrategyBuilder,
                    configuration.FromDateIS.Value,
                    configuration.ToDateIS.Value,
                    timeframeId,
                    node.NodeData,
                    null,
                    candles,
                    manualResetEvent,
                    cancellationToken);

                node.BacktestStatusIS = BacktestStatus.Completed;

                // Winning Conditions

                return node.WinningStrategy =
                    node.BacktestIS.TotalTrades >= configuration.SBMinTotalTradesIS
                    && node.BacktestIS.SuccessRatePercent >= (double)configuration.SBMinSuccessRatePercentIS
                    && node.SuccessRateVariation <= (double)configuration.SBMaxSuccessRateVariation
                    && (!configuration.IsProgressiveness || node.ProgressivenessVariation <= (double)configuration.MaxProgressivenessVariation);
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
            ConfigurationBaseDTO configuration,
            int timeframeId,
            double meanSuccessRatePercentIS,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            // Backtest IS

            assemblyNode.BacktestStatusIS = BacktestStatus.Executing;

            assemblyNode.BacktestIS = Backtest(
                EntityTypeEnum.AssemblyBuilder,
                configuration.FromDateIS.Value,
                configuration.ToDateIS.Value,
                timeframeId,
                assemblyNode.ParentNodeData,
                assemblyNode.ChildNodesData,
                candles,
                manualResetEvent,
                cancellationToken);

            assemblyNode.BacktestStatusIS = BacktestStatus.Completed;

            // Backtest IS Pass Conditions

            var passBacktestIS =
                (assemblyNode.BacktestIS.SuccessRatePercent - meanSuccessRatePercentIS) >= (double)configuration.ABMinImprovePercent
                && assemblyNode.BacktestIS.TotalTrades >= configuration.ABMinTotalTradesIS;

            if (!passBacktestIS)
            {
                return assemblyNode.WinningStrategy = passBacktestIS;
            }

            // Backtest OS

            assemblyNode.BacktestStatusOS = BacktestStatus.Executing;

            assemblyNode.BacktestOS = Backtest(
                EntityTypeEnum.AssemblyBuilder,
                configuration.FromDateOS.Value,
                configuration.ToDateOS.Value,
                timeframeId,
                assemblyNode.ParentNodeData,
                assemblyNode.ChildNodesData,
                candles,
                manualResetEvent,
                cancellationToken);

            assemblyNode.BacktestStatusOS = BacktestStatus.Completed;

            // Winning Conditions

            return assemblyNode.WinningStrategy =
                assemblyNode.SuccessRateVariation <= (double)configuration.SBMaxSuccessRateVariation
                && (!configuration.IsProgressiveness || assemblyNode.ProgressivenessVariation <= (double)configuration.MaxProgressivenessVariation);
        }

        public bool BuildBacktestOfStrategyNode(
            StrategyNodeModel strategyNode,
            IEnumerable<Candle> mainCandles,
            IEnumerable<BacktestOperationModel> backtestOperationsIS,
            IEnumerable<BacktestOperationModel> backtestOperationsOS,
            ConfigurationBaseDTO configuration,
            int timeframeId,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            try
            {
                // Backtest OS

                strategyNode.BacktestStatusOS = BacktestStatus.Executing;

                strategyNode.BacktestOS = BacktestStrategyNode(
                    strategyNode,
                    mainCandles,
                    backtestOperationsOS,
                    configuration.FromDateOS.Value,
                    configuration.ToDateOS.Value,
                    timeframeId,
                    manualResetEvent,
                    cancellationToken);

                strategyNode.BacktestStatusOS = BacktestStatus.Completed;

                // Backtest OS Pass Conditions

                if (strategyNode.BacktestOS.WinningTrades == 0)
                {
                    return false;
                }

                // Backtest IS

                strategyNode.BacktestStatusIS = BacktestStatus.Executing;

                strategyNode.BacktestIS = BacktestStrategyNode(
                    strategyNode,
                    mainCandles,
                    backtestOperationsIS,
                    configuration.FromDateIS.Value,
                    configuration.ToDateIS.Value,
                    timeframeId,
                    manualResetEvent,
                    cancellationToken);

                strategyNode.BacktestStatusIS = BacktestStatus.Completed;

                // Winning Conditions

                if (strategyNode.BacktestIS.WinningTrades == 0)
                {
                    return false;
                }

                return true;
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
            IEnumerable<BacktestOperationModel> backtestOperations,
            DateTime fromDate,
            DateTime toDate,
            int timeframeId,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            var backtest = new BacktestModel
            {
                FromDate = fromDate,
                ToDate = toDate,
                TimeframeId = timeframeId,
                BacktestOperations = new List<BacktestOperationModel>()
            };

            var previousOperations = backtestOperations.Select(operation => operation.Date).ToList();

            var mainCandlesRange = mainCandles.Where(candle =>
            {
                var dt = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time);
                return dt >= fromDate && dt <= toDate;
            }).Select(candle => candle).ToList();

            var crossingCandlesRange = new Dictionary<int, List<Candle>>();
            foreach (var crossingNode in strategyNode.CrossingNodesData)
            {
                // TODO: REMOVE THIS SO IT DOES NOT CALL THE MARKET DATA SERVICE
                var historicalDataDTO = _marketDataService.GetHistoricalData(crossingNode.Item2, true);

                var candles = historicalDataDTO.HistoricalDataCandles.Select(candle => new Candle
                {
                    Date = candle.StartDate,
                    Time = candle.StartTime,

                    Open = candle.Open,
                    High = candle.High,
                    Low = candle.Low,
                    Close = candle.Close,

                    Volume = candle.Volume,
                    Spread = candle.Spread
                }).OrderBy(d => d.Date)
                .ThenBy(d => d.Time);

                var range = candles.Where(candle =>
                {
                    var dt = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time);
                    return dt >= fromDate && dt <= toDate;
                }).Select(candle => candle).ToList();

                crossingCandlesRange.Add(crossingNode.Item2, range);
            }

            for (var idx = 0; idx < mainCandlesRange.Count - 1; idx++)
            {
                manualResetEvent.Wait(CancellationToken.None);
                cancellationToken.ThrowIfCancellationRequested();

                backtest.TotalOpportunity++;

                var mainCurrentCandle = new Candle
                {
                    Date = mainCandlesRange[idx].Date,
                    Time = mainCandlesRange[idx].Time,

                    Open = mainCandlesRange[idx].Open,
                    High = mainCandlesRange[idx].Open,
                    Low = mainCandlesRange[idx].Open,
                    Close = mainCandlesRange[idx].Open,

                    Spread = mainCandlesRange[idx].Spread
                };

                // Skip dates that were not operated on
                if (!previousOperations.Contains(DateTimeHelper.BuildDateTime(timeframeId, mainCurrentCandle.Date, mainCurrentCandle.Time)))
                {
                    continue;
                }

                // Has to approve all of the crossing nodes
                var crossingNodePass = true;
                foreach (var crossingNode in strategyNode.CrossingNodesData)
                {
                    var candles = crossingCandlesRange.GetValueOrDefault(crossingNode.Item2);

                    var currentCandle = new Candle
                    {
                        Date = candles[idx].Date,
                        Time = candles[idx].Time,

                        Open = candles[idx].Open,
                        High = candles[idx].Open,
                        Low = candles[idx].Open,
                        Close = candles[idx].Open,

                        Spread = candles[idx].Spread
                    };

                    var crossingNodeIndicators = _extractorService.BuildIndicatorsFromNode(crossingNode.Item1.Node).ToList();
                    if (!ApproveCandle(crossingNodeIndicators, idx, candles[0], currentCandle, candles))
                    {
                        crossingNodePass = false;
                        break;
                    }
                }

                if (!crossingNodePass)
                {
                    // No trade signal, move to the next candle
                    continue;
                }

                backtest.TotalTrades++;

                var spread = mainCurrentCandle.Spread * 0.00001;

                var isWinnerTrade = strategyNode.ParentNodeData.Label.ToLowerInvariant() == "up"
                    ? (mainCurrentCandle.Open + spread) < mainCandlesRange[idx + 1].Open
                    : mainCurrentCandle.Open > (mainCandlesRange[idx + 1].Open + spread);

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
            ManualResetEventSlim manualResetEvent,
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
                manualResetEvent.Wait(CancellationToken.None);
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
    }
}
