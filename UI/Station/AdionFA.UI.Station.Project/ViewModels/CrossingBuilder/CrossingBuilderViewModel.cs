using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.Modules.CrossingBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums.Model;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Model.Common;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class CrossingBuilderViewModel : MenuItemViewModel
    {
        private readonly IProjectDirectoryService _projectDirectoryService;

        private readonly IEventAggregator _eventAggregator;
        private readonly IMarketDataServiceAgent _marketDataService;
        private readonly IProjectServiceAgent _projectService;

        public CrossingBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();

            _marketDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);
            _eventAggregator.GetEvent<StrategyBuilderCompletedEvent>().Subscribe(strategyBuilderCompleted =>
            {
                if (!strategyBuilderCompleted)
                {
                    // New process starting
                    CrossingBuilder.StrategyNodesUP.Clear();
                    CrossingBuilder.StrategyNodesDOWN.Clear();
                }
            });
            _eventAggregator.GetEvent<AssemblyBuilderCompletedEvent>().Subscribe(assemblyBuilderCompleted =>
            {
                if (assemblyBuilderCompleted)
                {
                    // Load new Assembly Nodes UP and Assembly Nodes DOWN
                    _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectAssemblyBuilderNodesUPDirectory(), "*.xml").ToList().ForEach(file =>
                    {
                        CrossingBuilder.StrategyNodesUP.Add(new StrategyNodeModel
                        {
                            MainNode = SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName),
                            CrossingNodes = new()
                        });
                    });
                    _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectAssemblyBuilderNodesDOWNDirectory(), "*.xml").ToList().ForEach(file =>
                    {
                        CrossingBuilder.StrategyNodesDOWN.Add(new StrategyNodeModel
                        {
                            MainNode = SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName),
                            CrossingNodes = new()
                        });
                    });
                }
            });

            CrossingHistoricalData = new();
            CrossingBuilderProcessesUP = new();
            CrossingBuilderProcessesDOWN = new();
            CrossingBuilder = new();

            // Load the Assembly Nodes UP and Assembly Nodes DOWN
            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectAssemblyBuilderNodesUPDirectory(), "*.xml").ToList().ForEach(file =>
            {
                CrossingBuilder.StrategyNodesUP.Add(new StrategyNodeModel
                {
                    MainNode = SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName),
                    CrossingNodes = new()
                });
            });
            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectAssemblyBuilderNodesDOWNDirectory(), "*.xml").ToList().ForEach(file =>
            {
                CrossingBuilder.StrategyNodesDOWN.Add(new StrategyNodeModel
                {
                    MainNode = SerializerHelper.XMLDeSerializeObject<AssemblyNodeModel>(file.FullName),
                    CrossingNodes = new()
                });
            });
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(async item =>
        {
            if (item == HamburgerMenuItems.CrossingBuilder.Replace(" ", string.Empty))
            {
                ProjectConfiguration = await _projectService.GetProjectConfigurationAsync(ProcessArgs.ProjectId).ConfigureAwait(true);

                var historicalData = await _marketDataService.GetAllHistoricalDataAsync().ConfigureAwait(true);
                CrossingHistoricalData.Clear();
                CrossingHistoricalData.AddRange(historicalData.Where(hd => hd.HistoricalDataId != ProjectConfiguration.HistoricalDataId).Select(hd => new Metadata
                {
                    Id = hd.HistoricalDataId,
                    Name = hd.Description
                }));

                if (!IsTransactionActive /* Check if any of the correlation nodes has been completed */)
                {
                    var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
                    foreach (var file in extractionTemplates)
                    {
                        CrossingBuilderProcessesUP.Add(new BuilderProcess
                        {

                        });
                    }
                }
            }
        });

        public ICommand Stop => new DelegateCommand(() =>
        {
            // ...
        }, () => IsTransactionActive && !CanCancelOrContinue)
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => CanCancelOrContinue);

        public ICommand Cancel => new DelegateCommand(() =>
        {
            // ...
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        public ICommand Continue => new DelegateCommand(() =>
        {
            // ...
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        public ICommand Reset => new DelegateCommand(() =>
        {
            // Reset the Strategy Builder nodes and start new ...
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public ICommand Process => new DelegateCommand(() =>
        {
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                // ...
            }
            catch (Exception ex)
            {
                LogHelper.LogException<CrossingBuilderViewModel>(ex);
                throw;
            }
            finally
            {
                IsTransactionActive = false;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);
            }

        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        // View Bindings

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }

        private bool _canExecute = true;
        public bool CanExecute
        {
            get => _canExecute;
            set => SetProperty(ref _canExecute, value);
        }

        private bool _canCancelOrContinue;
        public bool CanCancelOrContinue
        {
            get => _canCancelOrContinue;
            set => SetProperty(ref _canCancelOrContinue, value);
        }

        private ProjectConfigurationVM _projectConfiguration;
        public ProjectConfigurationVM ProjectConfiguration
        {
            get => _projectConfiguration;
            set => SetProperty(ref _projectConfiguration, value);
        }

        private CrossingBuilderModel _crossingBuilder;
        public CrossingBuilderModel CrossingBuilder
        {
            get => _crossingBuilder;
            set => SetProperty(ref _crossingBuilder, value);
        }

        public ObservableCollection<BuilderProcess> CrossingBuilderProcessesUP { get; }
        public ObservableCollection<BuilderProcess> CrossingBuilderProcessesDOWN { get; }

        public ObservableCollection<Metadata> CrossingHistoricalData { get; }
        public int CrossingHistoricalDataId { get; set; }


    }
}
