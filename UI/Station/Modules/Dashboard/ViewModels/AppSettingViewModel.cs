using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Directories.Services;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Contracts.Services;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Module.Dashboard.Model;
using AdionFA.UI.Station.Module.Dashboard.Services;
using ControlzEx.Theming;
using Prism.Ioc;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using AdionFA.UI.Station.Infrastructure.EventAggregator;

namespace AdionFA.UI.Station.Module.Dashboard.ViewModels
{
    public class AppSettingViewModel : ViewModelBase
    {
        private readonly IProjectDirectoryService _directoryService;

        private readonly ISharedServiceAgent _sharedService;
        private readonly IProjectServiceAgent _projectService;
        private readonly IProcessService _processService;
        private readonly IMetaTraderService _metaTraderService;

        private readonly IEventAggregator _eventAggregator;

        public AppSettingViewModel(
            IApplicationCommands applicationCommands,
            ISharedServiceAgent sharedService,
            IProjectServiceAgent projectService,
            IProcessService processService,
            IMetaTraderService metaTraderService)
        {
            // Infrastructure Common
            _directoryService = IoC.Get<IProjectDirectoryService>();

            // Infrastructure UI
            _sharedService = sharedService;
            _projectService = projectService;
            _processService = processService;
            _metaTraderService = metaTraderService;

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<MetaTraderConnectedEvent>().Subscribe(p => IsMetaTraderConnected = p);

            FlyoutCommand = new DelegateCommand<FlyoutModel>(ShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
        }

        private ICommand FlyoutCommand { get; set; }

        public void ShowFlyout(FlyoutModel flyoutModel)
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutAppSetting))
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
                        Key = SettingEnum.DefaultWorkspace.GetMetadata().Name,
                        Value = DefaultWorkspace
                    });

                    if (result.IsSuccess)
                    {
                        if (_directoryService.CreateDefaultWorkspace())
                        {
                            IList<ProjectVM> projects = await _projectService.GetAllProjects();
                            foreach (var p in projects)
                            {
                                _directoryService.CreateDefaultProjectWorkspace(p.ProjectName);
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
        });

        public DelegateCommand ConnectCommand => new DelegateCommand(async () =>
        {
            try
            {
                await _metaTraderService.ConnectAsync(IPAddress, Port);
                IsMetaTraderConnected = true;
                MessageHelper.ShowMessage(
                    this,
                    "MetaTrader 5 Server",
                    $"Connected to: {_metaTraderService.RemoteEndPoint}"
                    );
            }
            catch (SocketException ex)
            {
                MessageHelper.ShowMessage(
                    this,
                    "MetaTrader 5 Server",
                    $"{ex.Message}"
                    );
            }
        });

        public DelegateCommand DisconnectCommand => new(() =>
        {
            try
            {
                _metaTraderService.Disconnect();
                IsMetaTraderConnected = false;
                MessageHelper.ShowMessage(
                    this,
                    "MetaTrader 5 Server",
                    $"Disconnected"
                    );
            }
            catch (Exception ex)
            {
                MessageHelper.ShowMessage(
                    this,
                    "MetaTrader 5 Server",
                    $"{ex.Message}"
                    );
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
                ? Themes.FirstOrDefault(th => th.Name == themeSetting.Value) : Themes.FirstOrDefault();

            var colorSetting = await _sharedService.GetSettingAsync((int)SettingEnum.Color);
            SelectedColor = colorSetting != null
                ? Colors.FirstOrDefault(ac => ac.Name == colorSetting.Value) : Colors.FirstOrDefault();

            var cultureSetting = await _sharedService.GetSettingAsync((int)SettingEnum.Culture);
            SelectedCulture = cultureSetting != null
                ? Cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == cultureSetting.Value)
                    : Cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == "eng");

            var df = await _sharedService.GetSettingAsync((int)SettingEnum.DefaultWorkspace);
            DefaultWorkspace = (df?.Value ?? string.Empty).Length > 0 ? df.Value : ProjectDirectoryManager.DefaultDirectory();

            var ipaddress = await _sharedService.GetSettingAsync((int)SettingEnum.IPAddress);
            IPAddress = (ipaddress?.Value ?? string.Empty).Length > 0 ? ipaddress.Value : string.Empty;

            var port = await _sharedService.GetSettingAsync((int)SettingEnum.Port);
            Port = int.TryParse(port?.Value, out var portInt) ? portInt : 5555;
        }

        #region Properties

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
                        Key = SettingEnum.Culture.GetMetadata().Name,
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
                        Key = SettingEnum.Theme.GetMetadata().Name,
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
                        Key = SettingEnum.Color.GetMetadata().Name,
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

        private string _ipAddress;

        public string IPAddress
        {
            get => _ipAddress;
            set
            {
                if (SetProperty(ref _ipAddress, value))
                {
                    _sharedService.UpdateAppSetting(new SettingVM
                    {
                        SettingId = (int)SettingEnum.IPAddress,
                        Key = SettingEnum.IPAddress.GetMetadata().Name,
                        Value = IPAddress
                    });
                }
            }
        }

        private int _port;

        public int Port
        {
            get => _port;
            set
            {
                if (SetProperty(ref _port, value))
                {
                    _sharedService.UpdateAppSetting(new SettingVM
                    {
                        SettingId = (int)SettingEnum.Port,
                        Key = SettingEnum.Port.GetMetadata().Name,
                        Value = Port.ToString()
                    });
                }
            }
        }

        private bool _isMetaTraderConnected;

        public bool IsMetaTraderConnected
        {
            get => _isMetaTraderConnected;
            set => SetProperty(ref _isMetaTraderConnected, value);
        }

        #endregion Properties
    }
}