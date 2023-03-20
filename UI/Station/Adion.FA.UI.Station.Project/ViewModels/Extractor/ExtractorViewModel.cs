using Adion.FA.Infrastructure.Common.Directories.Contracts;
using Adion.FA.Infrastructure.Common.Directories.Services;
using Adion.FA.Infrastructure.Common.Extractor.Contracts;
using Adion.FA.Infrastructure.Common.Extractor.Model;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.Infrastructure.I18n.Resources;
using Adion.FA.UI.Station.Project.Commands;
using Adion.FA.UI.Station.Project.EventAggregator;
using Adion.FA.UI.Station.Project.Features;
using Adion.FA.UI.Station.Project.Model.Extractor;
using Adion.FA.UI.Station.Project.Services;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
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
using Adion.FA.UI.Station.Project.Validators.Extractor;
using Adion.FA.UI.Station.Project.Model.Configuration;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.UI.Station.Infrastructure.Helpers;

namespace Adion.FA.UI.Station.Project.ViewModels
{
    public class ExtractorViewModel : MenuItemViewModel
    {
        #region Services
        private readonly IProjectDirectoryService ProjectDirectoryService;
        private readonly IExtractorService ExtractorService;

        private readonly IAppProjectService AppProjectService;
        private readonly IMarketDataServiceAgent MarketDataService;

        private readonly IEventAggregator eventAggregator;
        #endregion

        private ProjectVM Project;

        #region Ctor
        public ExtractorViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            ProjectDirectoryService = IoC.Get<IProjectDirectoryService>();
            ExtractorService = IoC.Get<IExtractorService>();

            AppProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();
            MarketDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();

            eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Subscribe(p => CanExecute = p);
            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);
        }
        #endregion

        #region Command

        #region SelectItemHamburgerMenuCommand
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
        }, (s) => true); //item => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);
        #endregion

        #region ProcessBtnCommand
        public DelegateCommand ProcessBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                //validator
                if (!Validate(new ExtractorValidator()).IsValid)
                {
                    IsTransactionActive = false;
                    return;
                }

                //process
                if (ProjectDirectoryService.DeleteAllFiles(Project.ProjectName.ProjectExtractorWithoutScheduleDirectory()) &&
                    ProjectDirectoryService.DeleteAllFiles(Project.ProjectName.ProjectExtractorAmericaDirectory()) &&
                    ProjectDirectoryService.DeleteAllFiles(Project.ProjectName.ProjectExtractorEuropeDirectory()) &&
                    ProjectDirectoryService.DeleteAllFiles(Project.ProjectName.ProjectExtractorAsiaDirectory()))
                {
                    IsTransactionActive = true;
                    eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                    ProjectConfigurationSettingModel pconfig = await AppProjectService.GetProjectConfiguration(Project.ProjectId, true);
                    MarketDataVM projectMarketData = await MarketDataService.GetMarketData(pconfig.MarketDataId.Value, true);

                    IEnumerable<Candle> candles = projectMarketData.MarketDataDetails.Select(
                            h => new Candle
                            {
                                date = h.StartDate,
                                time = h.StartTime,
                                open = h.OpenPrice,
                                max = h.MaxPrice,
                                min = h.MinPrice,
                                close = h.ClosePrice,
                                volumen = h.Volumen
                            }
                        ).OrderBy(d => d.date).ThenBy(d => d.time).ToList();

                    bool result = false;
                    Action<object> action = (object e) =>
                    {
                        ExtractionProcessModel model = (ExtractionProcessModel)e;
                        try
                        {
                            //beginning
                            var beginning = ExtractorStatusEnum.Beginning.GetMetadata();
                                model.Status = beginning.Name;
                                model.Message = beginning.Description;
                            
                            //------------------------------------
                            model.Message = "Building Indicators";
                                List<IndicatorBase> indicators = ExtractorService.BuildIndicators(model.Path);
                            
                            //executing-------------------------------------
                            var executing = ExtractorStatusEnum.Executing.GetMetadata();
                                model.Status = executing.Name;
                                model.Message = executing.Description;
                                List<IndicatorBase> extractions = ExtractorService.ExtractorExecute(StartDate.Value, EndDate.Value, indicators, candles, pconfig.CurrencyPeriodId, pconfig.Variation);
                            
                            //-------------------------------------
                            model.Message = "Writing Extraction File";
                                string timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss");
                                string nameSignature = model.TemplateName.Replace(".csv", string.Empty);
                                ExtractorService.ExtractorWrite(
                                    Project.ProjectName.ProjectExtractorWithoutScheduleDirectory($"{nameSignature}.{timeSignature}.csv"),
                                    extractions,
                                    0, 0
                                );

                            model.ExtractionName = $"{nameSignature}.{timeSignature}.csv";

                            if (!pconfig.WithoutSchedule)
                            {
                                //america
                                model.Message = "Writing Extraction File America";
                                    ExtractorService.ExtractorWrite(
                                        Project.ProjectName.ProjectExtractorAmericaDirectory($"{nameSignature}.{timeSignature}.America.csv"),
                                        indicators,
                                        pconfig.FromTimeInSecondsAmerica.Hour, pconfig.ToTimeInSecondsAmerica.Hour
                                    );

                                //europe
                                model.Message = "Writing Extraction File Europe";
                                    ExtractorService.ExtractorWrite(
                                        Project.ProjectName.ProjectExtractorEuropeDirectory($"{nameSignature}.{timeSignature}.Europe.csv"),
                                        indicators,
                                        pconfig.FromTimeInSecondsEurope.Hour, pconfig.ToTimeInSecondsEurope.Hour
                                    );

                                //asia
                                model.Message = "Writing Extraction File Asia";
                                    ExtractorService.ExtractorWrite(
                                        Project.ProjectName.ProjectExtractorAsiaDirectory($"{nameSignature}.{timeSignature}.Asia.csv"),
                                        indicators,
                                        pconfig.FromTimeInSecondsAsia.Hour, pconfig.ToTimeInSecondsAsia.Hour
                                    );
                            }

                            var completed = ExtractorStatusEnum.Completed.GetMetadata();
                            model.Status = completed.Name;
                            model.Message = completed.Description;
                        }
                        catch (Exception ex)
                        {
                            model.Message = ex.Message;
                            model.Status = ExtractorStatusEnum.Error.GetMetadata().Name;
                        }
                    };

                    Task[] pool = ExtractionProcessList.Select(e => new Task(action, e)).ToArray();
                    foreach (var t in pool)
                    {
                        t.Start();
                    }
                    await Task.WhenAll(pool).ConfigureAwait(true);

                    IsTransactionActive = false;
                    result = !ExtractionProcessList.Any(e => e.Status != ExtractorStatusEnum.Completed.GetMetadata().Name);

                    string msg = result ? MessageResources.ExtractionCompleted : MessageResources.EntityErrorTransaction;
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
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);
        #endregion

        #endregion

        public async void PopulateViewModel()
        {
            try
            {
                Project = await AppProjectService.GetProject(ProcessArgs.ProjectId, true);
                ExtractorPath = ProcessArgs.ProjectName.ProjectExtractorDirectory();

                ProjectConfigurationVM pcVM = Project?.ProjectConfigurations.FirstOrDefault();
                CurrencyPair = ((CurrencyPairEnum)pcVM?.CurrencyPairId).GetMetadata()?.Name;
                CurrencyPeriod = ((CurrencyPeriodEnum)pcVM?.CurrencyPeriodId).GetMetadata()?.Name;
                Variation = pcVM?.Variation ?? 0;
                WithoutSchedule = pcVM?.WithoutSchedule ?? false;
                StartDate = pcVM.FromDateIS;
                EndDate = pcVM.ToDateIS;

                MarketData = await AppProjectService.GetMarketData(pcVM?.MarketDataId ?? 0);

                if (!IsTransactionActive)
                {
                    ExtractionProcessList = new ObservableCollection<ExtractionProcessModel>();

                    string templatePath = ProcessArgs.ProjectName.ProjectExtractorTemplatesDirectory();
                    ProjectDirectoryService.GetFilesInPath(templatePath).ToList().ForEach(fi =>
                    {
                        ExtractionProcessList.Add(new ExtractionProcessModel 
                        {
                            TemplateName = fi.Name,
                            Path = fi.FullName,
                            Status = ExtractorStatusEnum.NoStarted.GetMetadata().Name,
                        });
                    });
                }

                await CanExecuteValidate(true);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private async Task<List<string>> CanExecuteValidate(bool showDialogWithErrors = false)
        {
            List<string> errors = Array.Empty<string>().ToList();

            #region Validator Rules
            if (string.IsNullOrEmpty(CurrencyPair) || string.IsNullOrEmpty(CurrencyPeriod))
                errors.Add(MessageResources.SetCurrencyPairAndPeriodFromConfiguration);

            if (string.IsNullOrEmpty(ExtractorPath))
                errors.Add(MessageResources.SetValidDirectoryFromConfiguration);

            if (StartDate == null || EndDate == null)
                errors.Add(MessageResources.SetValidDateRangeFromConfiguration);
            
            if(MarketData == null)
                errors.Add(MessageResources.SetDataHistoryFromConfiguration);
            #endregion

            CanExecute = !errors.Any();

            if (errors.Any() && showDialogWithErrors)
                MessageHelper.ShowMessages(this, 
                    EntityTypeEnum.Extractor.GetMetadata().Description, errors.ToArray());

            return errors;
        }

        #region Bindable Model
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

        private string currencyPair;
        public string CurrencyPair 
        { 
            get => currencyPair; 
            set => SetProperty(ref currencyPair, value); 
        }

        private string currencyPeriod;
        public string CurrencyPeriod
        {
            get => currencyPeriod;
            set => SetProperty(ref currencyPeriod, value);
        }

        private MarketDataVM marketData;
        public MarketDataVM MarketData 
        {
            get => marketData;
            set => SetProperty(ref marketData, value); 
        }

        private ObservableCollection<ExtractionProcessModel> extractionProcessList;
        public ObservableCollection<ExtractionProcessModel> ExtractionProcessList
        {
            get => extractionProcessList;
            set => SetProperty(ref extractionProcessList, value);
        }
        #endregion
    }
}
