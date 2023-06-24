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
        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IStrategyBuilderService _strategyBuilderService;

        public AssembledBuilderService()
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _strategyBuilderService = IoC.Get<IStrategyBuilderService>();
        }

        public AssembledBuilderModel LoadAssembledBuilder(string projectName)
        {
            try
            {
                var assembledBuilder = new AssembledBuilderModel();

                LoadStrategyBuilderCorrelationNodes("up");
                LoadStrategyBuilderCorrelationNodes("down");

                LoadAssembledNodes("up");
                LoadAssembledNodes("down");

                return assembledBuilder;

                void LoadStrategyBuilderCorrelationNodes(string label)
                {
                    string directorySB;
                    IList<REPTreeNodeModel> nodes;

                    switch (label.ToLowerInvariant())
                    {
                        case "up":
                            directorySB = projectName.ProjectStrategyBuilderNodesUPDirectory();
                            nodes = assembledBuilder.ChildNodesUP;
                            break;

                        case "down":
                            directorySB = projectName.ProjectStrategyBuilderNodesDOWNDirectory();
                            nodes = assembledBuilder.ChildNodesDOWN;
                            break;

                        default:
                            return;
                    }

                    _projectDirectoryService.GetFilesInPath(directorySB, "*.xml").ToList().ForEach(file =>
                    {
                        nodes.Add(SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName));
                    });
                }

                void LoadAssembledNodes(string label)
                {
                    var directoryAB = string.Empty;
                    var assembledNodes = new List<AssembledNodeModel>();

                    switch (label.ToLowerInvariant())
                    {
                        case "up":
                            directoryAB = projectName.ProjectAssembledBuilderNodesUPDirectory();
                            assembledNodes = assembledBuilder.AssembledNodesUP;
                            break;

                        case "down":
                            directoryAB = projectName.ProjectAssembledBuilderNodesDOWNDirectory();
                            assembledNodes = assembledBuilder.AssembledNodesDOWN;
                            break;

                        default:
                            return;
                    }

                    _projectDirectoryService.GetFilesInPath(directoryAB, "*.xml").ToList().ForEach(file =>
                    {
                        assembledNodes.Add(SerializerHelper.XMLDeSerializeObject<AssembledNodeModel>(file.FullName));
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<AssembledBuilderService>(ex);
                throw;
            }
        }

        public void BuildBacktestOfNode(
            string projectName,
            AssembledBuilderModel assembledBuilder,
            REPTreeNodeModel parentNode,
            IList<REPTreeNodeModel> childNodes,
            ProjectConfigurationDTO configuration,
            IEnumerable<Candle> candles,
            ManualResetEventSlim manualResetEvent,
            CancellationToken cancellationToken)
        {
            // IS Backtest

            _strategyBuilderService.ExecuteBacktest(
                parentNode.BacktestIS,
                EntityTypeEnum.AssembledBuilder,
                configuration.FromDateIS.Value,
                configuration.ToDateIS.Value,
                configuration.TimeframeId,
                candles,
                parentNode,
                childNodes,
                manualResetEvent,
                cancellationToken);

            var meanSuccessRatePercentIS = childNodes
                .Select(node => node.BacktestIS.SuccessRatePercent)
                .Sum() / childNodes.Count;

            parentNode.WinningStrategy =
                (parentNode.BacktestIS.SuccessRatePercent - meanSuccessRatePercentIS) >= (double)configuration.ABMinImprovePercent
                && parentNode.BacktestIS.TotalTrades >= configuration.ABTransactionsTarget;

            if (!parentNode.WinningStrategy)
            {
                return;
            }

            // OS Backtest

            _strategyBuilderService.ExecuteBacktest(
                parentNode.BacktestOS,
                EntityTypeEnum.AssembledBuilder,
                configuration.FromDateOS.Value,
                configuration.ToDateOS.Value,
                configuration.TimeframeId,
                candles,
                parentNode,
                childNodes,
                manualResetEvent,
                cancellationToken);

            parentNode.WinningStrategy =
                parentNode.SuccessRateVariation <= (double)configuration.SBMaxSuccessRateVariation
                && (!configuration.IsProgressiveness || parentNode.ProgressivenessVariation <= (double)configuration.MaxProgressivenessVariation);

            if (parentNode.WinningStrategy)
            {
                var assembledNode = new AssembledNodeModel
                {
                    ParentNode = parentNode,
                    ChildNodes = childNodes.ToList(),
                };

                switch (parentNode.Label.ToLowerInvariant())
                {
                    case "up":
                        assembledBuilder.AssembledNodesUP.Add(assembledNode);
                        break;

                    case "down":
                        assembledBuilder.AssembledNodesDOWN.Add(assembledNode);
                        break;
                }

                SerializeAssembledNode(projectName, assembledNode);
            }
        }

        public void Correlation(string projectName, AssembledBuilderModel assembledBuilder, decimal maxCorrelation)
        {
            try
            {
                string directory;
                IList<BacktestModel> backtests;
                IList<AssembledNodeModel> assembledNodes;

                FindCorrelation("up");
                FindCorrelation("down");

                void FindCorrelation(string label)
                {
                    switch (label.ToLowerInvariant())
                    {
                        case "up":
                            directory = projectName.ProjectAssembledBuilderNodesUPDirectory();
                            backtests = assembledBuilder.AssembledNodesUP.Select(assembledNode => assembledNode.ParentNode.BacktestIS).ToList();
                            assembledNodes = assembledBuilder.AssembledNodesUP;
                            break;

                        case "down":
                            directory = projectName.ProjectAssembledBuilderNodesDOWNDirectory();
                            backtests = assembledBuilder.AssembledNodesDOWN.Select(assembledNode => assembledNode.ParentNode.BacktestIS).ToList();
                            assembledNodes = assembledBuilder.AssembledNodesDOWN;
                            break;

                        default:
                            return;
                    }

                    _projectDirectoryService.GetFilesInPath(directory, "*.xml").ToList().ForEach(file =>
                    {
                        var assembledNode = SerializerHelper.XMLDeSerializeObject<AssembledNodeModel>(file.FullName);

                        // Algorithm to find correlation
                        var indexOf = IndexOfCorrelation(backtests, assembledNode.ParentNode.BacktestIS, maxCorrelation);

                        assembledNode.ParentNode.BacktestIS.CorrelationPass = indexOf != null;
                        if (assembledNode.ParentNode.BacktestIS.CorrelationPass)
                        {
                            assembledNodes.Add(assembledNode);

                            if (indexOf >= 0)
                            {
                                backtests.Insert(indexOf.Value, assembledNode.ParentNode.BacktestIS);
                            }
                            if (indexOf == -1)
                            {
                                backtests.Add(assembledNode.ParentNode.BacktestIS);
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

        // Serialization

        public static void SerializeAssembledNode(string projectName, AssembledNodeModel assembledNode)
        {
            var directory = assembledNode.ParentNode.Label.ToLower() == "up"
                ? projectName.ProjectAssembledBuilderNodesUPDirectory()
                : projectName.ProjectAssembledBuilderNodesDOWNDirectory();

            var filename = RegexHelper.GetValidFileName(assembledNode.ParentNode.Name, "_") + ".xml";
            SerializerHelper.XMLSerializeObject(assembledNode, string.Format(@"{0}\{1}", directory, filename));
        }
    }
}
