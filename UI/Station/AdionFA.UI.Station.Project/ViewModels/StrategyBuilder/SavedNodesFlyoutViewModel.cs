using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Weka;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Project.AutoMapper;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public SavedNodesFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            SavedNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleSavedNodes, StringComparison.Ordinal))
            {
                SavedNodes.Clear();

                var path = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDirectory();
                _projectDirectoryService.GetFilesInPath(path, "*.xml").ToList()
                .ForEach(file =>
                {
                    var node = _mapper.Map<REPTreeNodeModel, REPTreeNodeVM>(SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName));
                    SavedNodes.Add(node);
                });
            }
        });

        public ICommand DeleteNodeCommand => new DelegateCommand<REPTreeNodeVM>(node =>
        {
            var directory = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDirectory();

            var filename = $"NODE-{RegexHelper.GetValidFileName(node.Name, "_")}-{node.TotalTradesIs}.xml";
            var filepath = string.Format(CultureInfo.InvariantCulture, @"{0}\{1}.xml", directory, filename);

            _projectDirectoryService.DeleteFile(filepath);
            SavedNodes.Remove(node);
        });

        // View Bindings

        private ObservableCollection<REPTreeNodeVM> _savedNodes;

        public ObservableCollection<REPTreeNodeVM> SavedNodes
        {
            get => _savedNodes;
            set => SetProperty(ref _savedNodes, value);
        }
    }
}