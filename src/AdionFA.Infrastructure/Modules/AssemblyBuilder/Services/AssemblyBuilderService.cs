using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
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

            // Get the child nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.NodeBuilderNodesUP.GetDescription()), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    assemblyBuilder.ChildNodesUP.Add(SerializerHelper.XMLDeSerializeObject<SingleNodeModel>(file.FullName));
                });

            // Get the child nodes DOWN
            _projectDirectoryService.GetFilesInPath(projectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.NodeBuilderNodesDOWN.GetDescription()), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    assemblyBuilder.ChildNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<SingleNodeModel>(file.FullName));
                });

            return assemblyBuilder;
        }

        public AssemblyBuilderModel GetExistingAssemblyBuilder(string projectName)
        {
            var assemblyBuilder = new AssemblyBuilderModel();

            // Get the assembly nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.AssemblyBuilderNodesUP.GetDescription()), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    assemblyBuilder.AllWinningAssemblyNodes.Add(SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName));
                });

            // Get the assembly nodes DOWN
            _projectDirectoryService.GetFilesInPath(projectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.AssemblyBuilderNodesDOWN.GetDescription()), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    assemblyBuilder.AllWinningAssemblyNodes.Add(SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName));
                });

            // Get the child nodes UP
            _projectDirectoryService.GetFilesInPath(projectName.ProjectNodesUPDirectory(ProjectDirectoryEnum.NodeBuilderNodesUP.GetDescription()), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    assemblyBuilder.ChildNodesUP.Add(SerializerHelper.XMLDeSerializeObject<SingleNodeModel>(file.FullName));
                });

            // Get the child nodes DOWN
            _projectDirectoryService.GetFilesInPath(projectName.ProjectNodesDOWNDirectory(ProjectDirectoryEnum.NodeBuilderNodesDOWN.GetDescription()), "*.xml")
                .ToList()
                .ForEach(file =>
                {
                    assemblyBuilder.ChildNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<SingleNodeModel>(file.FullName));
                });

            return assemblyBuilder;
        }
    }
}
