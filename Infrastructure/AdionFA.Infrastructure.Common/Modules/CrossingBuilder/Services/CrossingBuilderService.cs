using AdionFA.Infrastructure.Common.CrossingBuilder.Contracts;
using AdionFA.Infrastructure.Common.CrossingBuilder.Model;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.Weka.Model;
using System.Linq;

namespace AdionFA.Infrastructure.Common.CrossingBuilder.Services
{
    public class CrossingBuilderService : ICrossingBuilderService
    {
        private readonly IProjectDirectoryService _projectDirectoryService;

        public CrossingBuilderService()
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
        }

        public void LoadNewCrossingBuilder(string projectName, CrossingBuilderModel crossingBuilder)
        {
            crossingBuilder.WinningStrategyNodesUP.Clear();
            crossingBuilder.WinningStrategyNodesDOWN.Clear();

            // Get Assembly Nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectAssemblyBuilderNodesUPDirectory(), "*.xml").ToList().ForEach(file =>
            {
                var assemblyNode = SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName);
                crossingBuilder.WinningStrategyNodesUP.Add(new StrategyNodeModel
                {
                    ParentNodeData = assemblyNode.ParentNodeData,
                    ChildNodesData = assemblyNode.ChildNodesData,
                    CrossingNodesData = new(),
                    BacktestIS = assemblyNode.BacktestIS,
                    BacktestOS = assemblyNode.BacktestOS,
                });
            });
            // Get Assembly Nodes DOWN
            _projectDirectoryService.GetFilesInPath(projectName.ProjectAssemblyBuilderNodesDOWNDirectory(), "*.xml").ToList().ForEach(file =>
            {
                var assemblyNode = SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName);
                crossingBuilder.WinningStrategyNodesDOWN.Add(new StrategyNodeModel
                {
                    ParentNodeData = assemblyNode.ParentNodeData,
                    ChildNodesData = assemblyNode.ChildNodesData,
                    CrossingNodesData = new(),
                    BacktestIS = assemblyNode.BacktestIS,
                    BacktestOS = assemblyNode.BacktestOS
                });
            });
        }

        public void LoadExistingCrossingBuilder(string projectName, CrossingBuilderModel crossingBuilder)
        {
            crossingBuilder.WinningStrategyNodesUP.Clear();
            crossingBuilder.WinningStrategyNodesDOWN.Clear();

            // Get Strategy Nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectCrossingBuilderNodesUPDirectory(), "*.xml").ToList().ForEach(file =>
            {
                var strategyNode = SerializerHelper.XMLDeSerializeObject<StrategyNodeModel>(file.FullName);
                crossingBuilder.WinningStrategyNodesUP.Add(strategyNode);
            });
            // Get Strategy Nodes DOWN
            _projectDirectoryService.GetFilesInPath(projectName.ProjectCrossingBuilderNodesDOWNDirectory(), "*.xml").ToList().ForEach(file =>
            {
                var strategyNode = SerializerHelper.XMLDeSerializeObject<StrategyNodeModel>(file.FullName);
                crossingBuilder.WinningStrategyNodesDOWN.Add(strategyNode);
            });
        }
    }
}
