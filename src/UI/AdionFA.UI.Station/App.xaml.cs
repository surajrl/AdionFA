using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts.Services;
using AdionFA.UI.Infrastructure.Services;
using AdionFA.UI.Module;
using AdionFA.UI.Module.Dashboard;
using ControlzEx.Theming;
using FluentValidation;
using Ninject;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using Serilog;
using System;
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

            // Application Commands

            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommandsProxy>();

            // Infrastructure Services

            containerRegistry.RegisterInstance<IFlyoutService>(Container.Resolve<FlyoutService>());
            containerRegistry.RegisterInstance<IProcessService>(Container.Resolve<ProcessService>());

            // Fluent Validation

            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

            // Global Exception

            Current.DispatcherUnhandledException += (object sender, DispatcherUnhandledExceptionEventArgs e) =>
            {
                e.Handled = true;
                MessageBox.Show(e.Exception.Message);
                IoC.Kernel.Get<ILogger>().Error($"{e.Exception.Message} :: {e.Exception.StackTrace}.");
            };

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((object sender, UnhandledExceptionEventArgs e) =>
            {
                MessageBox.Show(e.ExceptionObject.ToString());
            });
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
            return new ConfigurationModuleCatalog();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IoC.Kernel.Get<ILogger>().Information("App.OnStartup() :: AdionFA.UI.Station");

            ContainerLocator.Current.Resolve<IApplicationCommands>().EndAllProcessProjectCommand.Execute(false);
            ContainerLocator.Current.Resolve<IProcessService>().StartProcessWeka();
        }

        protected override void OnInitialized()
        {
            var settingService = IoC.Kernel.Get<ISettingService>();
            var directoryService = IoC.Kernel.Get<IProjectDirectoryService>();

            // Workspace

            var defaultWorkspace = settingService.GetSetting((int)SettingEnum.DefaultWorkspace);
            if (Directory.Exists(defaultWorkspace.Value))
            {
                ProjectDirectoryManager.DefaultWorkspace = defaultWorkspace.Value;
            }

            directoryService.CreateDefaultWorkspace();

            // Theme

            var themeSetting = settingService.GetSetting((int)SettingEnum.Theme);
            ThemeManager.Current.ChangeThemeBaseColor(Current, themeSetting?.Value ?? "Light");

            var colorSetting = settingService.GetSetting((int)SettingEnum.Color);
            ThemeManager.Current.ChangeThemeColorScheme(Current, colorSetting?.Value ?? "Orange");

            // Culture

            var culturesAvailables = AppSettingsManager.Instance.Get<AppSettings>().Cultures?.Split(",");

            var cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures)
                .Where(cultureInfo => culturesAvailables
                .Any(available => available == cultureInfo.ThreeLetterISOLanguageName)).ToList();

            var cultureSetting = settingService.GetSetting((int)SettingEnum.Culture);

            var culture = cultures.FirstOrDefault(
                cultureInfo => cultureInfo.ThreeLetterISOLanguageName == cultureSetting?.Value)
                ?? cultures.FirstOrDefault(cultureInfo => cultureInfo.ThreeLetterISOLanguageName == "eng");

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            base.OnInitialized();
        }
    }
}
