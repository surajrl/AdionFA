using AdionFA.Infrastructure.Common.AssembledBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.StrategyBuilder.Services;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Common.Weka.Services;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Validators.AssembledBuilder;
using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class AssembledBuilderViewModel : MenuItemViewModel, IDisposable
    {
        private readonly IMapper _mapper;

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IAssembledBuilderService _assembledBuilderService;

        private readonly IProjectServiceAgent _projectService;
        private readonly IMarketDataServiceAgent _historicalDataService;
        private readonly IEventAggregator _eventAggregator;

        private readonly ManualResetEventSlim _manualResetEventSlim;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public AssembledBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _assembledBuilderService = IoC.Get<IAssembledBuilderService>();

            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _historicalDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            AssembledBuilder = new();

            _cancellationTokenSource = new();
            _manualResetEventSlim = new(true);
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.AssembledBuilder.Replace(" ", string.Empty))
            {
                PopulateViewModel();
            }
        });

        public DelegateCommand ProcessBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                // Validator

                if (!Validate(new AssembledBuilderValidator()).IsValid)
                {
                    IsTransactionActive = false;
                    return;
                }

                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                // Historical Data

                var projectHistoricalData = await _historicalDataService.GetHistoricalData(Configuration.HistoricalDataId.Value, true);
                var projectCandles = projectHistoricalData.HistoricalDataCandles.
                Select(hdCandle => new Candle
                {
                    Date = hdCandle.StartDate,
                    Time = hdCandle.StartTime,
                    Open = hdCandle.Open,
                    High = hdCandle.High,
                    Low = hdCandle.Low,
                    Close = hdCandle.Close,
                    Volume = hdCandle.Volume,
                    Spread = hdCandle.Spread
                })
                .OrderBy(d => d.Date)
                .ThenBy(d => d.Time);

                await Task.Factory.StartNew(() =>
                {
                    var projectConfiguration = _mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(Configuration);

                    // Extraction of UP and DOWN Nodes

                    Debug.WriteLine("Started Assembled Builder Extraction");

                    //_assembledBuilderService.CreateExtraction(ProcessArgs.ProjectName, AssembledBuilder, projectCandles, projectConfiguration);

                    Debug.WriteLine("Finished Assembled Builder Extraction");

                    // Strategy

                    Debug.WriteLine("Started Assembled Builder UP Nodes");
                    BacktestProcess("up", projectConfiguration, projectCandles);
                    Debug.WriteLine("Finished Assembled Builder UP Nodes");


                    Debug.WriteLine("Started Assembled Builder DOWN Nodes");
                    BacktestProcess("down", projectConfiguration, projectCandles);
                    Debug.WriteLine("Finished Assembled Builder DOWN Nodes");

                }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);

                IsTransactionActive = false;

                // Result Message

                MessageHelper.ShowMessage(this,
                    CommonResources.AssembledBuilder,
                    MessageResources.AssembledBuilderCompleted);
            }
            catch (Exception ex)
            {
                LogHelper.LogException<AssembledBuilderViewModel>(ex);
                throw;
            }
            finally
            {
                IsTransactionActive = false;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        private void BacktestProcess(string label, ProjectConfigurationDTO projectConfiguration, IEnumerable<Candle> projectCandles)
        {
            var directory = ProcessArgs.ProjectName.ProjectAssembledBuilderExtractorWithoutScheduleDirectory(label.ToUpperInvariant());
            var extractions = _projectDirectoryService.GetFilesInPath(directory);

            if (!extractions.Any())
            {
                IsTransactionActive = false;
                MessageHelper.ShowMessage(this,
                    CommonResources.AssembledBuilder,
                    $"No {label.ToUpperInvariant()} extractions found in the Assembled Builder directory");

                return;
            }

            foreach (var extraction in extractions)
            {
                var wekaApi = new WekaApiClient();
                IList<REPTreeOutputModel> responseWeka = wekaApi.GetREPTreeClassifier(
                extraction.FullName,
                projectConfiguration.DepthWeka,
                projectConfiguration.TotalDecimalWeka,
                projectConfiguration.MinimalSeed,
                projectConfiguration.MaximumSeed,
                projectConfiguration.TotalInstanceWeka,
                (double)projectConfiguration.MaxRatioTree,
                (double)projectConfiguration.NTotalTree,
                true);

                if (!responseWeka.Any())
                {
                    IsTransactionActive = false;
                    MessageHelper.ShowMessage(this,
                        CommonResources.AssembledBuilder,
                        "No Weka trees found");

                    return;
                }

                foreach (var wekaTree in responseWeka)
                {
                    var validNodes = wekaTree.NodeOutput.Where(tree => tree.Winner).ToList();

                    if (!validNodes.Any())
                    {
                        IsTransactionActive = false;
                        MessageHelper.ShowMessage(this,
                            CommonResources.AssembledBuilder,
                            $"No winning {label.ToUpperInvariant()} nodes found in Weka tree");

                        return;
                    }

                    foreach (var node in validNodes)
                    {
                        var strategyBuilder = _assembledBuilderService.BuildBacktestOfNode(
                            node.Label,
                            node.Node,
                            (label.ToLowerInvariant() == "up" ? AssembledBuilder.UPBacktests : AssembledBuilder.DOWNBacktests),
                            projectConfiguration,
                            projectCandles,
                            _manualResetEventSlim,
                            _cancellationTokenSource.Token);

                        if (strategyBuilder.WinningStrategy)
                        {
                            StrategyBuilderService.SerializeBacktest(EntityTypeEnum.AssembledBuilder, ProcessArgs.ProjectName, strategyBuilder.IS);
                        }
                    }
                }
            }
        }

        public DelegateCommand ReloadBtnCommand => new DelegateCommand(() =>
        {
            try
            {
                PopulateViewModel();
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;

                Trace.TraceError(ex.Message);
                throw;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public async void PopulateViewModel()
        {
            try
            {
                var project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true).ConfigureAwait(true);
                Configuration = project?.ProjectConfigurations.FirstOrDefault();

                if (!IsTransactionActive)
                {
                    AssembledBuilder.UPBacktests?.Clear();
                    AssembledBuilder.DOWNBacktests?.Clear();

                    AssembledBuilder = _assembledBuilderService.LoadStrategyBuilderResult(ProcessArgs.ProjectName);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<AssembledBuilderViewModel>(ex);
                throw;
            }
        }


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

        private ProjectConfigurationVM _configuration;
        public ProjectConfigurationVM Configuration
        {
            get => _configuration;
            set => SetProperty(ref _configuration, value);
        }

        private AssembledBuilderModel _assembledBuilder;
        public AssembledBuilderModel AssembledBuilder
        {
            get => _assembledBuilder;
            set => SetProperty(ref _assembledBuilder, value);
        }

        // IDisposable Implementation

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~AssembledBuilderViewModel()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
