using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Services;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class SavedNodesFlyoutViewModel : ViewModelBase
    {
        private readonly IProjectDirectoryService _projectDirectoryService;

        public SavedNodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            SavedNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleSavedNodes, StringComparison.Ordinal))
            {
                SavedNodes.Clear();

                _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDirectory(), "*.xml").ToList()
                .ForEach(file =>
                {
                    SavedNodes.Add(SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName));
                });
            }
        });


        // View Bindings

        public ObservableCollection<REPTreeNodeModel> SavedNodes { get; set; }
    }
}