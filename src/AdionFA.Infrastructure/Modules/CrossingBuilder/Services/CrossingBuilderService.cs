using AdionFA.Infrastructure.CrossingBuilder.Contracts;
using AdionFA.Infrastructure.CrossingBuilder.Model;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.Modules.Strategy;
using Ninject;
using System.Linq;

namespace AdionFA.Infrastructure.CrossingBuilder.Services
{
    public class CrossingBuilderService : ICrossingBuilderService
    {
        private readonly IProjectDirectoryService _projectDirectoryService;

        public CrossingBuilderService()
        {
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
        }

        public CrossingBuilderModel CreateNewCrossingBuilder(string projectName)
        {
            var crossingBuilder = new CrossingBuilderModel();

            // Get Assembly Nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectAssemblyBuilderNodesUPDirectory(), "*.xml")
                .ToList()
                .ForEach(file =>
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
            _projectDirectoryService.GetFilesInPath(projectName.ProjectAssemblyBuilderNodesDOWNDirectory(), "*.xml")
                .ToList()
                .ForEach(file =>
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

            return crossingBuilder;
        }

        public CrossingBuilderModel GetExistingCrossingBuilder(string projectName)
        {
            var crossingBuilder = new CrossingBuilderModel();

            // Get Strategy Nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectCrossingBuilderNodesUPDirectory(), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    var strategyNode = SerializerHelper.XMLDeSerializeObject<StrategyNodeModel>(file.FullName);
                    crossingBuilder.WinningStrategyNodesUP.Add(strategyNode);
                });

            // Get Strategy Nodes DOWN
            _projectDirectoryService.GetFilesInPath(projectName.ProjectCrossingBuilderNodesDOWNDirectory(), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    var strategyNode = SerializerHelper.XMLDeSerializeObject<StrategyNodeModel>(file.FullName);
                    crossingBuilder.WinningStrategyNodesDOWN.Add(strategyNode);
                });

            return crossingBuilder;
        }
    }
}
