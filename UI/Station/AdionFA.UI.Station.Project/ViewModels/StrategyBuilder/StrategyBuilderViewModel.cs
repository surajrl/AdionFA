using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
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
using AdionFA.UI.Station.Project.Model.Common;
using AdionFA.UI.Station.Project.Validators.StrategyBuilder;
using AutoMapper;
using DynamicData;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using ReactiveUI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
        private readonly IExtractorService _extractorService;
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
            _extractorService = IoC.Get<IExtractorService>();
            _strategyBuilderService = IoC.Get<IStrategyBuilderService>();

            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _marketDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            _lock = new();
            _manualResetEventSlim = new(true);
            _cancellationTokenSource = new();

            StrategyBuilderProcesses = new();
            MaxParallelism = Environment.ProcessorCount - 1;

            // Load XML files containing Winning Nodes
            StrategyBuilder = new();
            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectStrategyBuilderNodesUPDirectory(), "*.xml").ToList().ForEach(file =>
            {
                StrategyBuilder.WinningNodesUP.Add(SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName));
            });
            _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDOWNDirectory(), "*.xml").ToList().ForEach(file =>
            {
                StrategyBuilder.WinningNodesDOWN.Add(SerializerHelper.XMLDeSerializeObject<REPTreeNodeModel>(file.FullName));
            });
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(async item =>
        {
            if (item == HamburgerMenuItems.StrategyBuilder.Replace(" ", string.Empty))
            {
                try
                {
                    // Latest project configuration
                    ProjectConfiguration = await _projectService.GetProjectConfigurationAsync(ProcessArgs.ProjectId).ConfigureAwait(true);

                    if (!IsTransactionActive
                        && StrategyBuilderProcesses.All(process => process.Message == BuilderProcessStatus.SBNotStarted.GetMetadata().Description))
                    {
                        ResetBuilder();
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    throw;
                }
            }
        });

        public ICommand Stop => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Reset();

            foreach (var process in StrategyBuilderProcesses)
            {
                if (process.Message != BuilderProcessStatus.SBCompleted.GetMetadata().Description
                && process.Message != BuilderProcessStatus.SBNotStarted.GetMetadata().Description)
                {
                    var stopped = BuilderProcessStatus.Stopped.GetMetadata().Description;
                    process.Message = $"{process.Message} - {stopped}";
                }
            }

            CanCancelOrContinue = true;
        }, () => IsTransactionActive && !CanCancelOrContinue)
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => CanCancelOrContinue);

        public ICommand Cancel => new DelegateCommand(() =>
        {
            _cancellationTokenSource.Cancel();
            _manualResetEventSlim.Set();

            CanCancelOrContinue = false;
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        public ICommand Continue => new DelegateCommand(() =>
        {
            _manualResetEventSlim.Set();

            var stopped = BuilderProcessStatus.Stopped.GetMetadata().Description;
            foreach (var process in StrategyBuilderProcesses)
            {
                process.Message = process.Message.Replace($" - {stopped}", string.Empty);
            }
            CanCancelOrContinue = false;
        }, () => CanCancelOrContinue).ObservesProperty(() => CanCancelOrContinue);

        public ICommand Process => new DelegateCommand(async () =>
        {
            try
            {
                var validator = Validate(new StrategyBuilderValidator());
                if (!validator.IsValid)
                {
                    MessageHelper.ShowMessages(this,
                        EntityTypeEnum.StrategyBuilder.GetMetadata().Description,
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                var deleteAll = await MessageHelper.ShowMessageInput(this,
                    "Strategy Builder",
                    "Starting a new process will delete all the Winning Nodes, Assembly Nodes and Strategy Nodes.\n"
                    + "Do you want to continue?").ConfigureAwait(true);

                if (deleteAll == MessageDialogResult.Affirmative)
                {
                    ResetBuilder();

                    StrategyBuilder.WinningNodesUP.Clear();
                    StrategyBuilder.WinningNodesDOWN.Clear();

                    // Delete Strategy Builder Nodes
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectStrategyBuilderNodesUPDirectory(), "*.xml", isBackup: false);
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDOWNDirectory(), "*.xml", isBackup: false);
                    // Delete Strategy Builder Extractions
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectStrategyBuilderExtractorWithoutScheduleDirectory(), isBackup: false);
                    // Delete Assembly Builder Nodes
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderNodesUPDirectory(), "*.xml", isBackup: false);
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderNodesDOWNDirectory(), "*.xml", isBackup: false);
                    // Delete Assembly Builder Extractions
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory("up"), isBackup: false);
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectAssemblyBuilderExtractorWithoutScheduleDirectory("down"), isBackup: false);
                    // Delete Crossing Builder Nodes
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectCrossingBuilderNodesDirectory(), "*.xml", option: SearchOption.AllDirectories, isBackup: false);
                    // Delete Crossing Builder Extractions
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory("up"), isBackup: false);
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory("down"), isBackup: false);

                }
                else
                {
                    return;
                }

                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                _eventAggregator.GetEvent<StrategyBuilderCompletedEvent>().Publish(false);
                _eventAggregator.GetEvent<AssemblyBuilderCompletedEvent>().Publish(false);

                _cancellationTokenSource = new();

                // Historical Data

                var projectHistoricalData = await _marketDataService.GetHistoricalDataAsync(ProjectConfiguration.HistoricalDataId.Value, true);
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
                .ThenBy(candle => candle.Time);

                await Task.Factory.StartNew(() =>
                {
                    ExtractionProcess(allProjectCandles);
                    BacktestProcess(GetBacktestNodes(), allProjectCandles);
                }, _cancellationTokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);

                // Correlation Analysis

                foreach (var process in StrategyBuilderProcesses)
                {
                    process.Message = BuilderProcessStatus.ExecutingCorrelation.GetMetadata().Description;
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
                    process.Message = BuilderProcessStatus.SBCompleted.GetMetadata().Description;
                }

                _eventAggregator.GetEvent<StrategyBuilderCompletedEvent>().Publish(true);

                // Results Message

                var msgUP = StrategyBuilder.WinningNodesUP.Count > 0 ? $"{StrategyBuilder.WinningNodesUP.Count} Correlation UP Nodes Found." : "No Correlation UP Nodes Found.";
                var msgDOWN = StrategyBuilder.WinningNodesDOWN.Count > 0 ? $"{StrategyBuilder.WinningNodesDOWN.Count} Correlation DOWN Nodes Found." : "No Correlation DOWN Nodes Found.";

                MessageHelper.ShowMessage(this,
                    CommonResources.StrategyBuilder,
                    $"{MessageResources.StrategyBuilderCompleted}\n\n"
                    + $"{msgUP}\n"
                    + $"{msgDOWN}");
            }
            catch (OperationCanceledException)
            {
                foreach (var strategyBuilderProcess in StrategyBuilderProcesses)
                {
                    strategyBuilderProcess.Message = BuilderProcessStatus.Canceled.GetMetadata().Description;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<StrategyBuilderViewModel>(ex);
                throw;
            }
            finally
            {
                IsTransactionActive = false;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);

                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);


        private void ExtractionProcess(IEnumerable<Candle> projectCandles)
        {
            Parallel.ForEach(
                StrategyBuilderProcesses,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = MaxParallelism,
                    CancellationToken = _cancellationTokenSource.Token
                },
                process =>
                {
                    process.Message = BuilderProcessStatus.ExecutingExtraction.GetMetadata().Description;

                    var indicators = _extractorService.BuildIndicatorsFromCSV(process.ExtractionTemplatePath);
                    var extractionResult = _extractorService.DoExtraction(
                        ProjectConfiguration.FromDateIS.Value,
                        ProjectConfiguration.ToDateIS.Value,
                        indicators,
                        projectCandles.ToList(),
                        ProjectConfiguration.TimeframeId,
                        ProjectConfiguration.ExtractorMinVariation);

                    var timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss", CultureInfo.InvariantCulture);
                    var nameSignature = process.ExtractionTemplateName.Replace(".csv", string.Empty);

                    _extractorService.ExtractorWrite(
                        ProcessArgs.ProjectName.ProjectStrategyBuilderExtractorWithoutScheduleDirectory($"{nameSignature}.{timeSignature}.csv"),
                        extractionResult,
                        0,
                        0);

                    process.ExtractionName = $"{nameSignature}.{timeSignature}.csv";
                    process.ExtractionPath = ProcessArgs.ProjectName.ProjectStrategyBuilderExtractorWithoutScheduleDirectory($"{nameSignature}.{timeSignature}.csv");
                    process.Message = BuilderProcessStatus.ExtractionCompleted.GetMetadata().Description;
                });
        }

        private List<REPTreeNodeModel> GetBacktestNodes()
        {
            var backtestNodes = new List<REPTreeNodeModel>();

            foreach (var process in StrategyBuilderProcesses)
            {
                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                // Weka

                process.Message = BuilderProcessStatus.ExecutingWeka.GetMetadata().Description;

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

                backtestNodes.AddRange(nodes);
                process.BacktestNodes.Clear();
                process.BacktestNodes.AddRange(nodes);

                _manualResetEventSlim.Wait();
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                process.Message = BuilderProcessStatus.WekaCompleted.GetMetadata().Description;
            }

            return backtestNodes;
        }

        private void BacktestProcess(List<REPTreeNodeModel> backtestNodes, IEnumerable<Candle> projectCandles)
        {
            var backtestNodesPartition = Partitioner.Create(backtestNodes, EnumerablePartitionerOptions.NoBuffering);

            Parallel.ForEach(
                backtestNodesPartition,
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
                        process.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Description} of {process.ExecutingBacktests} Nodes";
                    }

                    var winningNode = _strategyBuilderService.BuildBacktestOfNode(
                        node,
                        projectCandles,
                        _mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(ProjectConfiguration),
                        _manualResetEventSlim,
                        _cancellationTokenSource.Token);

                    if (winningNode)
                    {
                        StrategyBuilderService.SerializeNode(EntityTypeEnum.StrategyBuilder, ProcessArgs.ProjectName, node);
                    }

                    lock (_lock)
                    {
                        process.ExecutingBacktests--;
                        process.CompletedBacktests++;
                        process.ProgressCounter++;

                        process.Message = process.CompletedBacktests == process.BacktestNodes.Count
                        ? process.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Description
                        : process.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Description} of {process.ExecutingBacktests} Nodes";
                    }
                });
        }

        private void ResetBuilder()
        {
            StrategyBuilderProcesses.Clear();

            var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
            foreach (var file in extractionTemplates)
            {
                StrategyBuilderProcesses.Add(new BuilderProcess
                {
                    ExtractionTemplatePath = file.FullName,
                    ExtractionTemplateName = file.Name,
                    ExtractionName = file.Name,
                    Message = BuilderProcessStatus.SBNotStarted.GetMetadata().Description,
                    Tree = new(),
                    BacktestNodes = new()
                });
            }
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

        private int _maxParallelism;
        public int MaxParallelism
        {
            get => _maxParallelism;
            set => SetProperty(ref _maxParallelism, value);
        }

        private StrategyBuilderModel _strategyBuilder;
        public StrategyBuilderModel StrategyBuilder
        {
            get => _strategyBuilder;
            set => SetProperty(ref _strategyBuilder, value);
        }

        private ObservableCollection<BuilderProcess> _strategyBuilderProcesses;
        public ObservableCollection<BuilderProcess> StrategyBuilderProcesses
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
