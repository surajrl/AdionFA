using AdionFA.Application.Contracts;
using AdionFA.Application.Services.Commons;
using AdionFA.Application.Services.MarketData;
using AdionFA.Application.Services.Projects;
using AdionFA.Infrastructure.AutoMappers;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.Directories.Services;
using AdionFA.Infrastructure.Extractor.Contracts;
using AdionFA.Infrastructure.Extractor.Services;
using AdionFA.Infrastructure.MetaTrader.Contracts;
using AdionFA.Infrastructure.MetaTrader.Services;
using AdionFA.Infrastructure.Modules.Builder;
using AdionFA.Infrastructure.Weka.Contracts;
using AdionFA.Infrastructure.Weka.Services;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Ninject.Modules;
using Serilog;
using System.IO;

namespace AdionFA.Infrastructure.IofC
{
    public class ModuleBase : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IMapper>().ToMethod(automapper => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingDomainProfile());
            }).CreateMapper()).InSingletonScope();

            // Serilog

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Kernel.Bind<ILogger>().ToMethod(x => new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger())
                .InSingletonScope();

            // Weka

            Kernel.Bind(typeof(IWekaApiClient)).To(typeof(WekaApiClient)).InSingletonScope();

            // Services

            Kernel.Bind<IProjectDirectoryService>().To<ProjectDirectoryService>();
            Kernel.Bind<IExtractorService>().To<ExtractorService>();
            Kernel.Bind<IBuilderService>().To<BuilderService>();
            Kernel.Bind<ITradeService>().To<TradeService>();

            // Application

            Kernel.Bind(typeof(IProjectService)).To(typeof(ProjectService));
            Kernel.Bind(typeof(IMarketDataService)).To(typeof(MarketDataService));
            Kernel.Bind(typeof(IGlobalConfigurationService)).To(typeof(GlobalConfigurationService));
            Kernel.Bind(typeof(ISettingService)).To(typeof(SettingService));
        }
    }
}
