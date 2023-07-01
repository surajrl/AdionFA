using AdionFA.Infrastructure.Common.AssemblyBuilder.Model;
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
            decimal maxCorrelation)
        {
            string directory;
            IList<REPTreeNodeModel> winningNodes;

            FindCorrelation("up");
            FindCorrelation("down");

            void FindCorrelation(string label)
            {
                switch (label.ToLowerInvariant())
                {
                    case "up":
                        directory = projectName.ProjectStrategyBuilderNodesUPDirectory();
                        winningNodes = strategyBuilder.WinningNodesUP;
                        break;

                    case "down":
                        directory = projectName.ProjectStrategyBuilderNodesDOWNDirectory();
                        winningNodes = strategyBuilder.WinningNodesDOWN;
                        break;

                    default:
                        return;
                }

                var backtests = new List<BacktestModel>();

                _projectDirectoryService.GetFilesInPath(directory, "*.xml").ToList().ForEach(file =>
                {
                    var node = SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName);

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
                    var indexOf = IndexOfCorrelation(backtests, assemblyNode.ParentNode.BacktestIS, maxCorrelation);
                    assemblyNode.ParentNode.BacktestIS.CorrelationPass = indexOf != null;

                    if (assemblyNode.ParentNode.BacktestIS.CorrelationPass)
                    {
                        winningAssemblyNodes.Add(assemblyNode);

                        if (indexOf >= 0)
                        {
                            backtests.Insert(indexOf.Value, assemblyNode.ParentNode.BacktestIS);
                        }
                        if (indexOf == -1)
                        {
                            backtests.Add(assemblyNode.ParentNode.BacktestIS);
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
            REPTreeNodeModel node,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
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

                // OS Winning Conditions

                node.WinningStrategy =
                    node.BacktestOS.WinningTrades >= projectConfiguration.SBMinTransactionsOS
                    && node.BacktestOS.SuccessRatePercent >= (double)projectConfiguration.SBMinSuccessRatePercentOS;

                if (!node.WinningStrategy)
                {
                    return false;
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

                // IS Winning Conditions

                node.WinningStrategy =
                     node.BacktestIS.WinningTrades >= projectConfiguration.SBMinTransactionsIS
                     && node.BacktestIS.SuccessRatePercent >= (double)projectConfiguration.SBMinSuccessRatePercentIS;

                // Winning Conditions

                node.WinningStrategy =
                    node.SuccessRateVariation <= (double)projectConfiguration.SBMaxSuccessRateVariation
                    && (!projectConfiguration.IsProgressiveness || node.ProgressivenessVariation <= (double)projectConfiguration.MaxProgressivenessVariation);

                return node.WinningStrategy;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderService>(ex);
                throw;
            }
        }

        public bool BuildBacktestOfAssemblyNode(
            REPTreeNodeModel backtestingAssemblyNode,
            IList<REPTreeNodeModel> childNodes,
            ProjectConfigurationDTO projectConfiguration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            // IS Backtest

            ExecuteBacktest(
                backtestingAssemblyNode.BacktestIS,
                EntityTypeEnum.AssemblyBuilder,
                projectConfiguration.FromDateIS.Value,
                projectConfiguration.ToDateIS.Value,
                projectConfiguration.TimeframeId,
                candles,
                backtestingAssemblyNode,
                childNodes,
                manualResetEvent,
                cancellationToken);

            // IS Winning Conditions

            var meanSuccessRatePercentIS = childNodes
                .Select(node => node.BacktestIS.SuccessRatePercent)
                .Sum() / childNodes.Count;

            backtestingAssemblyNode.WinningStrategy =
                (backtestingAssemblyNode.BacktestIS.SuccessRatePercent - meanSuccessRatePercentIS) >= (double)projectConfiguration.ABMinImprovePercent
                && backtestingAssemblyNode.BacktestIS.TotalTrades >= projectConfiguration.ABTransactionsTarget;

            if (!backtestingAssemblyNode.WinningStrategy)
            {
                return false;
            }

            // OS Backtest

            ExecuteBacktest(
                backtestingAssemblyNode.BacktestOS,
                EntityTypeEnum.AssemblyBuilder,
                projectConfiguration.FromDateOS.Value,
                projectConfiguration.ToDateOS.Value,
                projectConfiguration.TimeframeId,
                candles,
                backtestingAssemblyNode,
                childNodes,
                manualResetEvent,
                cancellationToken);

            // Winning Conditions

            backtestingAssemblyNode.WinningStrategy =
                backtestingAssemblyNode.SuccessRateVariation <= (double)projectConfiguration.SBMaxSuccessRateVariation
                && (!projectConfiguration.IsProgressiveness || backtestingAssemblyNode.ProgressivenessVariation <= (double)projectConfiguration.MaxProgressivenessVariation);

            return backtestingAssemblyNode.WinningStrategy;
        }

        public bool BuildBacktestOfCrossingNode(
            StrategyNodeModel mainNode,
            REPTreeNodeModel backtestingCrossingNode,
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
                    mainNode,
                    backtestingCrossingNode,
                    backtestingCrossingNode.BacktestOS,
                    mainCandles,
                    crossingCandles,
                    projectConfiguration.FromDateOS.Value,
                    projectConfiguration.ToDateOS.Value,
                    projectConfiguration.TimeframeId,
                    manualResetEvent,
                    cancellationToken);

                // IS Backtest

                ExecuteCrossingBacktest(
                    mainNode,
                    backtestingCrossingNode,
                    backtestingCrossingNode.BacktestIS,
                    mainCandles,
                    crossingCandles,
                    projectConfiguration.FromDateIS.Value,
                    projectConfiguration.ToDateIS.Value,
                    projectConfiguration.TimeframeId,
                    manualResetEvent,
                    cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderService>(ex);
                throw;
            }
        }

        private void ExecuteCrossingBacktest(
            StrategyNodeModel mainNode,
            REPTreeNodeModel backtestingCrossingNode,
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

            // mainCandlesRange and crossingCandlesRange should both have the same number of candles

            backtest.FromDate = fromDate;
            backtest.ToDate = toDate;
            backtest.TimeframeId = timeframeId;

            backtest.BacktestOperations = new List<BacktestOperationModel>();
            backtest.TotalOpportunity = mainCandlesRange.Count - 1;

            var mainFirstCandle = mainCandlesRange[0];
            var crossingFirstCandle = crossingCandlesRange[0];

            var mainNodeIndicators = _extractorService.BuildIndicatorsFromNode(mainNode.MainNode.ParentNode.Node).ToList();
            var backtestingNodeIndicators = _extractorService.BuildIndicatorsFromNode(backtestingCrossingNode.Node).ToList();

            for (var idx = 0; idx < mainCandlesRange.Count - 1; idx++)
            {
                //manualResetEvent.Wait();
                //cancellationToken.ThrowIfCancellationRequested();

                var currentMainCandle = new Candle
                {
                    Date = mainCandlesRange[idx].Date,
                    Time = mainCandlesRange[idx].Time,

                    Open = mainCandlesRange[idx].Open,
                    High = mainCandlesRange[idx].Open,
                    Low = mainCandlesRange[idx].Open,
                    Close = mainCandlesRange[idx].Open,

                    Spread = mainCandlesRange[idx].Spread
                };

                // Has to approve the parent assemblyNode of the main assemblyNode           
                if (ApproveCandle(mainNodeIndicators, idx, mainFirstCandle, currentMainCandle, mainCandlesRange))
                {
                    // Has to approve one of the child winningNodes from the main Node
                    var childNodePass = false;
                    foreach (var childNode in mainNode.MainNode.ChildNodes)
                    {
                        var childNodeIndicators = _extractorService.BuildIndicatorsFromNode(childNode.Node);
                        if (ApproveCandle(childNodeIndicators, idx, mainFirstCandle, currentMainCandle, mainCandlesRange))
                        {
                            childNodePass = true;
                            break;
                        }
                    }

                    if (!childNodePass)
                    {
                        // No trade signal, move to the next candle
                        continue;
                    }

                    // Has to approve the assemblyNode being backtested
                    var currentCrossingCandle = new Candle
                    {
                        Date = crossingCandlesRange[idx].Date,
                        Time = crossingCandlesRange[idx].Time,

                        Open = crossingCandlesRange[idx].Open,
                        High = crossingCandlesRange[idx].Open,
                        Low = crossingCandlesRange[idx].Open,
                        Close = crossingCandlesRange[idx].Open,

                        Spread = crossingCandlesRange[idx].Spread
                    };
                    if (!ApproveCandle(backtestingNodeIndicators, idx, crossingFirstCandle, currentCrossingCandle, crossingCandlesRange))
                    {
                        continue;
                    }

                    // Has to approve all of the crossing winningNodes
                    var crossingNodePass = true;
                    foreach (var crossingNode in mainNode.CrossingNodes)
                    {
                        var candlesRange = (from candle in crossingNode.Item2
                                            let dt = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time)
                                            where dt >= fromDate && dt <= toDate
                                            select candle).ToList();

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

                        var crossingNodeIndicators = _extractorService.BuildIndicatorsFromNode(crossingNode.Item1.Node).ToList();
                        if (!ApproveCandle(crossingNodeIndicators, idx, candlesRange[0], currentCandle, candlesRange))
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
                    var spread = currentMainCandle.Spread * 0.00001;

                    isWinnerTrade = mainNode.MainNode.ParentNode.Label.ToLowerInvariant() == "up"
                        ? (currentMainCandle.Open + spread) < mainCandlesRange[idx + 1].Open
                        : currentMainCandle.Open > (mainCandlesRange[idx + 1].Open + spread);

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
                        Date = DateTimeHelper.BuildDateTime(timeframeId, currentMainCandle.Date, currentMainCandle.Time),
                        IsWinner = isWinnerTrade
                    });
                }
            }
        }

        private void ExecuteBacktest(
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
                            // Move to the next candle if none of the child winningNodes is a pass
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
