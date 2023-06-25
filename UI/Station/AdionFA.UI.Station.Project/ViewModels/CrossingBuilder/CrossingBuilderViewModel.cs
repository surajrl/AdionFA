using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.Modules.CrossingBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums;
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class CrossingBuilderViewModel : MenuItemViewModel
    {
        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IExtractorService _extractorService;

        private readonly IEventAggregator _eventAggregator;
        private readonly IMarketDataServiceAgent _marketDataService;
        private readonly IProjectServiceAgent _projectService;


        public CrossingBuilderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _extractorService = IoC.Get<IExtractorService>();

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
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                // ...

                var crossingHistoricalData = await _marketDataService.GetHistoricalDataAsync(CrossingHistoricalDataId, false).ConfigureAwait(true);
                var candles = crossingHistoricalData.HistoricalDataCandles.Select(candle => new Candle
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
                    // Extractor Template

                    foreach (var process in CrossingBuilderProcessesUP)
                    {
                        // Assembly Node

                        foreach (var strategyNode in CrossingBuilder.StrategyNodesUP)
                        {
                            // Backtest Operations will be those of the last crossing node
                            // added or of the main node if there are no crossing nodes
                            var backtestOperations = strategyNode.MainNode.ParentNode.BacktestIS.BacktestOperations;

                            // Perfrom extraction
                            var indicators = _extractorService.BuildIndicatorsFromCSV(process.ExtractionTemplatePath);
                            var extractionResult = _extractorService.DoExtraction(
                                backtestOperations.First().Date,
                                backtestOperations.Last().Date,
                                indicators,
                                candles.ToList(),
                                ProjectConfiguration.TimeframeId);

                            // Filter the extraction for only the candles with backtest operations
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
                                ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory("UP", $"{nameSignature}.{timeSignature}.csv"),
                                extractionResult,
                                0,
                                0);

                            process.ExtractionName = $"{nameSignature}.{timeSignature}.csv";
                            process.ExtractionPath = ProcessArgs.ProjectName.ProjectCrossingBuilderExtractorWithoutScheduleDirectory("UP", $"{nameSignature}.{timeSignature}.csv");
                            process.Message = BuilderProcessStatus.ExtractionCompleted.GetMetadata().Description;

                            // Generate Weka Tree

                            // Get Backtest Nodes

                            // Perform Backtest
                            // Backtest will take the Strategy Node, and check
                            // for trade signals on the main node and crossing nodes

                            //strategyNode.CrossingNodes.Add(new REPTreeNodeModel { /* Winner Node */ });
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
