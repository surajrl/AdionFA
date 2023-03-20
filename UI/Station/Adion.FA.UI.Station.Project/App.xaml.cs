using Adion.FA.Infrastructure.Common.Directories.Contracts;
using Adion.FA.Infrastructure.Common.Directories.Services;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Common.Security.Helper;
using Adion.FA.Infrastructure.Common.Security.Model;
using Adion.FA.Infrastructure.Core.IofCExt;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.UI.Station.Project.Commands;
using Adion.FA.UI.Station.Project.Services;
using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using Adion.FA.UI.Station.Infrastructure.Contracts.Services;
using Adion.FA.UI.Station.Infrastructure.Model.Common;
using Adion.FA.UI.Station.Infrastructure.Services;
using Adion.FA.UI.Station.Infrastructure.Services.AppServices;
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
using Adion.FA.Infrastructure.Common.Validators.FluentValidator;
using System.Windows.Threading;
using Adion.FA.TransferObject.Project;

namespace Adion.FA.UI.Station.Project
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

            #region Identity

            AdionIdentity Identity = new AdionIdentity(SecurityHelper.DefaultTenantId, SecurityHelper.DefaultOwnerId, SecurityHelper.DefaultOwner);
            AdionPrincipal Principal = new AdionPrincipal();
            Principal.Identity = Identity;
            AppDomain.CurrentDomain.SetThreadPrincipal(Principal);

            #endregion

            #region Application commands

            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommandsProxy>();
            containerRegistry.RegisterSingleton<IAppProjectCommands, AppProjectCommandsProxy>();

            #endregion

            #region Infrastructure Services

            containerRegistry.RegisterSingleton<ISharedServiceAgent, SharedServiceAgent>();
            containerRegistry.RegisterSingleton<IProjectServiceAgent, ProjectServiceAgent>();
            containerRegistry.RegisterSingleton<IMarketDataServiceAgent, MarketDataServiceAgent>();
            containerRegistry.RegisterSingleton<IAppProjectService, AppProjectService>();

            containerRegistry.RegisterInstance<IProcessService>(Container.Resolve<ProcessService>());
            containerRegistry.RegisterInstance<IFlyoutService>(Container.Resolve<FlyoutService>());

            #endregion

            #region FluentValidation
            ValidatorOptions.Global.LanguageManager = new FluentValidatorLanguageManager();
            #endregion

            #region Global Exception
            Application.Current.DispatcherUnhandledException += (object sender, DispatcherUnhandledExceptionEventArgs e) =>
            {
                e.Handled = true;
                MessageBox.Show(e.Exception.Message);
            };
            
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((object sender, UnhandledExceptionEventArgs e) =>
            {
                MessageBox.Show(e.ExceptionObject.ToString());
            });
            #endregion
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ConfigurationModuleCatalog catalog = new ConfigurationModuleCatalog();
            return catalog;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            string arg = e.Args[0];
            //string arg = "6_Adion.FA.UI.Station.Project_test5";
            ProcessArgs.Args = arg;
            if (ProcessArgs.ProjectId > 0)
            {
                base.OnStartup(e);

                var settingService = FacadeService.SharedServiceAgent;
                ProjectDirectoryManager.defaultWorkspace = settingService.GetSetting((int)SettingEnum.DefaultWorkspace)?.Value;

                var directoryService = FacadeService.DirectoryService;
                if (!directoryService.IsValidProjectDiractory(ProcessArgs.ProjectName))
                {
                    if (!directoryService.IsValidProjectDiractory(ProcessArgs.ProjectName))
                        MessageBox.Show("Loaded Project Error (Modified Directory)");
                    Current.Shutdown();
                }
                else
                {
                    try
                    {
                        #region Theme

                        SettingVM themeSetting = settingService.GetSetting((int)SettingEnum.Theme);
                        ThemeManager.Current.ChangeThemeBaseColor(Application.Current, themeSetting?.Value ?? "Light");

                        SettingVM colorSetting = settingService.GetSetting((int)SettingEnum.Color);
                        ThemeManager.Current.ChangeThemeColorScheme(Application.Current, colorSetting?.Value ?? "Orange");

                        #endregion

                        #region Culture

                        IList<CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures).ToList();

                        SettingVM cultureSetting = settingService.GetSetting((int)SettingEnum.Culture);

                        CultureInfo Culture = cultures.FirstOrDefault(
                            c => c.ThreeLetterISOLanguageName == cultureSetting?.Value) ?? cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == "eng");

                        Thread.CurrentThread.CurrentCulture = Culture;
                        Thread.CurrentThread.CurrentUICulture = Culture;

                        #endregion

                        //esto va en projecto station
                        ContainerLocator.Current.Resolve<IProcessService>().StartProcessWekaJava();
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(ex.Message);
                        throw;
                    }

                    #region RPC

                    //var response = ProjectRPCClientService.LoadProjectRequest(projectId, true);
                    //IProjectService service = ServiceLocator.Current.GetInstance<IKernel>().Get<IProjectService>();
                    //ProjectVM project = await service.GetProject(projectId, true);
                    //ProcessArgs.project = project;

                    #endregion
                }
            }
            else
            {
                MessageBox.Show("Loaded Project Error");
                Current.Shutdown();
            }
        }
    }

    public static class ProcessArgs
    {
        private static string[] args;
        public static string Args 
        {
            set => args = value.Split("_Adion.FA.UI.Station.Project_");
        }
        public static int ProjectId => args.Length > 0 ? int.Parse(args[0]) : 0;
        public static string ProjectName => args[1] ?? string.Empty;
        public static ProjectDTO Project => FacadeService.ProjectAPI.GetProject(ProjectId, true);
        public static String DefaultWorkspace => Project?.ProjectConfigurations?.FirstOrDefault(x => x.EndDate == null)?.WorkspacePath ?? string.Empty;
    }
}
