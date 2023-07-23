using AdionFA.Application.Contracts;
using AdionFA.Application.Services.Commons;
using AdionFA.Application.Services.MarketData;
using AdionFA.Application.Services.Projects;
using AdionFA.Domain.Contracts.Repositories;
using AdionFA.Infrastructure.AssemblyBuilder.Contracts;
using AdionFA.Infrastructure.AssemblyBuilder.Services;
using AdionFA.Infrastructure.AutoMappers;
using AdionFA.Infrastructure.CrossingBuilder.Contracts;
using AdionFA.Infrastructure.CrossingBuilder.Services;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.Directories.Services;
using AdionFA.Infrastructure.Extractor.Contracts;
using AdionFA.Infrastructure.Extractor.Services;
using AdionFA.Infrastructure.MetaTrader.Contracts;
using AdionFA.Infrastructure.MetaTrader.Services;
using AdionFA.Infrastructure.Persistence;
using AdionFA.Infrastructure.Persistence.Repositories;
using AdionFA.Infrastructure.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.StrategyBuilder.Services;
using AdionFA.Infrastructure.Weka.Contracts;
using AdionFA.Infrastructure.Weka.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;

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

            // Weka

            Kernel.Bind(typeof(IWekaApiClient)).To(typeof(WekaApiClient)).InSingletonScope();

            // Database

            Kernel.Bind(typeof(DbContext)).To(typeof(AdionFADbContext)).InParentScope();
            Kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));

            // Services

            Kernel.Bind<IProjectDirectoryService>().To<ProjectDirectoryService>();
            Kernel.Bind<IExtractorService>().To<ExtractorService>();
            Kernel.Bind<IStrategyBuilderService>().To<StrategyBuilderService>();
            Kernel.Bind<IAssemblyBuilderService>().To<AssemblyBuilderService>();
            Kernel.Bind<ICrossingBuilderService>().To<CrossingBuilderService>();
            Kernel.Bind<ITradeService>().To<TradeService>();

            // Application

            Kernel.Bind(typeof(IProjectService)).To(typeof(ProjectService));
            Kernel.Bind(typeof(IMarketDataService)).To(typeof(MarketDataService));
            Kernel.Bind(typeof(IGlobalConfigurationService)).To(typeof(GlobalConfigurationService));
            Kernel.Bind(typeof(ISettingService)).To(typeof(SettingService));
        }
    }
}
