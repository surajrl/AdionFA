using AdionFA.Infrastructure.Common.AssemblyBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssemblyBuilder.Model;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.Modules.Weka.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using System.Linq;

namespace AdionFA.Infrastructure.Common.AssemblyBuilder.Services
{
    public class AssemblyBuilderService : IAssemblyBuilderService
    {
        private readonly IProjectDirectoryService _projectDirectoryService;

        public AssemblyBuilderService()
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
        }

        public void LoadNewAssemblyBuilder(string projectName, AssemblyBuilderModel assemblyBuilder)
        {
            assemblyBuilder.WinningAssemblyNodesUP.Clear();
            assemblyBuilder.WinningAssemblyNodesDOWN.Clear();
            assemblyBuilder.ChildNodesUP.Clear();
            assemblyBuilder.ChildNodesDOWN.Clear();

            // Get the Child Nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectStrategyBuilderNodesUPDirectory(), "*.xml").ToList().ForEach(file =>
            {
                assemblyBuilder.ChildNodesUP.Add(SerializerHelper.XMLDeSerializeObject<NodeModel>(file.FullName));
            });
            // Get the Child Nodes DOWN
            _projectDirectoryService.GetFilesInPath(projectName.ProjectStrategyBuilderNodesDOWNDirectory(), "*.xml").ToList().ForEach(file =>
            {
                assemblyBuilder.ChildNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<NodeModel>(file.FullName));
            });
        }

        public void LoadExistingAssemblyBuilder(string projectName, AssemblyBuilderModel assemblyBuilder)
        {
            assemblyBuilder.WinningAssemblyNodesUP.Clear();
            assemblyBuilder.WinningAssemblyNodesDOWN.Clear();
            assemblyBuilder.ChildNodesUP.Clear();
            assemblyBuilder.ChildNodesDOWN.Clear();

            // Get the Assembly Nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectAssemblyBuilderNodesUPDirectory(), "*.xml").ToList().ForEach(file =>
            {
                assemblyBuilder.WinningAssemblyNodesUP.Add(SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName));
            });
            // Get the Assembly Nodes DOWN
            _projectDirectoryService.GetFilesInPath(projectName.ProjectAssemblyBuilderNodesDOWNDirectory(), "*.xml").ToList().ForEach(file =>
            {
                assemblyBuilder.WinningAssemblyNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName));
            });

            // Get the Child Nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectStrategyBuilderNodesUPDirectory(), "*.xml").ToList().ForEach(file =>
            {
                assemblyBuilder.ChildNodesUP.Add(SerializerHelper.XMLDeSerializeObject<NodeModel>(file.FullName));
            });
            // Get the Child Nodes DOWN
            _projectDirectoryService.GetFilesInPath(projectName.ProjectStrategyBuilderNodesDOWNDirectory(), "*.xml").ToList().ForEach(file =>
            {
                assemblyBuilder.ChildNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<NodeModel>(file.FullName));
            });
        }
    }
}
