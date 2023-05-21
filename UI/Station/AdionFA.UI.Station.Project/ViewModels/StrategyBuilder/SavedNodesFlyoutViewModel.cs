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
using DynamicData;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            applicationCommands.DeleteNodeCommand.RegisterCommand(DeleteNodeCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            SavedNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleSavedNodes))
            {
                SavedNodes.Clear();

                var path = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDirectory();
                _projectDirectoryService.GetFilesInPath(path, "*.xml")
                .ToList()
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
            var filepath = string.Format(@"{0}\{1}.xml", directory, RegexHelper.GetValidFileName(node.NodeName(), "_"));

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