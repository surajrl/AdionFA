using AdionFA.Infrastructure.Common.AssemblyBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssemblyBuilder.Model;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.Weka.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Common.AssemblyBuilder.Services
{
    public class AssemblyBuilderService : IAssemblyBuilderService
    {
        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IStrategyBuilderService _strategyBuilderService;

        public AssemblyBuilderService()
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _strategyBuilderService = IoC.Get<IStrategyBuilderService>();
        }

        public AssemblyBuilderModel LoadAssemblyBuilder(string projectName)
        {
            try
            {
                var assembledBuilder = new AssemblyBuilderModel();

                LoadStrategyBuilderCorrelationNodes("up");
                LoadStrategyBuilderCorrelationNodes("down");

                LoadAssemblyNodes("up");
                LoadAssemblyNodes("down");

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

                void LoadAssemblyNodes(string label)
                {
                    var directoryAB = string.Empty;
                    var assembledNodes = new List<AssemblyNodeModel>();

                    switch (label.ToLowerInvariant())
                    {
                        case "up":
                            directoryAB = projectName.ProjectAssemblyBuilderNodesUPDirectory();
                            assembledNodes = assembledBuilder.WinningAssemblyNodesUP;
                            break;

                        case "down":
                            directoryAB = projectName.ProjectAssemblyBuilderNodesDOWNDirectory();
                            assembledNodes = assembledBuilder.WinningAssemblyNodesDOWN;
                            break;

                        default:
                            return;
                    }

                    _projectDirectoryService.GetFilesInPath(directoryAB, "*.xml").ToList().ForEach(file =>
                    {
                        assembledNodes.Add(SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName));
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<AssemblyBuilderService>(ex);
                throw;
            }
        }

        // Serialization

        public static void SerializeAssemblyNode(string projectName, AssemblyNodeModel assemblyNode)
        {
            var directory = assemblyNode.ParentNode.Label.ToLower() == "up"
                ? projectName.ProjectAssemblyBuilderNodesUPDirectory()
                : projectName.ProjectAssemblyBuilderNodesDOWNDirectory();

            var filename = RegexHelper.GetValidFileName(assemblyNode.ParentNode.Name, "_") + ".xml";
            SerializerHelper.XMLSerializeObject(assemblyNode, string.Format(@"{0}\{1}", directory, filename));
        }
    }
}
