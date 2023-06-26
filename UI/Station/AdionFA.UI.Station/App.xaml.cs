using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.Security.Helper;
using AdionFA.Infrastructure.Common.Security.Model;
using AdionFA.Infrastructure.Common.Validators.FluentValidator;
using AdionFA.Infrastructure.Core.IofCExt;
using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Contracts.Services;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Infrastructure.Services.AppServices;
using AdionFA.UI.Station.Module.Dashboard;
using AdionFA.UI.Station.Module.Dashboard.Services;
using AdionFA.UI.Station.Module.Shell;
using ControlzEx.Theming;
using FluentValidation;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace AdionFA.UI.Station
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            new IoC().Setup();

            // Identity

            AdionIdentity Identity = new(SecurityHelper.DefaultOwnerId, SecurityHelper.DefaultOwner);
            AdionPrincipal Principal = new()
            {
                Identity = Identity
            };

            AppDomain.CurrentDomain.SetThreadPrincipal(Principal);

            // Application Commands

            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommandsProxy>();

            // Infrastructure Services

            containerRegistry.RegisterSingleton<IMarketDataServiceAgent, MarketDataServiceAgent>();
            containerRegistry.RegisterSingleton<IProjectServiceAgent, ProjectServiceAgent>();
            containerRegistry.RegisterSingleton<ISharedServiceAgent, SharedServiceAgent>();
            containerRegistry.RegisterSingleton<IMetaTraderService, MetaTraderService>();

            containerRegistry.RegisterInstance<IFlyoutService>(Container.Resolve<FlyoutService>());
            containerRegistry.RegisterInstance<IProcessService>(Container.Resolve<ProcessService>());

            // Fluent Validation

            ValidatorOptions.Global.LanguageManager = new FluentValidatorLanguageManager();
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

            // Global Exception

            Current.DispatcherUnhandledException += (object sender, DispatcherUnhandledExceptionEventArgs e) =>
            {
                e.Handled = true;
                MessageBox.Show(e.Exception.Message);
            };

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((object sender, UnhandledExceptionEventArgs e) =>
            {
                MessageBox.Show(e.ExceptionObject.ToString());
            });
        }

        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            var shellModuleType = typeof(ShellModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = shellModuleType.Name,
                ModuleType = shellModuleType.AssemblyQualifiedName,
            });

            var settingModuleType = typeof(DashboardModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = settingModuleType.Name,
                ModuleType = settingModuleType.AssemblyQualifiedName,
            });
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ConfigurationModuleCatalog catalog = new();
            return catalog;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ContainerLocator.Current.Resolve<IApplicationCommands>().EndAllProcessProjectCommand.Execute(false);

            ContainerLocator.Current.Resolve<IProcessService>().StartProcessWekaJava();
        }

        protected override void OnInitialized()
        {
            var settingService = ContainerLocator.Current.Resolve<ISharedServiceAgent>();
            var directoryService = IoC.Get<IProjectDirectoryService>();

            // Workspace

            var workspace = settingService.GetSetting((int)SettingEnum.DefaultWorkspace);
            if (Directory.Exists(workspace.Value))
            {
                ProjectDirectoryManager.DefaultWorkspace = workspace.Value;
            }

            directoryService.CreateDefaultWorkspace();

            // Theme

            var themeSetting = settingService.GetSetting((int)SettingEnum.Theme);
            ThemeManager.Current.ChangeThemeBaseColor(Application.Current, themeSetting?.Value ?? "Light");

            var colorSetting = settingService.GetSetting((int)SettingEnum.Color);
            ThemeManager.Current.ChangeThemeColorScheme(Application.Current, colorSetting?.Value ?? "Orange");

            // Culture

            var culturesAvailables = AppSettingsManager.Instance.Get<AppSettings>().Cultures?.Split(",");

            IList<CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures)
                .Where(c => culturesAvailables.Any(ca => ca == c.ThreeLetterISOLanguageName)).ToList();

            var cultureSetting = settingService.GetSetting((int)SettingEnum.Culture);

            var Culture = cultures.FirstOrDefault(
                c => c.ThreeLetterISOLanguageName == cultureSetting?.Value) ?? cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == "eng");

            Thread.CurrentThread.CurrentCulture = Culture;
            Thread.CurrentThread.CurrentUICulture = Culture;

            base.OnInitialized();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Container.Resolve<IMetaTraderService>().Disconnect();

            base.OnExit(e);
        }
    }
}
