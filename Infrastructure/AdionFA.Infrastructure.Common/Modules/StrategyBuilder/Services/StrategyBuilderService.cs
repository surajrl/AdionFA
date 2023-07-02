using AdionFA.Core.API.Contracts.MarketData;
using AdionFA.Infrastructure.Common.AssemblyBuilder.Model;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.Modules.Weka.Model;
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
        private readonly IMarketDataAPI _marketDataService;

        public StrategyBuilderService()
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _extractorService = IoC.Get<IExtractorService>();
            _marketDataService = IoC.Get<IMarketDataAPI>();
        }

        // Correlation

        public void Correlation(
            string projectName,
            StrategyBuilderModel strategyBuilder,
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
            ProjectConfigurationDTO projectConfiguration,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            try
            {
                // Backtest OS

                node.BacktestOS = Backtest(
                    EntityTypeEnum.StrategyBuilder,
                    projectConfiguration.FromDateOS.Value,
                    projectConfiguration.ToDateOS.Value,
                    projectConfiguration.TimeframeId,
                    node.NodeData,
                    null,
                    candles,
                    manualResetEvent,
                    cancellationToken);

                // Backtest OS Pass Conditions

                node.WinningStrategy =
                    node.BacktestOS.WinningTrades >= projectConfiguration.SBMinTransactionsOS
                    && node.BacktestOS.SuccessRatePercent >= (double)projectConfiguration.SBMinSuccessRatePercentOS;

                if (!node.WinningStrategy)
                {
                    return false;
                }

                // Backtest IS

                node.BacktestIS = Backtest(
                    EntityTypeEnum.StrategyBuilder,
                    projectConfiguration.FromDateIS.Value,
                    projectConfiguration.ToDateIS.Value,
                    projectConfiguration.TimeframeId,
                    node.NodeData,
                    null,
                    candles,
                    manualResetEvent,
                    cancellationToken);

                // Backtest IS Pass Conditions

                node.WinningStrategy =
                     node.BacktestIS.WinningTrades >= projectConfiguration.SBMinTransactionsIS
                     && node.BacktestIS.SuccessRatePercent >= (double)projectConfiguration.SBMinSuccessRatePercentIS;

                if (!node.WinningStrategy)
                {
                    return false;
                }

                // Winning Strategy Conditions

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
            AssemblyNodeModel assemblyNode,
            IEnumerable<Candle> candles,
            ProjectConfigurationDTO projectConfiguration,
            double meanSuccessRatePercentIS,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            // IS Backtest

            assemblyNode.BacktestIS = Backtest(
                EntityTypeEnum.AssemblyBuilder,
                projectConfiguration.FromDateIS.Value,
                projectConfiguration.ToDateIS.Value,
                projectConfiguration.TimeframeId,
                assemblyNode.ParentNodeData,
                assemblyNode.ChildNodesData,
                candles,
                manualResetEvent,
                cancellationToken);

            // IS Winning Conditions

            assemblyNode.WinningStrategy =
                (assemblyNode.BacktestIS.SuccessRatePercent - meanSuccessRatePercentIS) >= (double)projectConfiguration.ABMinImprovePercent
                && assemblyNode.BacktestIS.TotalTrades >= projectConfiguration.ABTransactionsTarget;

            if (!assemblyNode.WinningStrategy)
            {
                return false;
            }

            // OS Backtest

            assemblyNode.BacktestOS = Backtest(
                EntityTypeEnum.AssemblyBuilder,
                projectConfiguration.FromDateOS.Value,
                projectConfiguration.ToDateOS.Value,
                projectConfiguration.TimeframeId,
                assemblyNode.ParentNodeData,
                assemblyNode.ChildNodesData,
                candles,
                manualResetEvent,
                cancellationToken);

            // Winning Conditions

            assemblyNode.WinningStrategy =
                assemblyNode.SuccessRateVariation <= (double)projectConfiguration.SBMaxSuccessRateVariation
                && (!projectConfiguration.IsProgressiveness || assemblyNode.ProgressivenessVariation <= (double)projectConfiguration.MaxProgressivenessVariation);

            return assemblyNode.WinningStrategy;
        }

        public bool BuildBacktestOfStrategyNode(
            StrategyNodeModel strategyNode,
            IEnumerable<Candle> mainCandles,
            ProjectConfigurationDTO projectConfiguration,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            try
            {
                // OS Backtest

                strategyNode.BacktestOS = BacktestStrategyNode(
                    strategyNode,
                    mainCandles,
                    projectConfiguration.FromDateOS.Value,
                    projectConfiguration.ToDateOS.Value,
                    projectConfiguration.TimeframeId,
                    manualResetEvent,
                    cancellationToken);

                if (strategyNode.BacktestOS.WinningTrades == 0) { return false; }

                // IS Backtest

                strategyNode.BacktestIS = BacktestStrategyNode(
                    strategyNode,
                    mainCandles,
                    projectConfiguration.FromDateIS.Value,
                    projectConfiguration.ToDateIS.Value,
                    projectConfiguration.TimeframeId,
                    manualResetEvent,
                    cancellationToken);

                if (strategyNode.BacktestIS.WinningTrades == 0) { return false; }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderService>(ex);
                throw;
            }
        }

        private BacktestModel BacktestStrategyNode(
            StrategyNodeModel strategyNode,
            IEnumerable<Candle> mainCandles,
            DateTime fromDate,
            DateTime toDate,
            int timeframeId,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            var candlesRange = (from candle in mainCandles
                                let dt = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time)
                                where dt >= fromDate && dt <= toDate
                                select candle).ToList();

            var crossingCandles = new List<List<Candle>>();

            foreach (var crossingNode in strategyNode.CrossingNodesData)
            {
                var candles = _marketDataService
                            .GetHistoricalData(crossingNode.Item2, true)
                            .HistoricalDataCandles.Select(candle => new Candle
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
                            .OrderBy(d => d.Date)
                            .ThenBy(d => d.Time);

                var range = (from candle in candles
                             let dt = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time)
                             where dt >= fromDate && dt <= toDate
                             select candle).ToList();
                crossingCandles.Add(range);
            }

            var backtest = new BacktestModel
            {
                FromDate = fromDate,
                ToDate = toDate,
                TimeframeId = timeframeId,
                BacktestOperations = new List<BacktestOperationModel>()
            };

            var mainFirstCandle = candlesRange[0];
            var parentNodeIndicators = _extractorService.BuildIndicatorsFromNode(strategyNode.ParentNodeData.Node).ToList();

            for (var idx = 0; idx < candlesRange.Count - 1; idx++)
            {
                backtest.TotalOpportunity++;

                manualResetEvent.Wait(cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();

                var currentMainCandle = new Candle
                {
                    Date = candlesRange[idx].Date,
                    Time = candlesRange[idx].Time,

                    Open = candlesRange[idx].Open,
                    High = candlesRange[idx].Open,
                    Low = candlesRange[idx].Open,
                    Close = candlesRange[idx].Open,

                    Spread = candlesRange[idx].Spread
                };

                // Has to approve the parent node         
                if (ApproveCandle(parentNodeIndicators, idx, mainFirstCandle, currentMainCandle, candlesRange))
                {
                    // Has to approve one of the child nodes
                    var childNodePass = false;
                    foreach (var childNode in strategyNode.ChildNodesData)
                    {
                        var childNodeIndicators = _extractorService.BuildIndicatorsFromNode(childNode.Node);
                        if (ApproveCandle(childNodeIndicators, idx, mainFirstCandle, currentMainCandle, candlesRange))
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

                    // Has to approve all of the crossing nodes
                    var crossingNodePass = true;
                    for (var i = 0; i < strategyNode.CrossingNodesData.Count; i++)
                    {
                        var c = crossingCandles[i];

                        var currentCandle = new Candle
                        {
                            Date = c[idx].Date,
                            Time = c[idx].Time,

                            Open = c[idx].Open,
                            High = c[idx].Open,
                            Low = c[idx].Open,
                            Close = c[idx].Open,

                            Spread = c[idx].Spread
                        };

                        var crossingNodeIndicators = _extractorService.BuildIndicatorsFromNode(strategyNode.CrossingNodesData[i].Item1.Node).ToList();
                        if (!ApproveCandle(crossingNodeIndicators, idx, c[0], currentCandle, c))
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

                    isWinnerTrade = strategyNode.ParentNodeData.Label.ToLowerInvariant() == "up"
                        ? (currentMainCandle.Open + spread) < candlesRange[idx + 1].Open
                        : currentMainCandle.Open > (candlesRange[idx + 1].Open + spread);

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
                manualResetEvent.Wait(cancellationToken);
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
