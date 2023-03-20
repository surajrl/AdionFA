using Adion.FA.UI.Station.Module.Dashboard.Model;
using System.Collections.Generic;
using ControlzEx.Theming;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using System.Threading;
using System.Globalization;
using System;
using System.Diagnostics;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.UI.Station.Infrastructure.Model.Common;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.UI.Station.Infrastructure.Services;
using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Base;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using Adion.FA.Infrastructure.Common.Directories.Contracts;
using Adion.FA.UI.Station.Infrastructure.Contracts.Services;
using Adion.FA.UI.Station.Infrastructure.Helpers;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Common.Managements;
using Adion.FA.Infrastructure.Common.Directories.Services;

namespace Adion.FA.UI.Station.Module.Dashboard.ViewModels
{
    public class AppSettingViewModel : ViewModelBase
    {
        #region Services

        private readonly IProjectDirectoryService DirectoryService;

        private readonly ISharedServiceAgent SharedService;
        private readonly IProjectServiceAgent ProjectService;
        private readonly IProcessService ProcessService;

        #endregion

        #region Ctor

        public AppSettingViewModel(
            IApplicationCommands applicationCommands,
            ISharedServiceAgent sharedService,
            IProjectServiceAgent projectService,
            IProcessService processService)
        {
            //Infra Common
            DirectoryService = IoC.Get<IProjectDirectoryService>();

            //Infra UI
            SharedService = sharedService;
            ProjectService = projectService;
            ProcessService = processService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        #endregion

        #region Commands

        private ICommand FlyoutCommand { get; set; }
        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutAppSetting))
            {
                PopulateViewModel();
            }
        }

        public DelegateCommand UploadDefaultWorkspaceBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                if (!ProcessService.AnyProcessProject())
                {
                    var result = await SharedService.UpdateAppSetting(new SettingVM
                    {
                        SettingId = (int)SettingEnum.DefaultWorkspace,
                        Key = SettingEnum.DefaultWorkspace.GetMetadata().Name,
                        Value = DefaultWorkspace
                    });

                    if (result.IsSuccess)
                    {
                        if (DirectoryService.CreateDefaultWorkspace())
                        {
                            IList<ProjectVM> projects = await ProjectService.GetAllProjects();
                            foreach (var p in projects)
                            {
                                DirectoryService.CreateDefaultProjectWorkspace(p.ProjectName);
                            }
                            MessageHelper.ShowMessage(this, nameof(EntityTypeEnum.Setting), "Default Workspace was updated.");
                        }
                    }
                }
                else
                {
                    MessageHelper.ShowMessage(this, 
                        EntityTypeEnum.Setting.GetMetadata().Description, 
                            "Close all running Projects to run the operation.");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }, () => true);
        
        #endregion

        private async void PopulateViewModel() 
        {
            ApplicationThemes = ThemeManager.Current.BaseColors.Select(a => new ApplicationTheme
            {
                Name = a,
            }).ToList();

            AccentColors = ThemeManager.Current.ColorSchemes.Select(a => new AccentColor
            {
                Name = a
            }).ToList();

            var culturesAvailables = AppSettingsManager.Instance.Get<AppSettings>().Cultures?.Split(",");
            Cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures)
                .Where(c => culturesAvailables.Any(ca => ca == c.ThreeLetterISOLanguageName)).ToList();

            var themeSetting = await SharedService.GetSettingAsync((int)SettingEnum.Theme);
            SelectedTheme = themeSetting != null 
                ? ApplicationThemes.FirstOrDefault(th => th.Name == themeSetting.Value) : ApplicationThemes.FirstOrDefault();

            var colorSetting = await SharedService.GetSettingAsync((int)SettingEnum.Color);
            SelectedAccentColor = colorSetting != null 
                ? AccentColors.FirstOrDefault(ac => ac.Name == colorSetting.Value) : AccentColors.FirstOrDefault();

            var cultureSetting = await SharedService.GetSettingAsync((int)SettingEnum.Culture);
            Culture = cultureSetting != null 
                ? Cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == cultureSetting.Value) 
                    : Cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == "eng");

            var df = await SharedService.GetSettingAsync((int)SettingEnum.DefaultWorkspace);
            DefaultWorkspace = (df?.Value ?? string.Empty).Length > 0 ? df.Value : ProjectDirectoryManager.DefaultDirectory();
        }

        #region Properties

        private IList<CultureInfo> cultures;
        public IList<CultureInfo> Cultures 
        { 
            get => cultures; 
            set => SetProperty(ref cultures, value);
        }

        private IList<ApplicationTheme> applicationsThemes;
        public IList<ApplicationTheme> ApplicationThemes
        {
            get { return applicationsThemes; }
            set { SetProperty(ref applicationsThemes, value); }
        }


        private IList<AccentColor> accentColors;
        public IList<AccentColor> AccentColors
        {
            get { return accentColors; }
            set { SetProperty(ref accentColors, value); }
        }

        private CultureInfo culture;
        public CultureInfo Culture 
        { 
            get => culture;
            set
            {
                if (SetProperty(ref culture, value) && value != null)
                {
                    Thread.CurrentThread.CurrentCulture = Culture;
                    Thread.CurrentThread.CurrentUICulture = Culture;
                    SharedService.UpdateAppSetting(new SettingVM 
                    { 
                        SettingId = (int)SettingEnum.Culture,
                        Key = SettingEnum.Culture.GetMetadata().Name,
                        Value = Culture.ThreeLetterISOLanguageName 
                    });
                }
            }
        }

        private ApplicationTheme selectedTheme;
        public ApplicationTheme SelectedTheme
        {
            get { return selectedTheme; }
            set
            {
                if (SetProperty(ref selectedTheme, value) && value != null)
                {
                    ThemeManager.Current.ChangeThemeBaseColor(Application.Current, value.Name);
                    SharedService.UpdateAppSetting(new SettingVM 
                    { 
                        SettingId = (int)SettingEnum.Theme,
                        Key = SettingEnum.Theme.GetMetadata().Name,
                        Value = value.Name 
                    });
                }
            }
        }

        private AccentColor selectedAccentColor;
        public AccentColor SelectedAccentColor
        {
            get { return selectedAccentColor; }
            set
            {
                if (SetProperty(ref selectedAccentColor, value) && value != null)
                {
                    ThemeManager.Current.ChangeThemeColorScheme(Application.Current, value.Name);
                    SharedService.UpdateAppSetting(new SettingVM 
                    { 
                        SettingId = (int)SettingEnum.Color, 
                        Key = SettingEnum.Color.GetMetadata().Name,
                        Value = value.Name 
                    });
                }
            }
        }

        private string defaultWorkspace;
        public string DefaultWorkspace 
        {
            get => defaultWorkspace;
            set => SetProperty(ref defaultWorkspace, value);
        }

        #endregion
    }
}
