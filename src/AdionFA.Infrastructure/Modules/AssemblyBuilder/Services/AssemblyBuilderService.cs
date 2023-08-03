using AdionFA.Infrastructure.AssemblyBuilder.Contracts;
using AdionFA.Infrastructure.AssemblyBuilder.Model;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.Modules.Strategy;
using Ninject;
using System.Linq;

namespace AdionFA.Infrastructure.AssemblyBuilder.Services
{
    public class AssemblyBuilderService : IAssemblyBuilderService
    {
        private readonly IProjectDirectoryService _projectDirectoryService;

        public AssemblyBuilderService()
        {
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
        }

        public AssemblyBuilderModel CreateNewAssemblyBuilder(string projectName)
        {
            var assemblyBuilder = new AssemblyBuilderModel();

            // Get the Child Nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectNodeBuilderNodesUPDirectory(), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    assemblyBuilder.ChildNodesUP.Add(SerializerHelper.XMLDeSerializeObject<NodeModel>(file.FullName));
                });

            // Get the Child Nodes DOWN
            _projectDirectoryService.GetFilesInPath(projectName.ProjectNodeBuilderNodesDOWNDirectory(), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    assemblyBuilder.ChildNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<NodeModel>(file.FullName));
                });

            return assemblyBuilder;
        }

        public AssemblyBuilderModel GetExistingAssemblyBuilder(string projectName)
        {
            var assemblyBuilder = new AssemblyBuilderModel();

            // Get the Assembly Nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectAssemblyBuilderNodesUPDirectory(), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    assemblyBuilder.WinningAssemblyNodesUP.Add(SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName));
                });

            // Get the Assembly Nodes DOWN
            _projectDirectoryService.GetFilesInPath(projectName.ProjectAssemblyBuilderNodesDOWNDirectory(), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    assemblyBuilder.WinningAssemblyNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName));
                });

            // Get the Child Nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectNodeBuilderNodesUPDirectory(), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    assemblyBuilder.ChildNodesUP.Add(SerializerHelper.XMLDeSerializeObject<NodeModel>(file.FullName));
                });

            // Get the Child Nodes DOWN
            _projectDirectoryService.GetFilesInPath(projectName.ProjectNodeBuilderNodesDOWNDirectory(), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    assemblyBuilder.ChildNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<NodeModel>(file.FullName));
                });

            return assemblyBuilder;
        }
    }
}
