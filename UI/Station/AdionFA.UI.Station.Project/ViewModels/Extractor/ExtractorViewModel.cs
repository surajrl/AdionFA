using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Model.Extractor;
using AdionFA.UI.Station.Project.Services;
using AdionFA.UI.Station.Project.Validators.Extractor;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class ExtractorViewModel : MenuItemViewModel
    {
        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IExtractorService _extractorService;
        private readonly IAppProjectService _appProjectService;

        private readonly IProjectServiceAgent _projectService;
        private readonly IMarketDataServiceAgent _historicalDataService;

        private readonly IEventAggregator _eventAggregator;

        public ExtractorViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _extractorService = IoC.Get<IExtractorService>();

            _appProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _historicalDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            ExtractionProcessList = new ObservableCollection<ExtractionProcessModel>();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.Extractor.Replace(" ", string.Empty))
            {
                PopulateViewModel();
            }
        });

        public ICommand RefreshBtnCommand => new DelegateCommand(() =>
        {
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectExtractorWithoutScheduleDirectory(), isBackup: false);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectExtractorAmericaDirectory(), isBackup: false);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectExtractorEuropeDirectory(), isBackup: false);
            _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectExtractorAsiaDirectory(), isBackup: false);

            PopulateViewModel();
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public ICommand ProcessBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                // Validator

                if (!Validate(new ExtractorValidator()).IsValid)
                {
                    return;
                }

                // Process

                if (_projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectExtractorWithoutScheduleDirectory(), isBackup: false) &&
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectExtractorAmericaDirectory(), isBackup: false) &&
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectExtractorEuropeDirectory(), isBackup: false) &&
                    _projectDirectoryService.DeleteAllFiles(ProcessArgs.ProjectName.ProjectExtractorAsiaDirectory(), isBackup: false))
                {
                    IsTransactionActive = true;
                    _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                    ExtractionProcessList.Clear();

                    var templatePath = ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory();
                    _projectDirectoryService.GetFilesInPath(templatePath).ToList().ForEach(file =>
                    {
                        ExtractionProcessList.Add(new ExtractionProcessModel
                        {
                            TemplateName = file.Name,
                            Path = file.FullName,
                            Status = ExtractorStatusEnum.NoStarted.GetMetadata().Name,
                        });
                    });

                    var projectConfiguration = await _appProjectService.GetProjectConfiguration(ProcessArgs.ProjectId, true);
                    var projectHistoricalData = await _historicalDataService.GetHistoricalData(projectConfiguration.HistoricalDataId.Value, true);

                    var candles = projectHistoricalData.HistoricalDataCandles
                    .Select(hdCandle => new Candle
                    {
                        Date = hdCandle.StartDate,
                        Time = hdCandle.StartTime,
                        Open = hdCandle.Open,
                        High = hdCandle.High,
                        Low = hdCandle.Low,
                        Close = hdCandle.Close,
                        Volume = hdCandle.Volume
                    })
                    .OrderBy(candle => candle.Date)
                    .ThenBy(candle => candle.Time).ToList();

                    await Task.Run(() =>
                    {
                        Parallel.ForEach(
                            ExtractionProcessList,
                            new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount - 1 },
                            extractionProcess =>
                            {
                                try
                                {
                                    // Beginning --------------------------
                                    var beginning = ExtractorStatusEnum.Beginning.GetMetadata();
                                    extractionProcess.Status = beginning.Name;
                                    extractionProcess.Message = beginning.Description;
                                    //-------------------------------------

                                    extractionProcess.Message = "Building Indicators";
                                    var indicators = _extractorService.BuildIndicatorsFromCSV(extractionProcess.Path);

                                    // Executing --------------------------
                                    var executing = ExtractorStatusEnum.Executing.GetMetadata();
                                    extractionProcess.Status = executing.Name;
                                    extractionProcess.Message = executing.Description;
                                    var extractions = _extractorService.DoExtraction(
                                        StartDate.Value,
                                        EndDate.Value,
                                        indicators,
                                        candles.ToList(),
                                        projectConfiguration.TimeframeId,
                                        projectConfiguration.ExtractorMinVariation);
                                    //-------------------------------------

                                    extractionProcess.Message = "Writing Extraction File";
                                    var timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss", CultureInfo.InvariantCulture);
                                    var nameSignature = extractionProcess.TemplateName.Replace(".csv", string.Empty);

                                    _extractorService.ExtractorWrite(
                                        ProcessArgs.ProjectName.ProjectExtractorWithoutScheduleDirectory($"{nameSignature}.{timeSignature}.csv"),
                                        extractions,
                                        0,
                                        0);

                                    extractionProcess.ExtractionName = $"{nameSignature}.{timeSignature}.csv";

                                    if (!projectConfiguration.WithoutSchedule)
                                    {
                                        // America

                                        extractionProcess.Message = "Writing Extraction File America";
                                        _extractorService.ExtractorWrite(
                                            ProcessArgs.ProjectName.ProjectExtractorAmericaDirectory($"{nameSignature}.{timeSignature}.America.csv"),
                                            indicators,
                                            projectConfiguration.FromTimeInSecondsAmerica.Hour, projectConfiguration.ToTimeInSecondsAmerica.Hour
                                        );

                                        // Europe

                                        extractionProcess.Message = "Writing Extraction File Europe";
                                        _extractorService.ExtractorWrite(
                                            ProcessArgs.ProjectName.ProjectExtractorEuropeDirectory($"{nameSignature}.{timeSignature}.Europe.csv"),
                                            indicators,
                                            projectConfiguration.FromTimeInSecondsEurope.Hour, projectConfiguration.ToTimeInSecondsEurope.Hour
                                        );

                                        // Asia

                                        extractionProcess.Message = "Writing Extraction File Asia";
                                        _extractorService.ExtractorWrite(
                                            ProcessArgs.ProjectName.ProjectExtractorAsiaDirectory($"{nameSignature}.{timeSignature}.Asia.csv"),
                                            indicators,
                                            projectConfiguration.FromTimeInSecondsAsia.Hour, projectConfiguration.ToTimeInSecondsAsia.Hour
                                        );
                                    }

                                    var completed = ExtractorStatusEnum.Completed.GetMetadata();
                                    extractionProcess.Status = completed.Name;
                                    extractionProcess.Message = completed.Description;
                                }
                                catch (Exception ex)
                                {
                                    extractionProcess.Message = ex.Message;
                                    extractionProcess.Status = ExtractorStatusEnum.Error.GetMetadata().Name;
                                }
                            });
                    });

                    IsTransactionActive = false;

                    var result = !ExtractionProcessList.Any(extractionProcess => extractionProcess.Status != ExtractorStatusEnum.Completed.GetMetadata().Name);

                    var msg = result
                    ? MessageResources.ExtractionCompleted
                    : MessageResources.EntityErrorTransaction;

                    MessageHelper.ShowMessage(this, CommonResources.Extractor, msg);
                }
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;

                Trace.TraceError(ex.Message);
                throw;
            }
            finally
            {
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public async void PopulateViewModel()
        {
            try
            {
                var project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true);
                ExtractorPath = ProcessArgs.ProjectName.ProjectExtractorDirectory();

                var projectConfigurationVM = project?.ProjectConfigurations.FirstOrDefault();
                Symbol = ((CurrencyPairEnum)projectConfigurationVM?.SymbolId).GetMetadata()?.Name;

                var symbolVM = await _historicalDataService.GetSymbol(projectConfigurationVM.SymbolId).ConfigureAwait(true);
                Symbol = symbolVM.Name;

                var timeframeVM = await _historicalDataService.GetTimeframe(projectConfigurationVM.TimeframeId).ConfigureAwait(true);
                Timeframe = timeframeVM.Name;

                Variation = projectConfigurationVM?.ExtractorMinVariation ?? 0;
                WithoutSchedule = projectConfigurationVM?.WithoutSchedule ?? false;
                StartDate = projectConfigurationVM.FromDateIS;
                EndDate = projectConfigurationVM.ToDateIS;

                HistoricalData = await _historicalDataService.GetHistoricalData(historicalDataId: projectConfigurationVM?.HistoricalDataId ?? 0).ConfigureAwait(true);

                if (!IsTransactionActive)
                {
                    ExtractionProcessList.Clear();

                    var templatePath = ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory();
                    _projectDirectoryService.GetFilesInPath(templatePath).ToList().ForEach(file =>
                    {
                        ExtractionProcessList.Add(new ExtractionProcessModel
                        {
                            TemplateName = file.Name,
                            Path = file.FullName,
                            Status = ExtractorStatusEnum.NoStarted.GetMetadata().Name,
                        });
                    });
                }

                CanExecuteValidate(true);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private List<string> CanExecuteValidate(bool showDialogWithErrors = false)
        {
            List<string> errors = Array.Empty<string>().ToList();

            // Validation Rules

            if (string.IsNullOrEmpty(Symbol) || string.IsNullOrEmpty(Timeframe))
            {
                errors.Add(MessageResources.SetCurrencyPairAndPeriodFromConfiguration);
            }

            if (string.IsNullOrEmpty(ExtractorPath))
            {
                errors.Add(MessageResources.SetValidDirectoryFromConfiguration);
            }

            if (StartDate == null || EndDate == null)
            {
                errors.Add(MessageResources.SetValidDateRangeFromConfiguration);
            }

            if (HistoricalData == null)
            {
                errors.Add(MessageResources.SetDataHistoryFromConfiguration);
            }

            CanExecute = !errors.Any();

            if (errors.Any() && showDialogWithErrors)
            {
                MessageHelper.ShowMessages(this,
                    EntityTypeEnum.Extractor.GetMetadata().Description,
                    errors.ToArray());
            }

            return errors;
        }

        // View Bindings

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }

        private bool _canExecute;
        public bool CanExecute
        {
            get => _canExecute;
            set => SetProperty(ref _canExecute, value);
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        private decimal _variation;
        public decimal Variation
        {
            get => _variation;
            set => SetProperty(ref _variation, value);
        }

        private bool _withoutSchedule;
        public bool WithoutSchedule
        {
            get => _withoutSchedule;
            set => SetProperty(ref _withoutSchedule, value);
        }

        private string _extractorPath;

        public string ExtractorPath
        {
            get => _extractorPath;
            set => SetProperty(ref _extractorPath, value);
        }

        private string _symbol;
        public string Symbol
        {
            get => _symbol;
            set => SetProperty(ref _symbol, value);
        }

        private string _timeframe;
        public string Timeframe
        {
            get => _timeframe;
            set => SetProperty(ref _timeframe, value);
        }

        private HistoricalDataVM _historicalData;
        public HistoricalDataVM HistoricalData
        {
            get => _historicalData;
            set => SetProperty(ref _historicalData, value);
        }

        private ObservableCollection<ExtractionProcessModel> _extractionProcessList;
        public ObservableCollection<ExtractionProcessModel> ExtractionProcessList
        {
            get => _extractionProcessList;
            set => SetProperty(ref _extractionProcessList, value);
        }
    }
}