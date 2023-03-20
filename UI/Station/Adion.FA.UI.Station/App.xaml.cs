using Adion.FA.Infrastructure.Common.Directories.Contracts;
using Adion.FA.Infrastructure.Common.Directories.Services;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Common.Managements;
using Adion.FA.Infrastructure.Common.Security.Helper;
using Adion.FA.Infrastructure.Common.Security.Model;
using Adion.FA.Infrastructure.Common.Validators.FluentValidator;
using Adion.FA.Infrastructure.Core.IofCExt;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using Adion.FA.UI.Station.Infrastructure.Contracts.Services;
using Adion.FA.UI.Station.Infrastructure.Model.Common;
using Adion.FA.UI.Station.Infrastructure.Services;
using Adion.FA.UI.Station.Infrastructure.Services.AppServices;
using Adion.FA.UI.Station.Module.Dashboard;
using Adion.FA.UI.Station.Module.Shell;
using ControlzEx.Theming;
using FluentValidation;
using Grpc.Core;
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

namespace Adion.FA.UI.Station
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

            #endregion

            #region Infrastructure Services

            containerRegistry.RegisterSingleton<IMarketDataServiceAgent, MarketDataServiceAgent>();
            containerRegistry.RegisterSingleton<IProjectServiceAgent, ProjectServiceAgent>();
            containerRegistry.RegisterSingleton<ISecurityServiceAgent, SecurityServiceAgent>();
            containerRegistry.RegisterSingleton<ISharedServiceAgent, SharedServiceAgent>();

            containerRegistry.RegisterInstance<IFlyoutService>(Container.Resolve<FlyoutService>());
            containerRegistry.RegisterInstance<IProcessService>(Container.Resolve<ProcessService>());
            

            #endregion

            #region FluentValidation

            ValidatorOptions.Global.LanguageManager = new FluentValidatorLanguageManager();
            //ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

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

            
            // Infrastructure Services
            //containerRegistry.Register<ISchedulerProvider>(() => new SchedulerProvider(Dispatcher));
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            
            Type shellModuleType = typeof(ShellModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = shellModuleType.Name,
                ModuleType = shellModuleType.AssemblyQualifiedName,
            });

            Type settingModuleType = typeof(SettingModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = settingModuleType.Name,
                ModuleType = settingModuleType.AssemblyQualifiedName,
            });
            /*
            Type tradeModuleType = typeof(TraderModule);
            moduleCatalog.AddModule(new ModuleInfo() 
            {
                ModuleName = tradeModuleType.Name,
                ModuleType = tradeModuleType.AssemblyQualifiedName,
            });*/

            /*Type toolsModuleType = typeof(ToolsModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = toolsModuleType.Name,
                ModuleType = toolsModuleType.AssemblyQualifiedName,
            });*/
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ConfigurationModuleCatalog catalog = new ConfigurationModuleCatalog();
            return catalog;
        }

        private Server server { get; set; }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ContainerLocator.Current.Resolve<IApplicationCommands>().EndAllProcessProjectCommand.Execute(false);

            ContainerLocator.Current.Resolve<IProcessService>().StartProcessWekaJava();
            
            /*server = new Server
            {
                Services = { ProjectRPCServicePartial.BindService(new ProjectRPCServerService()) },
                Ports = { new ServerPort("localhost", (int)PortEnum.ServerRPCPort, ServerCredentials.Insecure) }
            };
            server.Start();*/
        }

        protected override void OnInitialized()
        {
            ISharedServiceAgent settingService = ContainerLocator.Current.Resolve<ISharedServiceAgent>();
            IProjectDirectoryService directoryService = IoC.Get<IProjectDirectoryService>();

            #region Workspace

            var workspace = settingService.GetSetting((int)SettingEnum.DefaultWorkspace);
            if (Directory.Exists(workspace.Value))
            { 
                ProjectDirectoryManager.defaultWorkspace = workspace.Value;
            }

            directoryService.CreateDefaultWorkspace();

            #endregion

            #region Theme

            SettingVM themeSetting = settingService.GetSetting((int)SettingEnum.Theme);
            ThemeManager.Current.ChangeThemeBaseColor(Application.Current, themeSetting?.Value ?? "Light");

            SettingVM colorSetting = settingService.GetSetting((int)SettingEnum.Color);
            ThemeManager.Current.ChangeThemeColorScheme(Application.Current, colorSetting?.Value ?? "Orange");

            #endregion

            #region Culture

            var culturesAvailables = AppSettingsManager.Instance.Get<AppSettings>().Cultures?.Split(",");

            IList <CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures)
                .Where(c => culturesAvailables.Any(ca => ca == c.ThreeLetterISOLanguageName)).ToList();

            SettingVM cultureSetting = settingService.GetSetting((int)SettingEnum.Culture);

            CultureInfo Culture = cultures.FirstOrDefault(
                c => c.ThreeLetterISOLanguageName == cultureSetting?.Value) ?? cultures.FirstOrDefault(c => c.ThreeLetterISOLanguageName == "eng");

            Thread.CurrentThread.CurrentCulture = Culture;
            Thread.CurrentThread.CurrentUICulture = Culture;

            #endregion

            base.OnInitialized();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ContainerLocator.Current.Resolve<IApplicationCommands>().EndAllProcessProjectCommand.Execute(true);
            //server.ShutdownAsync().Wait();
            base.OnExit(e);
        }
    }
}
