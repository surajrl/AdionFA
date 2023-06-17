using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Project.EventAggregator;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class CorrelationFlyoutViewModel : ViewModelBase
    {
        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IEventAggregator _eventAggregator;

        public CorrelationFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            CorrelationNodes = new();
        }

        public ICommand DeleteNodeCommand => new DelegateCommand<REPTreeNodeModel>(node =>
        {
            var directory = node.Label.ToLower(CultureInfo.InvariantCulture) == "up"
            ? ProcessArgs.ProjectName.ProjectStrategyBuilderNodesUPDirectory()
            : ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDOWNDirectory();

            var filename = RegexHelper.GetValidFileName(node.Name, "_") + ".xml";
            _projectDirectoryService.DeleteFile(string.Format(CultureInfo.InvariantCulture, @"{0}\{1}", directory, filename));

            CorrelationNodes.Remove(node);

            _eventAggregator.GetEvent<CorrelationNodeDeletedEvent>().Publish(true);
        });

        // View Bindings

        private ObservableCollection<REPTreeNodeModel> _correlationNodes;

        public ObservableCollection<REPTreeNodeModel> CorrelationNodes
        {
            get => _correlationNodes;
            set => SetProperty(ref _correlationNodes, value);
        }
    }
}