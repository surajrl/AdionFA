using AdionFA.Application.Contract.Commons;
using AdionFA.Application.Contracts.Commons;
using AdionFA.Application.Contracts.MarketData;
using AdionFA.Application.Contracts.MetaTrader;
using AdionFA.Application.Contracts.Projects;
using AdionFA.Application.Services.Commons;
using AdionFA.Application.Services.MarketData;
using AdionFA.Application.Services.MetaTrader;
using AdionFA.Application.Services.Projects;
using AdionFA.Domain.Contracts.Repositories;
using AdionFA.Infrastructure.AssemblyBuilder.Contracts;
using AdionFA.Infrastructure.AssemblyBuilder.Services;
using AdionFA.Infrastructure.CrossingBuilder.Contracts;
using AdionFA.Infrastructure.CrossingBuilder.Services;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.Directories.Services;
using AdionFA.Infrastructure.Extractor.Contracts;
using AdionFA.Infrastructure.Extractor.Services;
using AdionFA.Infrastructure.MediatR;
using AdionFA.Infrastructure.MetaTrader.Contracts;
using AdionFA.Infrastructure.MetaTrader.Services;
using AdionFA.Infrastructure.Persistance.AutoMappers;
using AdionFA.Infrastructure.Persistance.Contracts;
using AdionFA.Infrastructure.Persistance.Repositories;
using AdionFA.Infrastructure.Persistence.EFCore;
using AdionFA.Infrastructure.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.StrategyBuilder.Services;
using AdionFA.Infrastructure.Weka.Contracts;
using AdionFA.Infrastructure.Weka.Services;
using AutoMapper;
using MediatR;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;
using System;

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

            Kernel.Bind(typeof(IMediator)).ToMethod(f => MediatRManager.BuildMediator(new WrappingWriter(Console.Out))).InSingletonScope();

            // Weka

            Kernel.Bind(typeof(IWekaApiClient)).To(typeof(WekaApiClient)).InSingletonScope();

            // Database

            Kernel.Bind(typeof(IUnitOfWork<>)).To(typeof(UnitOfWork<>)).InCallScope()
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));

            Kernel.Bind(typeof(AdionFADbContext)).ToSelf().WhenInjectedInto(typeof(IUnitOfWork<>)).InParentScope();

            Kernel.Bind(typeof(ITransaction)).To(typeof(Transactions));
            Kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));

            // Services

            Kernel.Bind<IProjectDirectoryService>().To<ProjectDirectoryService>();
            Kernel.Bind<IExtractorService>().To<ExtractorService>();
            Kernel.Bind<IStrategyBuilderService>().To<StrategyBuilderService>();
            Kernel.Bind<IAssemblyBuilderService>().To<AssemblyBuilderService>();
            Kernel.Bind<ICrossingBuilderService>().To<CrossingBuilderService>();
            Kernel.Bind<ITradeService>().To<TradeService>();

            // Application

            Kernel.Bind(typeof(IProjectAppService)).To(typeof(ProjectAppService));
            Kernel.Bind(typeof(IExpertAdvisorAppService)).To(typeof(ExpertAdvisorAppService));
            Kernel.Bind(typeof(IMarketDataAppService)).To(typeof(MarketDataAppService));
            Kernel.Bind(typeof(IConfigurationAppService)).To(typeof(ConfigurationAppService));
            Kernel.Bind(typeof(IAppSettingAppService)).To(typeof(AppSettingAppService));
        }
    }
}
