using AdionFA.Application.Contracts;
using AdionFA.Application.Services.Projects;
using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.Validators.FluentValidator;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.Contracts.Services;
using AdionFA.UI.Infrastructure.Services;
using AdionFA.UI.ProjectStation.Commands;
using AdionFA.UI.ProjectStation.Services;
using ControlzEx.Theming;
using FluentValidation;
using Ninject;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Diagnostics;
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

            // Application Services

            containerRegistry.RegisterSingleton<IAppProjectService, AppProjectService>();

            // FluentValidation

            ValidatorOptions.Global.LanguageManager = new FluentValidatorLanguageManager();

            // Global Exception

            Current.DispatcherUnhandledException += (object sender, DispatcherUnhandledExceptionEventArgs e) =>
            {
                e.Handled = true;
                MessageBox.Show(e.Exception.Message);
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
            //var arg = e.Args[0];
            var arg = $"1_AdionFA.UI.ProjectStation_test1";

            ProcessArgs.Args = arg;
            if (ProcessArgs.ProjectId > 0)
            {
                base.OnStartup(e);

                var settingService = IoC.Kernel.Get<IAppSettingAppService>();
                ProjectDirectoryManager.DefaultWorkspace = settingService.GetSetting((int)SettingEnum.DefaultWorkspace)?.Value;

                try
                {
                    // Theme

                    var themeSetting = settingService.GetSetting((int)SettingEnum.Theme);
                    ThemeManager.Current.ChangeThemeBaseColor(Current, themeSetting?.Value ?? "Light");

                    var colorSetting = settingService.GetSetting((int)SettingEnum.Color);
                    ThemeManager.Current.ChangeThemeColorScheme(Current, colorSetting?.Value ?? "Orange");

                    // Culture

                    var cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures).ToList();

                    var cultureSetting = settingService.GetSetting((int)SettingEnum.Culture);
                    var culture = cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == cultureSetting?.Value)
                        ?? cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == "eng");

                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;

                    ContainerLocator.Current.Resolve<IProcessService>().StartProcessWekaJava();
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    throw;
                }
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
        private static string[] args;

        public static string Args
        {
            set => args = value.Split("_AdionFA.UI.ProjectStation_");
        }

        public static int ProjectId => args.Length > 0 ? int.Parse(args[0], CultureInfo.InvariantCulture) : 0;
        public static string ProjectName => args[1] ?? string.Empty;
        public static ProjectDTO Project => IoC.Kernel.Get<ProjectAppService>().GetProject(ProjectId, true);

        public static string DefaultWorkspace => Project?
            .ProjectConfigurations?
            .FirstOrDefault(x => x.EndDate == null)?
            .WorkspacePath ?? string.Empty;
    }
}