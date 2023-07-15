using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.Helpers;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.Modules.Weka.Model;
using AdionFA.Infrastructure.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Services;
using Ninject;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class SavedNodesFlyoutViewModel : ViewModelBase
    {
        private readonly IProjectDirectoryService _projectDirectoryService;

        public SavedNodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
            applicationCommands.DeleteNodeCommand.RegisterCommand(DeleteNodeCommand);

            SavedNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyout =>
        {
            if ((flyout?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleSavedNodes, StringComparison.Ordinal))
            {
                SavedNodes.Clear();

                _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDirectory(), "*.xml").ToList()
                .ForEach(file =>
                {
                    SavedNodes.Add(SerializerHelper.XMLDeSerializeObject<NodeModel>(file.FullName));
                });
            }
        });

        public ICommand DeleteNodeCommand => new DelegateCommand<NodeModel>(node =>
        {
            var directory = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDirectory();
            var filename = RegexHelper.GetValidFileName(node.Name, "_") + ".xml";
            var path = string.Format(CultureInfo.InvariantCulture, @"{0}\{1}", directory, filename);

            _projectDirectoryService.DeleteFile(path);

            SavedNodes.Remove(node);
        });


        // View Bindings

        public ObservableCollection<AssemblyNodeModel> SavedAssemblyNodes { get; set; }
        public ObservableCollection<NodeModel> SavedNodes { get; set; }
    }
}