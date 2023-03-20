using Adion.FA.Infrastructure.Enums;
using Adion.FA.Infrastructure.Enums.Model;
using Adion.FA.Infrastructure.I18n.Resources;
using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Base;
using Adion.FA.UI.Station.Infrastructure.Helpers;
using Adion.FA.UI.Station.Infrastructure.Services;
using Adion.FA.UI.Station.Module.Dashboard.Model;
using Adion.FA.UI.Station.Module.Dashboard.Services;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace Adion.FA.UI.Station.Module.Dashboard.ViewModels
{
    public class CreateProjectViewModel : ViewModelBase
    {
        #region Services

        private readonly ISettingService SettingService;

        #endregion

        #region Ctor

        public CreateProjectViewModel(
            ISettingService settingService,
            IApplicationCommands applicationCommands)
        {
            SettingService = settingService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        #endregion

        #region Commands

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

                //create/update
                var result = await SettingService.CreateProject(project);

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

        #endregion

        private async void PopulateViewModel()
        {
            if (!IsTransactionActive)
            {
                Project = new CreateProjectModel { ProjectStepId = (int)ProjectStepEnum.InitialConfiguration };
                
                var config = await SettingService.GetGlobalConfiguration();
                project.ConfigurationId = config.ProjectGlobalConfigurationId;
                
                var marketdata = await SettingService.GetAllMarketData();
                MarketData.Clear();
                MarketData.AddRange(marketdata
                    .Select(c => new Metadata
                    {
                        Id = c.MarketId,
                        Name = $"{c.Description}"
                    }).ToList());
            }
        }

        #region Bindable Model

        bool istransactionActive;
        public bool IsTransactionActive
        {
            get { return istransactionActive; }
            set { SetProperty(ref istransactionActive, value); }
        }

        CreateProjectModel project;
        public CreateProjectModel Project
        {
            get => project; 
            set => SetProperty(ref project, value); 
        }

        public ObservableCollection<Metadata> MarketData { get; } = new ObservableCollection<Metadata>();

        #endregion
    }
}
