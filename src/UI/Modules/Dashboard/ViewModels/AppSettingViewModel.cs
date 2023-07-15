using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Contracts.Services;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Module.Dashboard.Model;
using ControlzEx.Theming;
using Ninject;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace AdionFA.UI.Station.Module.Dashboard.ViewModels
{
    public class AppSettingViewModel : ViewModelBase
    {
        private readonly IProjectDirectoryService _directoryService;

        private readonly ISharedServiceAgent _sharedService;
        private readonly IProjectServiceAgent _projectService;
        private readonly IProcessService _processService;

        public AppSettingViewModel(
            IApplicationCommands applicationCommands,
            ISharedServiceAgent sharedService,
            IProjectServiceAgent projectService,
            IProcessService processService)
        {
            // Infrastructure Common

            _directoryService = IoC.Kernel.Get<IProjectDirectoryService>();

            // Infrastructure UI
            _sharedService = sharedService;
            _projectService = projectService;
            _processService = processService;

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        private ICommand FlyoutCommand { get; set; }

        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutAppSetting))
            {
                PopulateViewModel();
            }
        }

        public DelegateCommand UploadDefaultWorkspaceBtnCommand => new(async () =>
        {
            try
            {
                if (!_processService.AnyProcessProject())
                {
                    var result = await _sharedService.UpdateAppSetting(new SettingVM
                    {
                        SettingId = (int)SettingEnum.DefaultWorkspace,
                        Name = SettingEnum.DefaultWorkspace.GetMetadata().Name,
                        Value = DefaultWorkspace
                    });

                    if (result.IsSuccess)
                    {
                        if (_directoryService.CreateDefaultWorkspace())
                        {
                            var projects = await _projectService.GetAllProjectAsync();

                            foreach (var p in projects)
                            {
                                _directoryService.CreateDefaultProjectWorkspace(p.ProjectName);
                            }

                            MessageHelper.ShowMessage(this,
                                nameof(EntityTypeEnum.Setting),
                                "Default workspace was updated.");
                        }
                    }
                }
                else
                {
                    MessageHelper.ShowMessage(this,
                        EntityTypeEnum.Setting.GetMetadata().Description,
                        "Close all running projects to run the operation.");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        });

        private async void PopulateViewModel()
        {
            Themes = ThemeManager.Current.BaseColors.Select(a => new ApplicationTheme
            {
                Name = a,
            }).ToList();

            Colors = ThemeManager.Current.ColorSchemes.Select(a => new AccentColor
            {
                Name = a
            }).ToList();

            var culturesAvailables = AppSettingsManager.Instance.Get<AppSettings>().Cultures?.Split(",");
            Cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures)
                .Where(c => culturesAvailables.Any(ca => ca == c.ThreeLetterISOLanguageName)).ToList();

            var themeSetting = await _sharedService.GetSettingAsync((int)SettingEnum.Theme);
            SelectedTheme = themeSetting != null
                ? Themes.FirstOrDefault(th => th.Name == themeSetting.Value)
                : Themes.FirstOrDefault();

            var colorSetting = await _sharedService.GetSettingAsync((int)SettingEnum.Color);
            SelectedColor = colorSetting != null
                ? Colors.FirstOrDefault(ac => ac.Name == colorSetting.Value)
                : Colors.FirstOrDefault();

            var cultureSetting = await _sharedService.GetSettingAsync((int)SettingEnum.Culture);
            SelectedCulture = cultureSetting != null
                ? Cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == cultureSetting.Value)
                : Cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == "eng");

            var defaultWorkspace = await _sharedService.GetSettingAsync((int)SettingEnum.DefaultWorkspace);
            DefaultWorkspace = defaultWorkspace?.Value;

            var host = await _sharedService.GetSettingAsync((int)SettingEnum.Host);
            Host = host?.Value;

            var port = await _sharedService.GetSettingAsync((int)SettingEnum.Port);
            Port = port?.Value;
        }

        // View Binding

        private IList<CultureInfo> _cultures;
        public IList<CultureInfo> Cultures
        {
            get => _cultures;
            set => SetProperty(ref _cultures, value);
        }

        private IList<ApplicationTheme> _themes;
        public IList<ApplicationTheme> Themes
        {
            get => _themes;
            set => SetProperty(ref _themes, value);
        }

        private IList<AccentColor> _colors;
        public IList<AccentColor> Colors
        {
            get => _colors;
            set => SetProperty(ref _colors, value);
        }

        private CultureInfo _selectedCulture;
        public CultureInfo SelectedCulture
        {
            get => _selectedCulture;
            set
            {
                if (SetProperty(ref _selectedCulture, value) && value != null)
                {
                    Thread.CurrentThread.CurrentCulture = SelectedCulture;
                    Thread.CurrentThread.CurrentUICulture = SelectedCulture;

                    _sharedService.UpdateAppSetting(new SettingVM
                    {
                        SettingId = (int)SettingEnum.Culture,
                        Name = SettingEnum.Culture.GetMetadata().Name,
                        Value = SelectedCulture.ThreeLetterISOLanguageName
                    });
                }
            }
        }

        private ApplicationTheme _selectedTheme;
        public ApplicationTheme SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (SetProperty(ref _selectedTheme, value) && value != null)
                {
                    ThemeManager.Current.ChangeThemeBaseColor(Application.Current, value.Name);
                    _sharedService.UpdateAppSetting(new SettingVM
                    {
                        SettingId = (int)SettingEnum.Theme,
                        Name = SettingEnum.Theme.GetMetadata().Name,
                        Value = value.Name
                    });
                }
            }
        }

        private AccentColor _selectedColor;
        public AccentColor SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (SetProperty(ref _selectedColor, value) && value != null)
                {
                    ThemeManager.Current.ChangeThemeColorScheme(Application.Current, value.Name);
                    _sharedService.UpdateAppSetting(new SettingVM
                    {
                        SettingId = (int)SettingEnum.Color,
                        Name = SettingEnum.Color.GetMetadata().Name,
                        Value = value.Name
                    });
                }
            }
        }

        private string _defaultWorkspace;
        public string DefaultWorkspace
        {
            get => _defaultWorkspace;
            set => SetProperty(ref _defaultWorkspace, value);
        }

        private string _host;
        public string Host
        {
            get => _host;
            set
            {
                if (SetProperty(ref _host, value))
                {
                    _sharedService.UpdateAppSetting(new SettingVM
                    {
                        SettingId = (int)SettingEnum.Host,
                        Name = SettingEnum.Host.GetMetadata().Name,
                        Value = Host
                    });
                }
            }
        }

        private string _port;
        public string Port
        {
            get => _port;
            set
            {
                if (SetProperty(ref _port, value))
                {
                    _sharedService.UpdateAppSetting(new SettingVM
                    {
                        SettingId = (int)SettingEnum.Port,
                        Name = SettingEnum.Port.GetMetadata().Name,
                        Value = Port
                    });
                }
            }
        }
    }
}
