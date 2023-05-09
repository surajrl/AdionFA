using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Security.Helper;
using AdionFA.Infrastructure.Common.Security.Model;
using AdionFA.Infrastructure.Core.IofCExt;
using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.Services;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Contracts.Services;
using AdionFA.UI.Station.Infrastructure.Model.Common;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Infrastructure.Services.AppServices;
using ControlzEx.Theming;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using FluentValidation;
using AdionFA.Infrastructure.Common.Validators.FluentValidator;
using System.Windows.Threading;
using AdionFA.TransferObject.Project;
using AdionFA.Core.Domain.Aggregates.Project;
using AdionFA.Infrastructure.Common.Managements;

namespace AdionFA.UI.Station.Project
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

            AdionIdentity Identity = new(SecurityHelper.DefaultTenantId, SecurityHelper.DefaultOwnerId, SecurityHelper.DefaultOwner);
            AdionPrincipal Principal = new()
            {
                Identity = Identity
            };
            AppDomain.CurrentDomain.SetThreadPrincipal(Principal);

            // Application commands

            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommandsProxy>();
            containerRegistry.RegisterSingleton<IAppProjectCommands, AppProjectCommandsProxy>();

            // Infrastructure Services

            containerRegistry.RegisterSingleton<ISharedServiceAgent, SharedServiceAgent>();
            containerRegistry.RegisterSingleton<IServiceAgent, ServiceAgent>();
            containerRegistry.RegisterSingleton<IProjectServiceAgent, ProjectServiceAgent>();
            containerRegistry.RegisterSingleton<IMarketDataServiceAgent, MarketDataServiceAgent>();

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

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((object sender, UnhandledExceptionEventArgs e) =>
            {
                MessageBox.Show(e.ExceptionObject.ToString());
            });
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ConfigurationModuleCatalog catalog = new();
            return catalog;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //var arg = e.Args[0];
            var arg = $"1_AdionFA.UI.Station.Project_test";

            ProcessArgs.Args = arg;
            if (ProcessArgs.ProjectId > 0)
            {
                base.OnStartup(e);

                var settingService = FacadeService.SharedServiceAgent;
                ProjectDirectoryManager.DefaultWorkspace = settingService.GetSetting((int)SettingEnum.DefaultWorkspace)?.Value;

                var directoryService = FacadeService.DirectoryService;
                if (!directoryService.IsValidProjectDirectory(ProcessArgs.ProjectName))
                {
                    MessageBox.Show("Error Loading Project");
                    Current.Shutdown();
                }
                else
                {
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
            set => args = value.Split("_AdionFA.UI.Station.Project_");
        }

        public static int ProjectId => args.Length > 0 ? int.Parse(args[0]) : 0;
        public static string ProjectName => args[1] ?? string.Empty;
        public static ProjectDTO Project => FacadeService.ProjectAPI.GetProject(ProjectId, true);

        public static string DefaultWorkspace => Project?
            .ProjectConfigurations?
            .FirstOrDefault(x => x.EndDate == null)?
            .WorkspacePath ?? string.Empty;
    }
}
