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
using AdionFA.UI.Station.Project.EventAggregator;
using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class CorrelationFlyoutViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IEventAggregator _eventAggregator;

        public CorrelationFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            CorrelationNodes = new();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleCorrelation, StringComparison.Ordinal))
            {
                CorrelationNodes.Clear();

                var directoryUP = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesUPDirectory();
                var directoryDOWN = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDOWNDirectory();

                _projectDirectoryService.GetFilesInPath(directoryUP, "*.xml").ToList().ForEach(file =>
                {
                    if (file.Name.Contains("NODE"))
                    {
                        var correlationNodeUP = SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName);
                        CorrelationNodes.Add(_mapper.Map<REPTreeNodeModel, REPTreeNodeVM>(correlationNodeUP));
                    }
                });

                _projectDirectoryService.GetFilesInPath(directoryDOWN, "*.xml").ToList().ForEach(file =>
                {
                    if (file.Name.Contains("NODE"))
                    {
                        var correlationNodeDOWN = SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName);
                        CorrelationNodes.Add(_mapper.Map<REPTreeNodeModel, REPTreeNodeVM>(correlationNodeDOWN));
                    }
                });
            }
        });

        public ICommand DeleteNodeCommand => new DelegateCommand<REPTreeNodeVM>(node =>
        {
            var directory = node.Label.ToLower(CultureInfo.InvariantCulture) == "up" ?
            ProcessArgs.ProjectName.ProjectStrategyBuilderNodesUPDirectory() :
            ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDOWNDirectory();

            var nodeFilename = $"NODE-{RegexHelper.GetValidFileName(node.Name, "_")}-{node.TotalTradesIs}.xml";
            var backtestFilename = $"BACKTEST-{RegexHelper.GetValidFileName(node.Name, "_")}-{node.TotalTradesIs}.xml";

            var filepathBacktest = string.Format(CultureInfo.InvariantCulture, @"{0}\BACKTEST-{1}.xml", directory, backtestFilename);
            var filepathNode = string.Format(CultureInfo.InvariantCulture, @"{0}\NODE-{1}.xml", directory, nodeFilename);

            _projectDirectoryService.DeleteFile(filepathBacktest);
            _projectDirectoryService.DeleteFile(filepathNode);

            CorrelationNodes.Remove(node);

            _eventAggregator.GetEvent<CorrelationNodeDeletedEvent>().Publish(true);
        });

        // View Bindings

        private ObservableCollection<REPTreeNodeVM> _correlationNodes;

        public ObservableCollection<REPTreeNodeVM> CorrelationNodes
        {
            get => _correlationNodes;
            set => SetProperty(ref _correlationNodes, value);
        }
    }
}