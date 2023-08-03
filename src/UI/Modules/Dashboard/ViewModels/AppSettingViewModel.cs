using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.TransferObject.Common;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Contracts.Services;
using AdionFA.UI.Infrastructure.Helpers;
using AdionFA.UI.Infrastructure.Services;
using AdionFA.UI.Module.Dashboard.Model;
using ControlzEx.Theming;
using Ninject;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace AdionFA.UI.Module.Dashboard.ViewModels
{
    public class AppSettingViewModel : ViewModelBase
    {
        private readonly IProjectDirectoryService _projectDirectoryService;
        private readonly IProjectService _projectService;
        private readonly ISettingService _appSettingService;

        private readonly IProcessService _processService;

        public AppSettingViewModel(
            IApplicationCommands applicationCommands,
            IProcessService processService)
        {
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
            _appSettingService = IoC.Kernel.Get<ISettingService>();
            _projectService = IoC.Kernel.Get<IProjectService>();

            _processService = processService;

            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.Name ?? string.Empty).Equals(FlyoutRegions.FlyoutAppSetting))
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

                var themeSetting = _appSettingService.GetSetting((int)SettingEnum.Theme);
                SelectedTheme = themeSetting != null
                    ? Themes.FirstOrDefault(th => th.Name == themeSetting.Value)
                    : Themes.FirstOrDefault();

                var colorSetting = _appSettingService.GetSetting((int)SettingEnum.Color);
                SelectedColor = colorSetting != null
                    ? Colors.FirstOrDefault(ac => ac.Name == colorSetting.Value)
                    : Colors.FirstOrDefault();

                var cultureSetting = _appSettingService.GetSetting((int)SettingEnum.Culture);
                SelectedCulture = cultureSetting != null
                    ? Cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == cultureSetting.Value)
                    : Cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == "eng");

                DefaultWorkspace = _appSettingService.GetSetting((int)SettingEnum.DefaultWorkspace).Value;

                Host = _appSettingService.GetSetting((int)SettingEnum.Host).Value;
                Port = _appSettingService.GetSetting((int)SettingEnum.Port).Value;

            }
        });

        public ICommand UploadDefaultWorkspaceBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                if (!_processService.AnyProcessProject())
                {
                    var result = await _appSettingService.UpdateSettingAsync(new SettingDTO
                    {
                        SettingId = (int)SettingEnum.DefaultWorkspace,
                        Name = SettingEnum.DefaultWorkspace.GetMetadata().Name,
                        Value = DefaultWorkspace
                    }).ConfigureAwait(true);

                    if (result.IsSuccess)
                    {
                        if (_projectDirectoryService.CreateDefaultWorkspace())
                        {
                            var projects = _projectService.GetAllProject(false);

                            foreach (var project in projects)
                            {
                                _projectDirectoryService.CreateDefaultProjectWorkspace(project.ProjectName);
                            }

                            MessageHelper.ShowMessage(this,
                                EntityTypeEnum.Setting.GetMetadata().Name,
                                "Default workspace was updated");
                        }
                    }
                }
                else
                {
                    MessageHelper.ShowMessage(this,
                        EntityTypeEnum.Setting.GetMetadata().Name,
                        "Close all running projects to run the operation");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        });

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

                    _appSettingService.UpdateSettingAsync(new SettingDTO
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
                    ThemeManager.Current.ChangeThemeBaseColor(System.Windows.Application.Current, value.Name);
                    _appSettingService.UpdateSettingAsync(new SettingDTO
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
                    ThemeManager.Current.ChangeThemeColorScheme(System.Windows.Application.Current, value.Name);
                    _appSettingService.UpdateSettingAsync(new SettingDTO
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
                    _appSettingService.UpdateSettingAsync(new SettingDTO
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
                    _appSettingService.UpdateSettingAsync(new SettingDTO
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
