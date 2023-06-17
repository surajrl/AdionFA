using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
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
using AdionFA.UI.Station.Project.Model.StrategyBuilder;
using AdionFA.UI.Station.Project.Validators.StrategyBuilder;
using AutoMapper;
using DynamicData;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class StrategyBuilderViewModel : MenuItemViewModel, IDisposable
    {
        private readonly IMapper _mapper;

        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IStrategyBuilderService _strategyBuilderService;

        private readonly IProjectServiceAgent _projectService;
        private readonly IMarketDataServiceAgent _marketDataService;

        private readonly IEventAggregator _eventAggregator;

        private CancellationTokenSource _cancellationTokenSource;
        private ManualResetEventSlim _manualResetEventSlim;
        private readonly object _lock;
        private bool _disposedValue;


        public StrategyBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _strategyBuilderService = IoC.Get<IStrategyBuilderService>();

            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _marketDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);
            _eventAggregator.GetEvent<CorrelationNodeDeletedEvent>().Subscribe(p => UpdateTotalCorrelationNodes());

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            _lock = new();
            _manualResetEventSlim = new(true);
            _cancellationTokenSource = new();

            StrategyBuilderProcesses = new();
            StrategyBuilder = new();
            MaxParallelism = Environment.ProcessorCount - 1;
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.StrategyBuilder.Replace(" ", string.Empty))
            {
                PopulateViewModel();
            }
        });

        public ICommand RefreshCommand => new DelegateCommand(async () => await RefreshAsync().ConfigureAwait(true), () => !IsTransactionActive)
            .ObservesProperty(() => IsTransactionActive);

        private async Task<bool> RefreshAsync()
        {
            try
            {
                var upCorrelationDirectory = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesUPDirectory();
                var downCorrelationDirectory = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDOWNDirectory();

                if (!_projectDirectoryService.GetFilesInPath(upCorrelationDirectory, "*.xml").Any()
                    && !_projectDirectoryService.GetFilesInPath(downCorrelationDirectory, "*.xml").Any())
                {
                    StrategyBuilder.CorrelationNodesUP.Clear();
                    StrategyBuilder.CorrelationNodesDOWN.Clear();

                    StrategyBuilderProcesses.Clear();

                    PopulateViewModel();

                    return true;
                }

                var answer = await MessageHelper.ShowMessageInput(this,
                    "Strategy Builder",
                    "Do you want to delete all the correlation nodes?").ConfigureAwait(true);

                if (answer == MessageDialogResult.Affirmative)
                {
                    _projectDirectoryService.DeleteAllFiles(upCorrelationDirectory, "*.xml", isBackup: false);
                    _projectDirectoryService.DeleteAllFiles(downCorrelationDirectory, "*.xml", isBackup: false);

                    StrategyBuilder.CorrelationNodesUP.Clear();
                    StrategyBuilder.CorrelationNodesDOWN.Clear();

                    StrategyBuilderProcesses.Clear();

                    PopulateViewModel();

                    return true;
                }

                PopulateViewModel();

                return false;
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;

                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ICommand StopCommand => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Reset();

            foreach (var process in StrategyBuilderProcesses)
            {
                if (process.Message != StrategyBuilderStatus.Completed.GetMetadata().Description
                && process.Message != StrategyBuilderStatus.NotStarted.GetMetadata().Description)
                {
                    var stopped = StrategyBuilderStatus.Stopped.GetMetadata().Description;
                    process.Message = $"{process.Message} - {stopped}";
                }
            }

            CanCancelOrContinue = true;
        }, () => IsTransactionActive && !CanCancelOrContinue)
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => CanCancelOrContinue);

        public ICommand CancelCommand => new DelegateCommand(() =>
        {
            _cancellationTokenSource.Cancel();
            _manualResetEventSlim.Set();

            CanCancelOrContinue = false;
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        public ICommand ContinueCommand => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Set();

            var stopped = StrategyBuilderStatus.Stopped.GetMetadata().Description;
            foreach (var process in StrategyBuilderProcesses)
            {
                process.Message = process.Message.Replace($" - {stopped}", string.Empty);
            }
            CanCancelOrContinue = false;
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        public ICommand ProcessCommand => new DelegateCommand(async () =>
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var validator = Validate(new StrategyBuilderValidator());
                if (!validator.IsValid)
                {
                    MessageHelper.ShowMessages(this,
                        EntityTypeEnum.StrategyBuilder.GetMetadata().Description,
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                if (!await RefreshAsync().ConfigureAwait(true))
                {
                    return;
                }

                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                if (ProjectConfiguration.WithoutSchedule)
                {
                    PopulateStrategyBuilderProcesses(ProcessArgs.ProjectName.ProjectExtractorWithoutScheduleDirectory());
                }
                else
                {
                    PopulateStrategyBuilderProcesses(ProcessArgs.ProjectName.ProjectExtractorAmericaDirectory());
                    PopulateStrategyBuilderProcesses(ProcessArgs.ProjectName.ProjectExtractorEuropeDirectory());
                    PopulateStrategyBuilderProcesses(ProcessArgs.ProjectName.ProjectExtractorAsiaDirectory());
                }

                _cancellationTokenSource = new();

                // Historical Data

                var projectHistoricalData = await _marketDataService.GetHistoricalData(ProjectConfiguration.HistoricalDataId.Value, true);
                var allProjectCandles = projectHistoricalData.HistoricalDataCandles
                .Select(hdCandle => new Candle
                {
                    Date = hdCandle.StartDate,
                    Time = hdCandle.StartTime,

                    Open = hdCandle.Open,
                    High = hdCandle.High,
                    Low = hdCandle.Low,
                    Close = hdCandle.Close,

                    Volume = hdCandle.Volume,
                    Spread = hdCandle.Spread,

                    Label = hdCandle.Close > hdCandle.Open ? "UP" : "DOWN"
                })
                .OrderBy(candle => candle.Date)
                .ThenBy(candle => candle.Time).ToList();

                await Task.Run(() =>
                {
                    var allBacktestNodes = new List<REPTreeNodeModel>();

                    // Get all the nodes that will be backtested

                    foreach (var process in StrategyBuilderProcesses)
                    {
                        _manualResetEventSlim.Wait();
                        _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                        // Weka

                        process.Message = StrategyBuilderStatus.ExecutingWeka.GetMetadata().Description;

                        var wekaApi = new WekaApiClient();
                        var responseWeka = wekaApi.GetREPTreeClassifier(
                            process.ExtractionPath,
                            ProjectConfiguration.DepthWeka,
                            ProjectConfiguration.TotalDecimalWeka,
                            ProjectConfiguration.MinimalSeed,
                            ProjectConfiguration.MaximumSeed,
                            ProjectConfiguration.TotalInstanceWeka,
                            (double)ProjectConfiguration.MaxRatioTree,
                            (double)ProjectConfiguration.NTotalTree);

                        process.Tree = responseWeka[0];

                        var nodes = process.Tree.NodeOutput.Where(node => node.Winner)
                        .Select(node =>
                        {
                            node.Node = node.Node.OrderByDescending(node => node).ToList();
                            return node;
                        })
                        .ToList();

                        allBacktestNodes.AddRange(nodes);
                        process.BacktestNodes.Clear();
                        process.BacktestNodes.AddRange(nodes);

                        _manualResetEventSlim.Wait();
                        _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                        process.Message = AssembledBuilderStatus.WekaCompleted.GetMetadata().Description;
                    }

                    // Perform backtest of all the nodes

                    Parallel.ForEach(
                        allBacktestNodes,
                        new ParallelOptions
                        {
                            MaxDegreeOfParallelism = MaxParallelism,
                            CancellationToken = _cancellationTokenSource.Token,
                        },
                        node =>
                        {
                            _manualResetEventSlim.Wait();
                            _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                            var process = StrategyBuilderProcesses.Where(process => process.BacktestNodes.Any(processNode => processNode == node)).FirstOrDefault();

                            lock (_lock)
                            {
                                process.ExecutingBacktests++;
                                process.Message = $"{StrategyBuilderStatus.ExecutingBacktest.GetMetadata().Description} of {process.ExecutingBacktests} Nodes";
                            }

                            _strategyBuilderService.BuildBacktestOfNode(
                                node,
                                _mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(ProjectConfiguration),
                                allProjectCandles,
                                _manualResetEventSlim,
                                _cancellationTokenSource.Token);

                            if (node.WinningStrategy)
                            {
                                StrategyBuilderService.SerializeNode(EntityTypeEnum.StrategyBuilder, ProcessArgs.ProjectName, node);
                            }

                            lock (_lock)
                            {
                                process.ExecutingBacktests--;
                                process.CompletedBacktests++;
                                process.ProgressCounter++;

                                if (process.CompletedBacktests == process.BacktestNodes.Count)
                                {
                                    process.Message = StrategyBuilderStatus.BacktestCompleted.GetMetadata().Description;
                                }
                            }
                        });
                });

                // Correlation Analysis

                foreach (var process in StrategyBuilderProcesses)
                {
                    process.Message = StrategyBuilderStatus.ExecutingCorrelation.GetMetadata().Description;
                }

                await Task.Run(() =>
                {
                    _strategyBuilderService.Correlation(
                        ProcessArgs.ProjectName,
                        StrategyBuilder,
                        ProjectConfiguration.SBMaxCorrelationPercent);
                });

                foreach (var process in StrategyBuilderProcesses)
                {
                    process.Message = StrategyBuilderStatus.Completed.GetMetadata().Description;
                }

                // Results Message

                var msgUP = StrategyBuilder.CorrelationNodesUP.Count > 0 ? $"{StrategyBuilder.CorrelationNodesUP.Count} Correlation UP Nodes Found!" : "No Correlation UP Nodes Found.";
                var msgDOWN = StrategyBuilder.CorrelationNodesDOWN.Count > 0 ? $"{StrategyBuilder.CorrelationNodesDOWN.Count} Correlation DOWN Nodes Found!" : "No Correlation DOWN Nodes Found.";

                MessageHelper.ShowMessage(this,
                    CommonResources.StrategyBuilder,
                    $"{MessageResources.StrategyBuilderCompleted}\n\n"
                    + $"{msgUP}\n"
                    + $"{msgDOWN}\n\n"
                    + $"Strategy Builder Completed in {stopwatch.Elapsed:hh\\:mm\\:ss\\.fff}");
            }
            catch (OperationCanceledException)
            {
                foreach (var strategyBuilderProcess in StrategyBuilderProcesses)
                {
                    strategyBuilderProcess.Message = StrategyBuilderStatus.Canceled.GetMetadata().Description;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderViewModel>(ex);
                throw;
            }
            finally
            {
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;

                IsTransactionActive = false;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public async void PopulateViewModel()
        {
            try
            {
                // Get the latest project configuration
                var project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true).ConfigureAwait(true);
                ProjectConfiguration = project?.ProjectConfigurations.FirstOrDefault();

                UpdateTotalCorrelationNodes();

                if (!IsTransactionActive && !StrategyBuilderProcesses.Any())
                {
                    if (ProjectConfiguration.WithoutSchedule)
                    {
                        PopulateStrategyBuilderProcesses(ProcessArgs.ProjectName.ProjectExtractorWithoutScheduleDirectory());
                    }
                    else
                    {
                        PopulateStrategyBuilderProcesses(ProcessArgs.ProjectName.ProjectExtractorAmericaDirectory());
                        PopulateStrategyBuilderProcesses(ProcessArgs.ProjectName.ProjectExtractorEuropeDirectory());
                        PopulateStrategyBuilderProcesses(ProcessArgs.ProjectName.ProjectExtractorAsiaDirectory());
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private void PopulateStrategyBuilderProcesses(string extractionPath)
        {
            var regionName = "WithoutSchedule";

            if (extractionPath.Contains(Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.America)))
            {
                regionName = Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.America);
            }

            if (extractionPath.Contains(Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Europe)))
            {
                regionName = Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Europe);
            }

            if (extractionPath.Contains(Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Asia)))
            {
                regionName = Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Asia);
            }

            _projectDirectoryService.GetFilesInPath(extractionPath).ToList().ForEach(file =>
            {
                StrategyBuilderProcesses.Add(new StrategyBuilderProcessModel
                {
                    ExtractionPath = file.FullName,
                    ExtractionName = file.Name,
                    Message = StrategyBuilderStatus.NotStarted.GetMetadata().Description,
                    Tree = new(),
                    BacktestNodes = new()
                });
            });
        }

        private void UpdateTotalCorrelationNodes()
        {
            var upCorrelationDirectory = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesUPDirectory();
            var downCorrelationDirectory = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDOWNDirectory();

            StrategyBuilder.CorrelationNodesUP.Clear();
            StrategyBuilder.CorrelationNodesDOWN.Clear();

            _projectDirectoryService.GetFilesInPath(upCorrelationDirectory, "*.xml").ToList().ForEach(file =>
            {
                StrategyBuilder.CorrelationNodesUP.Add(SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName));
            });

            _projectDirectoryService.GetFilesInPath(downCorrelationDirectory, "*.xml").ToList().ForEach(file =>
            {
                StrategyBuilder.CorrelationNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName));
            });
        }

        // View Bindings

        private bool _canCancelOrContinue;
        public bool CanCancelOrContinue
        {
            get => _canCancelOrContinue;
            set => SetProperty(ref _canCancelOrContinue, value);
        }

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

        private ProjectConfigurationVM _projectConfiguration;
        public ProjectConfigurationVM ProjectConfiguration
        {
            get => _projectConfiguration;
            set => SetProperty(ref _projectConfiguration, value);
        }

        private StrategyBuilderModel _strategyBuilder;
        public StrategyBuilderModel StrategyBuilder
        {
            get => _strategyBuilder;
            set => SetProperty(ref _strategyBuilder, value);
        }

        private int _maxParallelism;
        public int MaxParallelism
        {
            get => _maxParallelism;
            set => SetProperty(ref _maxParallelism, value);
        }

        private ObservableCollection<StrategyBuilderProcessModel> _strategyBuilderProcesses;
        public ObservableCollection<StrategyBuilderProcessModel> StrategyBuilderProcesses
        {
            get => _strategyBuilderProcesses;
            set => SetProperty(ref _strategyBuilderProcesses, value);
        }

        // IDisposable Implementation

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~StrategyBuilderViewModel()
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
