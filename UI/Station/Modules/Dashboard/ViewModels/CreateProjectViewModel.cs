using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Enums.Model;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.MarketData;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Module.Dashboard.Model;
using AdionFA.UI.Station.Module.Dashboard.Services;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Module.Dashboard.ViewModels
{
    public class CreateProjectViewModel : ViewModelBase
    {
        private readonly ISettingService _settingService;

        public CreateProjectViewModel(
            ISettingService settingService,
            IApplicationCommands applicationCommands)
        {
            _settingService = settingService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        private ICommand FlyoutCommand { get; set; }

        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutCreateProject))
            {
                PopulateViewModel();
            }
        }

        public DelegateCommand CreateProjectBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                var validator = project.Validate();
                if (!validator.IsValid)
                {
                    IsTransactionActive = false;

                    MessageHelper.ShowMessages(this,
                        EntityTypeEnum.Project.GetMetadata().Description,
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                IsTransactionActive = true;

                // Create / Update
                var result = await _settingService.CreateProject(project);

                if (result)
                {
                    ContainerLocator.Current.Resolve<IApplicationCommands>().LoadProjectHierarchyCommand.Execute(null);
                }

                IsTransactionActive = false;

                MessageHelper.ShowMessage(this,
                    EntityTypeEnum.Project.GetMetadata().Description,
                        result ? MessageResources.EntitySaveSuccess : MessageResources.EntityErrorTransaction);
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                Trace.TraceError(ex.Message);
                throw;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        private async void PopulateViewModel()
        {
            if (!IsTransactionActive)
            {
                Project = new CreateProjectModel
                {
                    ProjectStepId = (int)ProjectStepEnum.InitialConfiguration
                };

                var config = await _settingService.GetGlobalConfiguration();
                project.ConfigurationId = config.ProjectGlobalConfigurationId;

                var historicalData = await _settingService.GetAllHistoricalData();
                HistoricalData.Clear();
                HistoricalData.AddRange(historicalData
                    .Select(c => new Metadata
                    {
                        Id = c.HistoricalDataId,
                        Name = $"{c.Description}"
                    }).ToList());
            }
        }

        // Bindable Model

        private bool _isTransactionActive;

        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }

        private CreateProjectModel project;

        public CreateProjectModel Project
        {
            get => project;
            set => SetProperty(ref project, value);
        }

        public ObservableCollection<Metadata> HistoricalData { get; } = new ObservableCollection<Metadata>();
    }
}