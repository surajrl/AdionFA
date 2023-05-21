using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Model.Extractor;
using AdionFA.UI.Station.Project.Services;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AdionFA.UI.Station.Project.Validators.Extractor;
using AdionFA.UI.Station.Project.Model.Configuration;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.Infrastructure.Common.Managements;

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

        private ProjectVM _project;

        public ExtractorViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            _projectDirectoryService = IoC.Get<IProjectDirectoryService>();
            _extractorService = IoC.Get<IExtractorService>();

            _appProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _historicalDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Subscribe(p => CanExecute = p);

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            try
            {
                if (item == HamburgerMenuItems.Extractor.Replace(" ", string.Empty))
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
        }, (s) => true);

        public DelegateCommand ProcessBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                // Validator

                if (!Validate(new ExtractorValidator()).IsValid)
                {
                    IsTransactionActive = false;
                    return;
                }

                // Process

                if (_projectDirectoryService.DeleteAllFiles(_project.ProjectName.ProjectExtractorWithoutScheduleDirectory()) &&
                    _projectDirectoryService.DeleteAllFiles(_project.ProjectName.ProjectExtractorAmericaDirectory()) &&
                    _projectDirectoryService.DeleteAllFiles(_project.ProjectName.ProjectExtractorEuropeDirectory()) &&
                    _projectDirectoryService.DeleteAllFiles(_project.ProjectName.ProjectExtractorAsiaDirectory()))
                {
                    IsTransactionActive = true;
                    _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                    var projectConfig = await _appProjectService.GetProjectConfiguration(_project.ProjectId, true);
                    var projectHistoricalData = await _historicalDataService.GetHistoricalData(projectConfig.HistoricalDataId.Value, true);

                    // Gets all the candles from the corresponding historical data
                    IEnumerable<Candle> candles = projectHistoricalData.HistoricalDataCandles
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
                    .ThenBy(candle => candle.Time)
                    .ToList();

                    var taskPool = new List<Task>();
                    foreach (var extractionProcess in ExtractionProcessList)
                    {
                        taskPool.Add(Task.Run(() =>
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
                                var extractions = _extractorService.DoExtraction(StartDate.Value, EndDate.Value, indicators, candles.ToList(), projectConfig.TimeframeId, projectConfig.Variation);
                                //-------------------------------------

                                extractionProcess.Message = "Writing Extraction File";
                                var timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss");
                                var nameSignature = extractionProcess.TemplateName.Replace(".csv", string.Empty);

                                _extractorService.ExtractorWrite(
                                    _project.ProjectName.ProjectExtractorWithoutScheduleDirectory($"{nameSignature}.{timeSignature}.csv"),
                                    extractions,
                                    0,
                                    0);

                                extractionProcess.ExtractionName = $"{nameSignature}.{timeSignature}.csv";

                                if (!projectConfig.WithoutSchedule)
                                {
                                    // America
                                    extractionProcess.Message = "Writing Extraction File America";
                                    _extractorService.ExtractorWrite(
                                        _project.ProjectName.ProjectExtractorAmericaDirectory($"{nameSignature}.{timeSignature}.America.csv"),
                                        indicators,
                                        projectConfig.FromTimeInSecondsAmerica.Hour, projectConfig.ToTimeInSecondsAmerica.Hour
                                    );

                                    // Europe
                                    extractionProcess.Message = "Writing Extraction File Europe";
                                    _extractorService.ExtractorWrite(
                                        _project.ProjectName.ProjectExtractorEuropeDirectory($"{nameSignature}.{timeSignature}.Europe.csv"),
                                        indicators,
                                        projectConfig.FromTimeInSecondsEurope.Hour, projectConfig.ToTimeInSecondsEurope.Hour
                                    );

                                    // Asia
                                    extractionProcess.Message = "Writing Extraction File Asia";
                                    _extractorService.ExtractorWrite(
                                        _project.ProjectName.ProjectExtractorAsiaDirectory($"{nameSignature}.{timeSignature}.Asia.csv"),
                                        indicators,
                                        projectConfig.FromTimeInSecondsAsia.Hour, projectConfig.ToTimeInSecondsAsia.Hour
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
                        }));
                    }

                    await Task.WhenAll(taskPool);

                    IsTransactionActive = false;

                    var result = !ExtractionProcessList.Any(e => e.Status != ExtractorStatusEnum.Completed.GetMetadata().Name);

                    var msg = result ? MessageResources.ExtractionCompleted : MessageResources.EntityErrorTransaction;
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
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public async void PopulateViewModel()
        {
            try
            {
                _project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true);
                ExtractorPath = ProcessArgs.ProjectName.ProjectExtractorDirectory();

                ProjectConfigurationVM pcVM = _project?.ProjectConfigurations.FirstOrDefault();
                Symbol = ((CurrencyPairEnum)pcVM?.SymbolId).GetMetadata()?.Name;

                var symbolVM = await _historicalDataService.GetSymbol(pcVM.SymbolId).ConfigureAwait(true);
                Symbol = symbolVM.Name;

                var timeframeVM = await _historicalDataService.GetTimeframe(pcVM.TimeframeId).ConfigureAwait(true);
                Timeframe = timeframeVM.Name;

                Variation = pcVM?.Variation ?? 0;
                WithoutSchedule = pcVM?.WithoutSchedule ?? false;
                StartDate = pcVM.FromDateIS;
                EndDate = pcVM.ToDateIS;

                HistoricalData = await _historicalDataService.GetHistoricalData(historicalDataId: pcVM?.HistoricalDataId ?? 0).ConfigureAwait(true);

                if (!IsTransactionActive)
                {
                    ExtractionProcessList = new ObservableCollection<ExtractionProcessModel>();

                    string templatePath = ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory();
                    _projectDirectoryService.GetFilesInPath(templatePath).ToList().ForEach(fi =>
                    {
                        ExtractionProcessList.Add(new ExtractionProcessModel
                        {
                            TemplateName = fi.Name,
                            Path = fi.FullName,
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

        // Bindable Model

        private bool istransactionActive;

        public bool IsTransactionActive
        {
            get => istransactionActive;
            set => SetProperty(ref istransactionActive, value);
        }

        private bool canExecute = false;

        public bool CanExecute
        {
            get => canExecute;
            set => SetProperty(ref canExecute, value);
        }

        private DateTime? startDate;

        public DateTime? StartDate
        {
            get => startDate;
            set => SetProperty(ref startDate, value);
        }

        private DateTime? endDate;

        public DateTime? EndDate
        {
            get => endDate;
            set => SetProperty(ref endDate, value);
        }

        private decimal variation;

        public decimal Variation
        {
            get => variation;
            set => SetProperty(ref variation, value);
        }

        private bool withoutSchedule;

        public bool WithoutSchedule
        {
            get => withoutSchedule;
            set => this.SetProperty(ref withoutSchedule, value);
        }

        private string extractorPath;

        public string ExtractorPath
        {
            get => extractorPath;
            set => SetProperty(ref extractorPath, value);
        }

        private string symbol;

        public string Symbol
        {
            get => symbol;
            set => SetProperty(ref symbol, value);
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

        private ObservableCollection<ExtractionProcessModel> extractionProcessList;

        public ObservableCollection<ExtractionProcessModel> ExtractionProcessList
        {
            get => extractionProcessList;
            set => SetProperty(ref extractionProcessList, value);
        }
    }
}