using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
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
using AdionFA.TransferObject.Base;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Infrastructure.Model.Weka;
using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Model.StrategyBuilder;
using AdionFA.UI.Station.Project.Validators.StrategyBuilder;
using AutoMapper;
using DynamicData;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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

        private CancellationTokenSource _cancellationTokenSrc;
        private ManualResetEventSlim _manualResetEvent;
        private readonly object _lock;

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
            _manualResetEvent = new(true);

            AllNodes = new();
            StrategyBuilderProcessList = new();
            MaxParallelism = Environment.ProcessorCount - 1;
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            try
            {
                if (item == HamburgerMenuItems.StrategyBuilder.Replace(" ", string.Empty))
                {
                    PopulateViewModel();
                }
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;

                Trace.TraceError(ex.Message);

                throw;
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
                    && !_projectDirectoryService.GetFilesInPath(upCorrelationDirectory, "*.xml").Any())
                {
                    AllNodes.Clear();
                    StrategyBuilderProcessList.Clear();

                    PopulateViewModel();

                    return true;
                }

                var answer = await MessageHelper.ShowMessageInput(this,
                    "Strategy Builder",
                    "Do you want to delete all the correlation nodes?").ConfigureAwait(true);

                if (answer == MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative)
                {
                    _projectDirectoryService.DeleteAllFiles(upCorrelationDirectory, "*.xml", isBackup: false);
                    _projectDirectoryService.DeleteAllFiles(downCorrelationDirectory, "*.xml", isBackup: false);

                    AllNodes.Clear();
                    StrategyBuilderProcessList.Clear();

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
            _manualResetEvent.Reset();

            foreach (var strategyBuilderProcess in StrategyBuilderProcessList)
            {
                if (strategyBuilderProcess.Status != StrategyBuilderStatusEnum.Completed.GetMetadata().Name
                || strategyBuilderProcess.Status != StrategyBuilderStatusEnum.NotStarted.GetMetadata().Name)
                {
                    strategyBuilderProcess.Status = StrategyBuilderStatusEnum.Stopped.GetMetadata().Name;
                }
            }

            CanCancelOrContinue = true;
        }, () => IsTransactionActive && !CanCancelOrContinue)
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => CanCancelOrContinue);

        public ICommand CancelCommand => new DelegateCommand(() =>
        {
            _cancellationTokenSrc.Cancel();

            foreach (var strategyBuilderProcess in StrategyBuilderProcessList)
            {
                var canceled = StrategyBuilderStatusEnum.Canceled.GetMetadata();
                strategyBuilderProcess.Status = canceled.Name;
            }

            _manualResetEvent.Set();

            CanCancelOrContinue = false;
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        public ICommand ContinueCommand => new DelegateCommand(() =>
        {
            _manualResetEvent.Set();

            foreach (var strategyBuilderProcess in StrategyBuilderProcessList)
            {
                if (strategyBuilderProcess.Status != StrategyBuilderStatusEnum.Completed.GetMetadata().Name)
                {
                    strategyBuilderProcess.Status = StrategyBuilderStatusEnum.Executing.GetMetadata().Name;
                }
            }

            CanCancelOrContinue = false;
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        public ICommand ProcessCommand => new DelegateCommand(async () =>
        {
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

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

                if (Configuration.WithoutSchedule)
                {
                    PopulateStrategyBuilderProcessList(ProcessArgs.ProjectName.ProjectExtractorWithoutScheduleDirectory());
                }
                else
                {
                    PopulateStrategyBuilderProcessList(ProcessArgs.ProjectName.ProjectExtractorAmericaDirectory());
                    PopulateStrategyBuilderProcessList(ProcessArgs.ProjectName.ProjectExtractorEuropeDirectory());
                    PopulateStrategyBuilderProcessList(ProcessArgs.ProjectName.ProjectExtractorAsiaDirectory());
                }

                _cancellationTokenSrc ??= new CancellationTokenSource();

                // Historical Data

                var projectHistoricalData = await _marketDataService.GetHistoricalData(Configuration.HistoricalDataId.Value, true);
                var projectCandles = projectHistoricalData.HistoricalDataCandles
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
                    // Asynchronous - Data Mine (Generate Weka Tree and do IS and OS Backtests)
                    foreach (var strategyBuilderProcess in StrategyBuilderProcessList)
                    {
                        _manualResetEvent.Wait();
                        _cancellationTokenSrc.Token.ThrowIfCancellationRequested();

                        strategyBuilderProcess.Status = StrategyBuilderStatusEnum.Executing.GetMetadata().Name;

                        // Weka

                        strategyBuilderProcess.Message = "Executing Weka";

                        var wekaApi = new WekaApiClient();
                        IList<REPTreeOutputModel> responseWeka = wekaApi.GetREPTreeClassifier(
                            strategyBuilderProcess.Path,
                            Configuration.DepthWeka,
                            Configuration.TotalDecimalWeka,
                            Configuration.MinimalSeed,
                            Configuration.MaximumSeed,
                            Configuration.TotalInstanceWeka,
                            (double)Configuration.MaxRatioTree,
                            (double)Configuration.NTotalTree);

                        var wekaTrees = responseWeka.Select(_mapper.Map<REPTreeOutputModel, REPTreeOutputVM>).ToList();

                        AddItemToStrategyBuilderProcessList(strategyBuilderProcess.ExtractionName, wekaTrees);

                        if (responseWeka.Any() && strategyBuilderProcess.InstancesList.Count > 0)
                        {
                            // Iterate over each Weka Tree from one Extraction
                            foreach (var tree in strategyBuilderProcess.InstancesList)
                            {
                                AllNodes.Add(tree.NodeOutput
                                    .Where(node => node.Winner)
                                    .Select(node =>
                                    {
                                        strategyBuilderProcess.TotalStrategy++;
                                        node.Tree = tree;
                                        node.StrategyBuilderProcess = strategyBuilderProcess;

                                        return node;
                                    }).ToList());
                            }

                            strategyBuilderProcess.Message = $"Weka Completed with {strategyBuilderProcess.TotalStrategy} Nodes Found";
                        }
                    }

                    Parallel.ForEach(
                        AllNodes,
                        new ParallelOptions
                        {
                            MaxDegreeOfParallelism = MaxParallelism,
                            CancellationToken = _cancellationTokenSrc.Token,
                        },
                        node =>
                        {
                            _manualResetEvent.Wait();
                            _cancellationTokenSrc.Token.ThrowIfCancellationRequested();

                            Debug.WriteLine($"[THREAD] {Environment.CurrentManagedThreadId}"
                                + $"\t[EXTRACTION] {node.StrategyBuilderProcess.ExtractionName}"
                                + $"\t[NODE] {node.Name}"
                                + $"\t[MESSAGE] Thread Started");

                            lock (_lock)
                            {
                                node.StrategyBuilderProcess.ExecutingBacktests++;
                            }

                            node.StrategyBuilderProcess.Message = $"Executing Backtest of {node.StrategyBuilderProcess.ExecutingBacktests} {(node.StrategyBuilderProcess.ExecutingBacktests == 1 ? "Node" : "Nodes")}";

                            node.Node = new ObservableCollection<string>(node.Node.OrderByDescending(n => n).ToList());

                            // Backtest ------------------------------------------------------------------------------
                            var timer = new Stopwatch();
                            timer.Start();
                            Debug.WriteLine($"[THREAD] {Environment.CurrentManagedThreadId}"
                                + $"\t[EXTRACTION] {node.StrategyBuilderProcess.ExtractionName}"
                                + $"\t[NODE] {node.Name}"
                                + $"\t[MESSAGE] Backtest Started");

                            var strategyBuilder = _strategyBuilderService.BuildBacktest(
                                node.Label,
                                node.Node.ToList(),
                                _mapper.Map<ConfigurationBaseVM, ConfigurationBaseDTO>(Configuration),
                                projectCandles,
                                _manualResetEvent,
                                _cancellationTokenSrc.Token);

                            timer.Stop();
                            Debug.WriteLine($"[THREAD] {Environment.CurrentManagedThreadId}"
                                + $"\t[EXTRACTION] {node.StrategyBuilderProcess.ExtractionName}"
                                + $"\t[NODE] {node.Name}"
                                + $"\t[MESSAGE] Backtest Finished in {timer.Elapsed:mm\\:ss\\.ffffff}");
                            // ---------------------------------------------------------------------------------------

                            // Update Node ---------------------------------------------------------------------------
                            node.HistoricalData = Configuration.HistoricalDataName;
                            node.WinningStrategy = strategyBuilder.WinningStrategy;

                            // OS

                            if (strategyBuilder.OS != null)
                            {
                                node.CorrelationPass = strategyBuilder.OS.CorrelationPass;

                                node.TotalOpportunityOs = strategyBuilder.OS.TotalOpportunity;
                                node.TotalTradesOs = strategyBuilder.OS.TotalTrades;
                                node.WinningTradesOs = strategyBuilder.OS.WinningTrades;
                                node.LosingTradesOs = strategyBuilder.OS.LosingTrades;

                                node.PercentSuccessOs = strategyBuilder.OS.PercentSuccess;
                                node.ProgressivenessOs = strategyBuilder.OS.Progressiveness;
                            }

                            // IS

                            if (strategyBuilder.IS != null)
                            {
                                node.CorrelationPass = strategyBuilder.IS.CorrelationPass;

                                node.TotalOpportunityIs = strategyBuilder.IS.TotalOpportunity;
                                node.TotalTradesIs = strategyBuilder.IS.TotalTrades;
                                node.WinningTradesIs = strategyBuilder.IS.WinningTrades;
                                node.LosingTradesIs = strategyBuilder.IS.LosingTrades;

                                node.PercentSuccessIs = strategyBuilder.IS.PercentSuccess;
                                node.ProgressivenessIs = strategyBuilder.IS.Progressiveness;
                            }
                            // ---------------------------------------------------------------------------------------

                            if (node.WinningStrategy)
                            {
                                // Serialization -------------------------------------------------------------------------
                                StrategyBuilderService.SerializeBacktest(ProcessArgs.ProjectName, strategyBuilder.IS);
                                StrategyBuilderService.SerializeNode(ProcessArgs.ProjectName, node.Name, _mapper.Map<REPTreeNodeVM, REPTreeNodeModel>(node));
                                // ---------------------------------------------------------------------------------------

                                // Update Tree ---------------------------------------------------------------------------
                                if (node.Label.ToLower(CultureInfo.CurrentCulture) == "up")
                                {
                                    node.Tree.TotalWinningStrategyUP += node.WinningStrategy ? 1 : 0;
                                }

                                if (node.Label.ToLower(CultureInfo.CurrentCulture) == "down")
                                {
                                    node.Tree.TotalWinningStrategyDOWN += node.WinningStrategy ? 1 : 0;
                                }

                                node.Tree.TotalWinningStrategy = node.Tree.TotalWinningStrategyUP + node.Tree.TotalWinningStrategyDOWN;
                                node.Tree.HasWinningStrategy = node.Tree.TotalWinningStrategy > 0;
                                node.StrategyBuilderProcess.HasWinningStrategy = true;
                                // ---------------------------------------------------------------------------------------
                            }

                            lock (_lock)
                            {
                                node.StrategyBuilderProcess.ExecutingBacktests--;
                                node.StrategyBuilderProcess.CompletedBacktests++;
                                node.Tree.CounterProgressBar++;
                            }

                            node.StrategyBuilderProcess.Message = $"Executing Backtest of {node.StrategyBuilderProcess.ExecutingBacktests} {(node.StrategyBuilderProcess.ExecutingBacktests == 1 ? "Node" : "Nodes")}";

                            if (node.StrategyBuilderProcess.TotalStrategy == node.StrategyBuilderProcess.CompletedBacktests)
                            {
                                var completed = StrategyBuilderStatusEnum.Completed.GetMetadata();
                                node.StrategyBuilderProcess.Status = completed.Name;
                                node.StrategyBuilderProcess.Message = completed.Description;
                            }

                            Debug.WriteLine($"[THREAD] {Environment.CurrentManagedThreadId}"
                                + $"\t[EXTRACTION] {node.StrategyBuilderProcess.ExtractionName}"
                                + $"\t[NODE] {node.Name}"
                                + $"\t[MESSAGE] Thread Finished");
                        });
                });

                // Correlation

                var correlation = new CorrelationModel();
                await Task.Run(() =>
                {
                    correlation = _strategyBuilderService.Correlation(
                        ProcessArgs.ProjectName,
                        Configuration.SBMaxPercentCorrelation,
                        EntityTypeEnum.StrategyBuilder);
                });

                TotalCorrelationUP = correlation.ISBacktestUP?.Count ?? 0;
                TotalCorrelationDOWN = correlation.ISBacktestDOWN?.Count ?? 0;

                // Update REP Tree Node VM with correlation pass results

                correlation.ISBacktestUP.ForEach(backtestUP =>
                {
                    var correspondingNode = AllNodes.FirstOrDefault(node => node.Node.SequenceEqual(backtestUP.Node));
                    if (correspondingNode != null)
                    {
                        correspondingNode.CorrelationPass = backtestUP.CorrelationPass;
                    }
                });

                correlation.ISBacktestDOWN.ForEach(backtestDOWN =>
                {
                    var correspondingNode = AllNodes.FirstOrDefault(node => node.Node.SequenceEqual(backtestDOWN.Node));
                    if (correspondingNode != null)
                    {
                        correspondingNode.CorrelationPass = backtestDOWN.CorrelationPass;
                    }
                });

                stopwatch.Stop();

                // Results Message

                var result = !StrategyBuilderProcessList.Any(strategyBuilderProcess => strategyBuilderProcess.Status != StrategyBuilderStatusEnum.Completed.GetMetadata().Name);
                var msg = result ? MessageResources.StrategyBuilderCompleted : MessageResources.EntityErrorTransaction;

                if (result && !correlation.Success)
                {
                    msg = $"{msg}\n{MessageResources.CorrelationRunWithNoResults}";
                }

                MessageHelper.ShowMessage(this,
                    CommonResources.StrategyBuilder,
                    $"{msg}\n\nTotal time taken {stopwatch.Elapsed:mm\\:ss\\.ffffff}");
            }
            catch (OperationCanceledException ex)
            {
                LogHelper.LogException<StrategyBuilderViewModel>(ex);

                foreach (var strategyBuilderProcess in StrategyBuilderProcessList)
                {
                    var suspended = StrategyBuilderStatusEnum.Canceled.GetMetadata();
                    strategyBuilderProcess.Status = suspended.Name;
                    strategyBuilderProcess.Message = suspended.Description;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderViewModel>(ex);

                foreach (var strategyBuilderProcess in StrategyBuilderProcessList)
                {
                    strategyBuilderProcess.Status = StrategyBuilderStatusEnum.Error.GetMetadata().Name;
                }

                throw;
            }
            finally
            {
                _cancellationTokenSrc.Dispose();
                _cancellationTokenSrc = null;

                IsTransactionActive = false;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public async void PopulateViewModel()
        {
            try
            {
                var project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true).ConfigureAwait(true);
                Configuration = project?.ProjectConfigurations.FirstOrDefault();

                UpdateTotalCorrelationNodes();

                if (!IsTransactionActive && !StrategyBuilderProcessList.Any())
                {
                    if (Configuration.WithoutSchedule)
                    {
                        PopulateStrategyBuilderProcessList(ProcessArgs.ProjectName.ProjectExtractorWithoutScheduleDirectory());
                    }
                    else
                    {
                        PopulateStrategyBuilderProcessList(ProcessArgs.ProjectName.ProjectExtractorAmericaDirectory());
                        PopulateStrategyBuilderProcessList(ProcessArgs.ProjectName.ProjectExtractorEuropeDirectory());
                        PopulateStrategyBuilderProcessList(ProcessArgs.ProjectName.ProjectExtractorAsiaDirectory());
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);

                throw;
            }
        }

        private void PopulateStrategyBuilderProcessList(string extractionPath)
        {
            var regionName = "WithoutSchedule";
            var regionType = 0;

            if (extractionPath.Contains(Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.America)))
            {
                regionType = (int)MarketRegionEnum.America;
                regionName = Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.America);
            }

            if (extractionPath.Contains(Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Europe)))
            {
                regionType = (int)MarketRegionEnum.Europe;
                regionName = Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Europe);
            }

            if (extractionPath.Contains(Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Asia)))
            {
                regionType = (int)MarketRegionEnum.Asia;
                regionName = Enum.GetName(typeof(MarketRegionEnum), MarketRegionEnum.Asia);
            }

            _projectDirectoryService.GetFilesInPath(extractionPath).ToList().ForEach(file =>
            {
                StrategyBuilderProcessList.Add(new StrategyBuilderProcessModel
                {
                    Path = file.FullName,
                    ExtractionName = file.Name,
                    RegionType = regionType,
                    RegionName = regionName,
                    Status = StrategyBuilderStatusEnum.NotStarted.GetMetadata().Name,
                    IsEnabled = false,
                    IsExpanded = false,
                    InstancesList = new ObservableCollection<REPTreeOutputVM>()
                });
            });
        }

        private void AddItemToStrategyBuilderProcessList(string templateName, IList<REPTreeOutputVM> wekaTree)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var strategyBuilderProcess = StrategyBuilderProcessList.FirstOrDefault(m => m.ExtractionName == templateName);

                foreach (var tree in wekaTree)
                {
                    tree.Message = tree.HasWinningStrategy == null ? CommonResources.Pending : tree.HasWinningStrategy.Value ? CommonResources.Winner : CommonResources.Loser;
                    tree.ValidNodes = tree.NodeOutput.Where(node => node.Winner).Count();
                }

                strategyBuilderProcess.WinningTrees = wekaTree.Where(n => n.ValidNodes > 0).Count();
                strategyBuilderProcess.InstancesList.Clear();
                strategyBuilderProcess.InstancesList.AddRange(wekaTree);

                if (strategyBuilderProcess.WinningTrees > 0)
                {
                    strategyBuilderProcess.IsEnabled = true;
                }
            });
        }

        public void UpdateTotalCorrelationNodes()
        {
            const int xmlFilesPerNode = 2;

            var upCorrelationDirectory = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesUPDirectory();
            var downCorrelationDirectory = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDOWNDirectory();

            TotalCorrelationUP = _projectDirectoryService.GetFilesInPath(upCorrelationDirectory, "*.xml").Length / xmlFilesPerNode;
            TotalCorrelationDOWN = _projectDirectoryService.GetFilesInPath(downCorrelationDirectory, "*.xml").Length / xmlFilesPerNode;
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

        private ObservableCollection<REPTreeNodeVM> _allNodes;

        public ObservableCollection<REPTreeNodeVM> AllNodes
        {
            get => _allNodes;
            set => SetProperty(ref _allNodes, value);
        }

        private ProjectConfigurationVM _configuration;

        public ProjectConfigurationVM Configuration
        {
            get => _configuration;
            set => SetProperty(ref _configuration, value);
        }

        private int _totalCorrelationUP;

        public int TotalCorrelationUP
        {
            get => _totalCorrelationUP;
            set => SetProperty(ref _totalCorrelationUP, value);
        }

        private int _totalCorrelationDOWN;

        public int TotalCorrelationDOWN
        {
            get => _totalCorrelationDOWN;
            set => SetProperty(ref _totalCorrelationDOWN, value);
        }

        private int _maxParallelism;

        public int MaxParallelism
        {
            get => _maxParallelism;
            set => SetProperty(ref _maxParallelism, value);
        }

        private int _currentConfiguration;

        public int CurrentConfiguration
        {
            get => _currentConfiguration;
            set => SetProperty(ref _currentConfiguration, value);
        }

        private ObservableCollection<StrategyBuilderProcessModel> _strategyBuilderProcessList;

        public ObservableCollection<StrategyBuilderProcessModel> StrategyBuilderProcessList
        {
            get => _strategyBuilderProcessList;
            set => SetProperty(ref _strategyBuilderProcessList, value);
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
