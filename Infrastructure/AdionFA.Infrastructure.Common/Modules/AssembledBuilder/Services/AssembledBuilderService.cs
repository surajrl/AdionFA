using AdionFA.Infrastructure.Common.AssembledBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.Directories.Contracts;
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

namespace AdionFA.Infrastructure.Common.AssembledBuilder.Services
{
    public class AssembledBuilderService : IAssembledBuilderService
    {
        // Services

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IStrategyBuilderService _strategyBuilderService;

        public AssembledBuilderService()
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _strategyBuilderService = IoC.Get<IStrategyBuilderService>();
        }

        public AssembledBuilderModel LoadAssembledBuilderNodes(string projectName)
        {
            try
            {
                var assembledBuilder = new AssembledBuilderModel();

                // Load Strategy Builder UP Backtests

                var upDirectorySB = projectName.ProjectStrategyBuilderNodesUPDirectory();
                _projectDirectoryService.GetFilesInPath(upDirectorySB, "*.xml").ToList().ForEach(file =>
                {
                    if (file.Name.Contains("BACKTEST"))
                    {
                        var backtest = SerializerHelper.XMLDeSerializeObject<BacktestModel>(file.FullName);
                        assembledBuilder.ChildBacktestsUP.Add(backtest);
                    }
                });

                // Load Strategy Builder DOWN Backtests

                var downDirectorySB = projectName.ProjectStrategyBuilderNodesDOWNDirectory();
                _projectDirectoryService.GetFilesInPath(downDirectorySB, "*.xml").ToList().ForEach(file =>
                {
                    if (file.Name.Contains("BACKTEST"))
                    {
                        var backtest = SerializerHelper.XMLDeSerializeObject<BacktestModel>(file.FullName);
                        assembledBuilder.ChildBacktestsDOWN.Add(backtest);
                    }
                });

                // Load Assembled Builder UP Nodes

                var upDirectoryAB = projectName.ProjectAssembledBuilderNodesUPDirectory();
                _projectDirectoryService.GetFilesInPath(upDirectoryAB, "*.xml").ToList().ForEach(file =>
                {
                    if (file.Name.Contains("NODE"))
                    {
                        var node = SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName);
                        assembledBuilder.WinningParentNodesUP.Add(new(node));
                    }
                });

                // Load Assembled Builder DOWN Nodes

                var downDirectoryAB = projectName.ProjectAssembledBuilderNodesDOWNDirectory();
                _projectDirectoryService.GetFilesInPath(downDirectoryAB, "*.xml").ToList().ForEach(file =>
                {
                    if (file.Name.Contains("NODE"))
                    {
                        var node = SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName);
                        assembledBuilder.WinningParentNodesDOWN.Add(new(node));
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
        public StrategyBuilderModel BuildBacktestOfNode(
            REPTreeNodeModel parentNode,
            IList<BacktestModel> childNodes,
            ProjectConfigurationDTO configuration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            var strategyBuilder = new StrategyBuilderModel();

            strategyBuilder.IS = _strategyBuilderService.ExecuteBacktest(
                EntityTypeEnum.AssembledBuilder,
                configuration.FromDateIS.Value,
                configuration.ToDateIS.Value,
                configuration.TimeframeId,
                candles,
                parentNode,
                childNodes,
                manualResetEvent,
                cancellationToken);

            // Update the node with backtest information of IS
            parentNode.WinningTradesIs = strategyBuilder.IS.WinningTrades;
            parentNode.LosingTradesIs = strategyBuilder.IS.LosingTrades;
            parentNode.TotalTradesIs = strategyBuilder.IS.TotalTrades;
            parentNode.SuccessRatePercentIs = strategyBuilder.IS.SuccessRatePercent;
            parentNode.TotalOpportunityIs = strategyBuilder.IS.TotalOpportunity;
            parentNode.ProgressivenessIs = strategyBuilder.IS.Progressiveness;

            var meanPercentSuccessIS = childNodes
                .Select(backtest => backtest.SuccessRatePercent)
                .Sum() / childNodes.Count;

            var winningStrategyIS =
                (strategyBuilder.IS.SuccessRatePercent - meanPercentSuccessIS) >= (double)configuration.ABMinImprovePercent
                && strategyBuilder.IS.TotalTrades >= configuration.ABTransactionsTarget;

            if (!winningStrategyIS)
            {
                return strategyBuilder;
            }

            strategyBuilder.OS = _strategyBuilderService.ExecuteBacktest(
                EntityTypeEnum.AssembledBuilder,
                configuration.FromDateOS.Value,
                configuration.ToDateOS.Value,
                configuration.TimeframeId,
                candles,
                parentNode,
                childNodes,
                manualResetEvent,
                cancellationToken);

            // Update the node with backtest information of OS
            parentNode.WinningTradesOs = strategyBuilder.OS.WinningTrades;
            parentNode.LosingTradesOs = strategyBuilder.OS.LosingTrades;
            parentNode.TotalTradesOs = strategyBuilder.OS.TotalTrades;
            parentNode.SuccessRatePercentOs = strategyBuilder.OS.SuccessRatePercent;
            parentNode.TotalOpportunityOs = strategyBuilder.OS.TotalOpportunity;
            parentNode.ProgressivenessOs = strategyBuilder.OS.Progressiveness;

            parentNode.SuccessRateVariation = Math.Abs(parentNode.SuccessRatePercentIs - parentNode.SuccessRatePercentOs);
            parentNode.Progressiveness = Math.Abs(parentNode.ProgressivenessIs - parentNode.ProgressivenessOs);

            strategyBuilder.WinningStrategy =
                        strategyBuilder.SuccessRateVariation <= (double)configuration.SBMaxSuccessRateVariation
                        && (!configuration.IsProgressiveness || strategyBuilder.ProgressivenessVariation <= (double)configuration.MaxProgressivenessVariation);

            return strategyBuilder;
        }
    }
}
