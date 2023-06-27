using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.Modules.CrossingBuilder.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Common.Weka.Services;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Enums.Model;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Model.Common;
using AutoMapper;
using DynamicData;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class CrossingBuilderViewModel : MenuItemViewModel
    {
        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IExtractorService _extractorService;
        private readonly IStrategyBuilderService _strategyBuilderService;

        private readonly IEventAggregator _eventAggregator;
        private readonly IMarketDataServiceAgent _marketDataService;
        private readonly IProjectServiceAgent _projectService;

        private readonly IMapper _mapper;

        public CrossingBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _extractorService = IoC.Get<IExtractorService>();
            _strategyBuilderService = IoC.Get<IStrategyBuilderService>();

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
                    CrossingBuilder.StrategyNodesUP.Clear();
                    CrossingBuilder.StrategyNodesDOWN.Clear();

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

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

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

                if (!IsTransactionActive
                && CrossingBuilderProcessesUP.All(process => process.Message == BuilderProcessStatus.CBNotStarted.GetMetadata().Description)
                && CrossingBuilderProcessesDOWN.All(process => process.Message == BuilderProcessStatus.CBNotStarted.GetMetadata().Description))
                {
                    var extractionTemplates = _projectDirectoryService.GetFilesInPath(ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory());
                    foreach (var file in extractionTemplates)
                    {
                        CrossingBuilderProcessesUP.Add(new BuilderProcess
                        {
                            ExtractionTemplatePath = file.FullName,
                            ExtractionTemplateName = file.Name,
                            ExtractionName = file.Name,
                            Message = BuilderProcessStatus.CBNotStarted.GetMetadata().Description,
                            Tree = new(),
                            BacktestNodes = new(),
                        });
                        CrossingBuilderProcessesDOWN.Add(new BuilderProcess
                        {
                            ExtractionTemplatePath = file.FullName,
                            ExtractionTemplateName = file.Name,
                            ExtractionName = file.Name,
                            Message = BuilderProcessStatus.CBNotStarted.GetMetadata().Description,
                            Tree = new(),
                            BacktestNodes = new(),
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

        public ICommand Process => new DelegateCommand(async () =>
        {
            // TEST CROSSING BUILDER OF VARIOUS NODES
            // FIND A WAY TO DISPLAY THE STRATEGY NODES IN THE UI
            // FIND A WAY TO DISPLAY THE SAME EXTRACTIONS EXECUITNG FOR DIFFERENT ASSEMBLED NODES
            // FIND A WAY TO DISPLAY THE NUMBER OF CROSSING BUILDS COMPLETED

            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                var crossingHistoricalData = await _marketDataService.GetHistoricalDataAsync(CrossingHistoricalDataId, true).ConfigureAwait(true);
                var crossingCandles = crossingHistoricalData.HistoricalDataCandles.Select(candle => new Candle
                {
                    Date = candle.StartDate,
                    Time = candle.StartTime,

                    Open = candle.Open,
                    High = candle.High,
                    Low = candle.Low,
                    Close = candle.Close,

                    Volume = candle.Volume,
                    Spread = candle.Spread
                })
                .OrderBy(d => d.Date)
                .ThenBy(d => d.Time);

                var mainHistoricalData = await _marketDataService.GetHistoricalDataAsync(ProjectConfiguration.HistoricalDataId.Value, true).ConfigureAwait(true);
                var mainCandles = mainHistoricalData.HistoricalDataCandles.Select(candle => new Candle
                {
                    Date = candle.StartDate,
                    Time = candle.StartTime,

                    Open = candle.Open,
                    High = candle.High,
                    Low = candle.Low,
                    Close = candle.Close,

                    Volume = candle.Volume,
                    Spread = candle.Spread
                })
                .OrderBy(d => d.Date)
                .ThenBy(d => d.Time);

                await Task.Factory.StartNew(() =>
                {


                    CrossingBuilderExecution(CrossingBuilderProcessesUP, "up");
                    CrossingBuilderExecution(CrossingBuilderProcessesDOWN, "down");

                    void CrossingBuilderExecution(IList<BuilderProcess> processes, string label)
                    {
                        // Go through each Extractor Template

                        foreach (var process in processes)
                        {
                            // Go through each Strategy Node (or Assembly Node)

                            foreach (var strategyNode in (label.ToLowerInvariant() == "up" ? CrossingBuilder.StrategyNodesUP : CrossingBuilder.StrategyNodesDOWN))
                            {
                                var backtestOperations = strategyNode.CrossingNodes.Count > 0
                                ? strategyNode.CrossingNodes.Last().Item1.BacktestIS.BacktestOperations
                                : strategyNode.MainNode.ParentNode.BacktestIS.BacktestOperations;

                                // ADD A COUNTER OR SOMETHING FOR THE STRATEGY OR ASSEMBLY NODE BEING TESTED
                                // DO NOT REMOVE ALREADY BACKTESTED NODES FROM THE SAME TEMPLATE, MANTAIN BUT
                                // SHOWING IT IS FROM A DIFFERENT STRATEGY NODE

                                process.Message = BuilderProcessStatus.ExecutingExtraction.GetMetadata().Description;

                                // Perfrom extraction
                                var indicators = _extractorService.BuildIndicatorsFromCSV(process.ExtractionTemplatePath);
                                var extractionResult = _extractorService.DoExtraction(
                                    backtestOperations.First().Date,
                                    backtestOperations.Last().Date,
                                    indicators,
                                    crossingCandles.ToList(),
                                    ProjectConfiguration.TimeframeId);

                                // Filter the extraction for only the crossingCandles with backtest operations
                                var filter = (from il in extractionResult[0].IntervalLabels.Select((_il, _idx) => new { _idx, _il })
                                              let backtestOperation = backtestOperations.Where(operation => operation.Date == il._il.Interval)
                                              where backtestOperation.Any()
                                              select new
                                              {
                                                  idx = il._idx,
                                                  il = new IntervalLabel
                                                  {
                                                      Interval = il._il.Interval,
                                                      Label = backtestOperation.Any(operation => operation.IsWinner) ? "UP" : "DOWN"
                                                  },
                                              }).ToList();


                                foreach (var extraction in extractionResult)
                                {
                                    extraction.IntervalLabels = filter.Select(a => a.il).ToArray();

                                    var outputExtraction = new List<double>();
                                    foreach (var idx in filter.Select(a => a.idx))
                                    {
                                        outputExtraction.Add(extraction.Output[idx]);
                                    }

                                    extraction.Output = outputExtraction.ToArray();
                                }

                                var timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss", CultureInfo.InvariantCulture);
                                var nameSignature = process.ExtractionTemplateName.Replace(".csv", string.Empty);

                                _extractorService.ExtractorWrite(
                                    ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory(label, $"{nameSignature}.{timeSignature}.csv"),
                                    extractionResult,
                                    0,
                                    0);

                                process.ExtractionName = $"{nameSignature}.{timeSignature}.csv";
                                process.ExtractionPath = ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory(label, $"{nameSignature}.{timeSignature}.csv");
                                process.Message = BuilderProcessStatus.ExtractionCompleted.GetMetadata().Description;

                                // Generate Weka Tree

                                process.Message = BuilderProcessStatus.ExecutingWeka.GetMetadata().Description;

                                var wekaApi = new WekaApiClient();
                                var responseWeka = wekaApi.GetREPTreeClassifier(
                                        process.ExtractionPath,
                                        ProjectConfiguration.DepthWeka,
                                        ProjectConfiguration.TotalDecimalWeka,
                                        ProjectConfiguration.MinimalSeed,
                                        ProjectConfiguration.MaximumSeed,
                                        ProjectConfiguration.TotalInstanceWeka,
                                        (double)ProjectConfiguration.ABWekaMaxRatioTree,
                                        (double)ProjectConfiguration.ABWekaNTotalTree);

                                process.Tree = responseWeka[0];

                                // Get Backtest Nodes

                                // UP   ->  WINNER
                                // DOWN ->  LOSER
                                var nodes = process.Tree.NodeOutput.Where(node => node.Winner && node.Label.ToLowerInvariant() == "up")
                                    .Select(node =>
                                    {
                                        node.Node = node.Node.OrderByDescending(node => node).ToList();
                                        node.Label = label.ToUpperInvariant();
                                        return node;
                                    }).ToList();

                                process.BacktestNodes.Clear();
                                process.BacktestNodes.AddRange(nodes);

                                process.Message = BuilderProcessStatus.WekaCompleted.GetMetadata().Description;

                                // Perform Backtest

                                foreach (var backtestingNode in process.BacktestNodes)
                                {
                                    process.ExecutingBacktests++;
                                    process.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Description} of {process.ExecutingBacktests} Nodes";

                                    var winningNode = _strategyBuilderService.BuildBacktestOfCrossingNode(
                                        strategyNode,
                                        backtestingNode,
                                        mainCandles,
                                        crossingCandles,
                                        _mapper.Map<ProjectConfigurationVM, ProjectConfigurationDTO>(ProjectConfiguration),
                                        null,
                                        CancellationToken.None);

                                    if (winningNode)
                                    {
                                        strategyNode.CrossingNodes.Add((backtestingNode, crossingCandles.ToList()));
                                    }

                                    process.ExecutingBacktests--;
                                    process.CompletedBacktests++;
                                    process.ProgressCounter++;

                                    process.Message = process.CompletedBacktests == process.BacktestNodes.Count
                                    ? process.Message = BuilderProcessStatus.BacktestCompleted.GetMetadata().Description
                                    : process.Message = $"{BuilderProcessStatus.ExecutingBacktest.GetMetadata().Description} of {process.ExecutingBacktests} Nodes";
                                }
                            }
                        }
                    }
                });

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
