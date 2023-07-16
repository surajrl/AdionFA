using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Domain.Model;
using AdionFA.Domain.Properties;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Helpers;
using AdionFA.UI.Infrastructure.Services;
using AdionFA.UI.Module.Dashboard.Model;
using AdionFA.UI.Module.Dashboard.Services;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Module.Dashboard.ViewModels
{
    public class CreateProjectViewModel : ViewModelBase
    {
        private readonly ISettingService _settingService;

        public CreateProjectViewModel(
            ISettingService settingService,
            IApplicationCommands applicationCommands)
        {
            _settingService = settingService;

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        private ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutCreateProject))
            {
                PopulateViewModel();
            }
        });

        public ICommand CreateProjectBtnCommand => new DelegateCommand(() =>
        {
            try
            {
                var validator = Project.Validate();
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
                var result = _settingService.CreateProject(Project);

                if (result)
                {
                    ContainerLocator.Current.Resolve<IApplicationCommands>().LoadProjectHierarchyCommand.Execute(null);
                }

                IsTransactionActive = false;

                MessageHelper.ShowMessage(this,
                    EntityTypeEnum.Project.GetMetadata().Description,
                    result
                    ? Resources.SuccessEntitySave
                    : Resources.FailEntitySave);
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;

                Trace.TraceError(ex.Message);
                throw;
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        private void PopulateViewModel()
        {
            if (!IsTransactionActive)
            {
                Project = new CreateProjectModel();

                var config = _settingService.GetConfiguration();
                Project.ConfigurationId = config.ConfigurationId;

                var historicalData = _settingService.GetAllHistoricalData();
                HistoricalData.Clear();
                HistoricalData.AddRange(historicalData
                    .Select(c => new Metadata
                    {
                        Id = c.HistoricalDataId,
                        Name = $"{c.Description}"
                    }).ToList());
            }
        }

        // View Bindings

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }

        private CreateProjectModel _project;
        public CreateProjectModel Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }

        public ObservableCollection<Metadata> HistoricalData { get; } = new ObservableCollection<Metadata>();
    }
}