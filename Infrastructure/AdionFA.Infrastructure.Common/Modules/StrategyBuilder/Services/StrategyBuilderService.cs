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
using AdionFA.TransferObject.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Services
{
    public class StrategyBuilderService : IStrategyBuilderService
    {
        // Services

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IExtractorService _extractorService;

        public StrategyBuilderService()
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _extractorService = IoC.Get<IExtractorService>();
        }

        // Correlation

        public void Correlation(
            string projectName,
            StrategyBuilderModel strategyBuilder,
            decimal correlation)
        {
            try
            {
                string directory;
                IList<BacktestModel> backtests;
                IList<REPTreeNodeModel> nodes;

                FindCorrelation("up");
                FindCorrelation("down");

                void FindCorrelation(string label)
                {
                    switch (label.ToLowerInvariant())
                    {
                        case "up":
                            directory = projectName.ProjectStrategyBuilderNodesUPDirectory();
                            backtests = strategyBuilder.CorrelationNodesUP.Select(node => node.BacktestIS).ToList();
                            nodes = strategyBuilder.CorrelationNodesUP;
                            break;

                        case "down":
                            directory = projectName.ProjectStrategyBuilderNodesDOWNDirectory();
                            backtests = strategyBuilder.CorrelationNodesDOWN.Select(node => node.BacktestIS).ToList();
                            nodes = strategyBuilder.CorrelationNodesDOWN;
                            break;

                        default:
                            return;
                    }

                    _projectDirectoryService.GetFilesInPath(directory, "*.xml").ToList().ForEach(file =>
                    {
                        var node = SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName);

                        // Algorithm to find correlation
                        var indexOf = IndexOfCorrelation(backtests, node.BacktestIS, correlation);

                        node.BacktestIS.CorrelationPass = indexOf != null;
                        if (node.BacktestIS.CorrelationPass)
                        {
                            nodes.Add(node);

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
            catch (Exception ex)
            {
                LogHelper.LogException<IStrategyBuilderService>(ex);
                throw;
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

        public void BuildBacktestOfNode(
            REPTreeNodeModel node,
            ProjectConfigurationDTO projectConfiguration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            try
            {
                // OS Backtest

                ExecuteBacktest(
                    node.BacktestOS,
                    EntityTypeEnum.StrategyBuilder,
                    projectConfiguration.FromDateOS.Value,
                    projectConfiguration.ToDateOS.Value,
                    projectConfiguration.TimeframeId,
                    candles,
                    node,
                    null,
                    manualResetEvent,
                    cancellationToken);

                node.WinningStrategy =
                    node.BacktestOS.WinningTrades >= projectConfiguration.SBMinTransactionsOS
                    && node.BacktestOS.SuccessRatePercent >= (double)projectConfiguration.SBMinSuccessRatePercentOS;

                if (!node.WinningStrategy)
                {
                    return;
                }

                // IS Backtest

                ExecuteBacktest(
                    node.BacktestIS,
                    EntityTypeEnum.StrategyBuilder,
                    projectConfiguration.FromDateIS.Value,
                    projectConfiguration.ToDateIS.Value,
                    projectConfiguration.TimeframeId,
                    candles,
                    node,
                    null,
                    manualResetEvent,
                    cancellationToken);

                node.WinningStrategy =
                     node.BacktestIS.WinningTrades >= projectConfiguration.SBMinTransactionsIS
                     && node.BacktestIS.SuccessRatePercent >= (double)projectConfiguration.SBMinSuccessRatePercentIS;

                if (!node.WinningStrategy)
                {
                    return;
                }

                node.WinningStrategy =
                    node.SuccessRateVariation <= (double)projectConfiguration.SBMaxSuccessRateVariation
                    && (!projectConfiguration.IsProgressiveness || node.ProgressivenessVariation <= (double)projectConfiguration.MaxProgressivenessVariation);
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderService>(ex);
                throw;
            }
        }

        public void BuildBacktestOfCrossingNode(
            StrategyNodeModel strategyNode,
            REPTreeNodeModel backtestingNode,
            IEnumerable<Candle> mainCandles,
            IEnumerable<Candle> crossingCandles,
            ProjectConfigurationDTO projectConfiguration,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            try
            {
                // OS Backtest

                ExecuteCrossingBacktest(
                    strategyNode,
                    backtestingNode,
                    backtestingNode.BacktestOS,
                    mainCandles,
                    crossingCandles,
                    projectConfiguration.FromDateOS.Value,
                    projectConfiguration.ToDateOS.Value,
                    projectConfiguration.TimeframeId,
                    manualResetEvent,
                    cancellationToken);

                // IS Backtest

                ExecuteCrossingBacktest(
                    strategyNode,
                    backtestingNode,
                    backtestingNode.BacktestIS,
                    mainCandles,
                    crossingCandles,
                    projectConfiguration.FromDateIS.Value,
                    projectConfiguration.ToDateIS.Value,
                    projectConfiguration.TimeframeId,
                    manualResetEvent,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderService>(ex);
                throw;
            }
        }

        private void ExecuteCrossingBacktest(
            StrategyNodeModel strategyNode,
            REPTreeNodeModel backtestingNode,
            BacktestModel backtest,
            IEnumerable<Candle> mainCandles,
            IEnumerable<Candle> crossingCandles,
            DateTime fromDate,
            DateTime toDate,
            int timeframeId,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {

            var mainCandlesRange = (from candle in mainCandles
                                    let dt = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time)
                                    where dt >= fromDate && dt <= toDate
                                    select candle).ToList();

            var crossingCandlesRange = (from candle in crossingCandles
                                        let dt = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time)
                                        where dt >= fromDate && dt <= toDate
                                        select candle).ToList();

            backtest.FromDate = fromDate;
            backtest.ToDate = toDate;
            backtest.TimeframeId = timeframeId;

            backtest.BacktestOperations = new List<BacktestOperationModel>();
            backtest.TotalOpportunity = crossingCandlesRange.Count - 1;

            for (var candleIdx = 0; candleIdx < crossingCandlesRange.Count - 1; candleIdx++)
            {
                //manualResetEvent.Wait();
                //cancellationToken.ThrowIfCancellationRequested();

                var firstCandle = crossingCandlesRange[0];
                var nextCandle = crossingCandlesRange[candleIdx + 1];
                var currentMainCandle = new Candle
                {
                    Date = mainCandlesRange[candleIdx].Date,
                    Time = mainCandlesRange[candleIdx].Time,

                    Open = mainCandlesRange[candleIdx].Open,
                    High = mainCandlesRange[candleIdx].Open,
                    Low = mainCandlesRange[candleIdx].Open,
                    Close = mainCandlesRange[candleIdx].Open,

                    Spread = mainCandlesRange[candleIdx].Spread
                };

                // Has to approve the parent backtestingNode of the main backtestingNode           
                var mainNodeIndicators = _extractorService.BuildIndicatorsFromNode(strategyNode.MainNode.ParentNode.Node).ToList();
                if (ApproveCandle(mainNodeIndicators, candleIdx, mainCandlesRange, currentMainCandle, mainCandlesRange))
                {
                    // Has to approve one of the child nodes from the main backtestingNode
                    var childNodePass = false;
                    foreach (var childNode in strategyNode.MainNode.ChildNodes)
                    {
                        var childNodeIndicators = _extractorService.BuildIndicatorsFromNode(childNode.Node);
                        if (ApproveCandle(childNodeIndicators, candleIdx, firstCandle, mainCandlesRange, mainCandlesRange))
                        {
                            childNodePass = true;
                            break;
                        }
                    }

                    // DEBUG BACKTEST OF CROSSING NODE
                    // FIX PROBLEM WITH DIFFERENT HISTORICAL DATA CANDLES FOR EACH NODE
                    // MULTITHREADING WITH SYNCHRONIZATION, LOCKS, SEMAPHORES, ETC ??

                    if (!childNodePass)
                    {
                        // No trade signal, move to the next candle
                        continue;
                    }

                    // Has to approve the node being backtested
                    var backtestingNodeIndicators = _extractorService.BuildIndicatorsFromNode(backtestingNode.Node).ToList();
                    if (!ApproveCandle(backtestingNodeIndicators, candleIdx, firstCandle, currentCandle, crossingCandlesRange))
                    {
                        continue;
                    }

                    // Has to approve all of the crossing nodes
                    var crossingNodePass = true;
                    foreach (var crossingNode in strategyNode.CrossingNodes)
                    {
                        var candlesRange = (from candle in crossingNode.Item2
                                            let dt = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time)
                                            where dt >= fromDate && dt <= toDate
                                            select candle).ToList();

                        var crossingNodeIndicators = _extractorService.BuildIndicatorsFromNode(crossingNode.Item1.Node).ToList();
                        if (!ApproveCandle(crossingNodeIndicators, candleIdx, firstCandle, currentCandle, candlesRange))
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

                    var isWinnerTrade = false;
                    var spread = currentCandle.Spread * 0.00001;

                    isWinnerTrade = strategyNode.MainNode.ParentNode.Label.ToLowerInvariant() == "up"
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
        }

        public void ExecuteBacktest(
            BacktestModel backtest,
            EntityTypeEnum entityType,
            DateTime fromDate,
            DateTime toDate,
            int timeframeId,
            IEnumerable<Candle> candles,
            REPTreeNodeModel parentNode,
            IList<REPTreeNodeModel> childNodes,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            var nodeIndicators = _extractorService.BuildIndicatorsFromNode(parentNode.Node);

            var candlesRange = (from candle in candles
                                let dt = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time)
                                where dt >= fromDate && dt <= toDate
                                select candle).ToList();


            backtest.FromDate = fromDate;
            backtest.ToDate = toDate;
            backtest.TimeframeId = timeframeId;

            backtest.BacktestOperations = new List<BacktestOperationModel>();
            backtest.TotalOpportunity = candlesRange.Count - 1;

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
                    if (entityType == EntityTypeEnum.AssemblyBuilder)
                    {
                        var childNodePass = false;
                        foreach (var childNode in childNodes)
                        {
                            var childNodeIndicators = _extractorService.BuildIndicatorsFromNode(childNode.Node);
                            if (ApproveCandle(childNodeIndicators, candleIdx, firstCandle, currentCandle, candlesRange))
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

        // Serialization

        public static void SerializeNode(EntityTypeEnum entityType, string projectName, REPTreeNodeModel node)
        {
            string directory;

            switch (entityType)
            {
                case EntityTypeEnum.StrategyBuilder:
                    directory = node.Label.ToLower() == "up"
                        ? projectName.ProjectStrategyBuilderNodesUPDirectory()
                        : projectName.ProjectStrategyBuilderNodesDOWNDirectory();
                    break;

                case EntityTypeEnum.AssemblyBuilder:
                    directory = node.Label.ToLower() == "up"
                        ? projectName.ProjectAssemblyBuilderNodesUPDirectory()
                        : projectName.ProjectAssemblyBuilderNodesDOWNDirectory();
                    break;

                default:
                    return;
            }

            var filename = RegexHelper.GetValidFileName(node.Name, "_") + ".xml";

            SerializerHelper.XMLSerializeObject(node, string.Format(@"{0}\{1}", directory, filename));
        }
    }
}
