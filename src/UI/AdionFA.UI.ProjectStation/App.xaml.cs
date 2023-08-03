using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts.Services;
using AdionFA.UI.Infrastructure.Services;
using AdionFA.UI.ProjectStation.Commands;
using ControlzEx.Theming;
using FluentValidation;
using Ninject;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using Serilog;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace AdionFA.UI.ProjectStation
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
            containerRegistry.RegisterSingleton<IAppProjectCommands, AppProjectCommandsProxy>();

            // Infrastructure Services

            containerRegistry.RegisterInstance<IProcessService>(Container.Resolve<ProcessService>());
            containerRegistry.RegisterInstance<IFlyoutService>(Container.Resolve<FlyoutService>());

            // FluentValidation

            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

            // Global Exception

            Current.DispatcherUnhandledException += (object sender, DispatcherUnhandledExceptionEventArgs e) =>
            {
                e.Handled = true;
                MessageBox.Show(e.Exception.Message);
                IoC.Kernel.Get<ILogger>().Error($"{e.Exception.Message} :: {e.Exception.StackTrace}.");
            };

            // Exit

            Current.Exit += (object sender, ExitEventArgs e) =>
            {
                ContainerLocator.Current.Resolve<IProcessService>().EndWeka();
            };

        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var args = e.Args[0].Split("_AdionFA.UI.ProjectStation_");

            ProcessArgs.ProjectId = args.Length > 0 ? int.Parse(args[0], CultureInfo.InvariantCulture) : 0;
            ProcessArgs.ProjectName = args[1] ?? string.Empty;

            if (ProcessArgs.ProjectId > 0)
            {
                base.OnStartup(e);

                IoC.Kernel.Get<ILogger>().Information("App.OnStartup() :: AdionFA.UI.ProjectStation.");

                var settingService = IoC.Kernel.Get<ISettingService>();
                ProjectDirectoryManager.DefaultWorkspace = settingService.GetSetting((int)SettingEnum.DefaultWorkspace).Value;

                // Set theme

                var themeSetting = settingService.GetSetting((int)SettingEnum.Theme);
                ThemeManager.Current.ChangeThemeBaseColor(Current, themeSetting?.Value ?? "Light");

                var colorSetting = settingService.GetSetting((int)SettingEnum.Color);
                ThemeManager.Current.ChangeThemeColorScheme(Current, colorSetting?.Value ?? "Orange");

                // Set culture info

                var cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures).ToList();

                var cultureSetting = settingService.GetSetting((int)SettingEnum.Culture);
                var culture = cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == cultureSetting?.Value)
                    ?? cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == "eng");

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                ContainerLocator.Current.Resolve<IProcessService>().StartWeka();
            }
            else
            {
                MessageBox.Show("Error loading project");
                Current.Shutdown();
            }
        }
    }

    public static class ProcessArgs
    {
        public static int SymbolId => IoC.Kernel.Get<IProjectService>().GetProject(ProjectId, true).HistoricalData.SymbolId;
        public static int TimeframeId => IoC.Kernel.Get<IProjectService>().GetProject(ProjectId, true).HistoricalData.TimeframeId;
        public static int HistoricalDataId => IoC.Kernel.Get<IProjectService>().GetProject(ProjectId, true).HistoricalDataId;
        public static int ProjectId { get; set; }
        public static string ProjectName { get; set; }
    }
}