using AdionFA.Core.API.Commons;
using AdionFA.Core.API.Contracts.Commons;
using AdionFA.Core.API.Contracts.MarketData;
using AdionFA.Core.API.Contracts.MetaTrader;
using AdionFA.Core.API.Contracts.Projects;
using AdionFA.Core.API.MarketData;
using AdionFA.Core.API.MetaTrader;
using AdionFA.Core.API.Projects;
using AdionFA.Core.Application.Contract.Commons;
using AdionFA.Core.Application.Contracts.Commons;
using AdionFA.Core.Application.Contracts.MarketData;
using AdionFA.Core.Application.Contracts.MetaTrader;
using AdionFA.Core.Application.Contracts.Projects;
using AdionFA.Core.Application.Services.Commons;
using AdionFA.Core.Application.Services.MarketData;
using AdionFA.Core.Application.Services.MetaTrader;
using AdionFA.Core.Application.Services.Projects;
using AdionFA.Core.Domain.Contracts.Commons;
using AdionFA.Core.Domain.Contracts.MarketData;
using AdionFA.Core.Domain.Contracts.MetaTrader;
using AdionFA.Core.Domain.Contracts.Projects;
using AdionFA.Core.Domain.Contracts.Repositories;
using AdionFA.Core.Domain.Services.Commons;
using AdionFA.Core.Domain.Services.MarketData;
using AdionFA.Core.Domain.Services.MetaTrader;
using AdionFA.Core.Domain.Services.Projects;
using AdionFA.Infrastructure.Common.AssemblyBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssemblyBuilder.Services;
using AdionFA.Infrastructure.Common.CrossingBuilder.Contracts;
using AdionFA.Infrastructure.Common.CrossingBuilder.Services;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Directories.Services;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Services;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.MediatR;
using AdionFA.Infrastructure.Common.MetaTrader.Contracts;
using AdionFA.Infrastructure.Common.MetaTrader.Services;
using AdionFA.Infrastructure.Common.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.StrategyBuilder.Services;
using AdionFA.Infrastructure.Common.Transaction.Contracts;
using AdionFA.Infrastructure.Common.Transaction.Services;
using AdionFA.Infrastructure.Common.Weka.Contracts;
using AdionFA.Infrastructure.Common.Weka.Services;
using AdionFA.Infrastructure.Core.AutoMappers;
using AdionFA.Infrastructure.Core.Data.Persistence;
using AdionFA.Infrastructure.Core.Data.Repositories;

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;
using System;

namespace AdionFA.Infrastructure.Core.IofC
{
    public class ModuleBase : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ILoggerFactory>().ToMethod(f => LoggerFactory.Create(b => b.AddDebug())).InSingletonScope();

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

            Kernel.Bind(typeof(ITransaction)).To(typeof(Transaction));
            Kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));

            // Services

            Kernel.Bind<IProjectDirectoryService>().To<ProjectDirectoryService>();
            Kernel.Bind<IExtractorService>().To<ExtractorService>();
            Kernel.Bind<IStrategyBuilderService>().To<StrategyBuilderService>();
            Kernel.Bind<IAssemblyBuilderService>().To<AssemblyBuilderService>();
            Kernel.Bind<ICrossingBuilderService>().To<CrossingBuilderService>();
            Kernel.Bind<ITradeService>().To<TradeService>();

            // API

            Kernel.Bind(typeof(ISharedAPI)).To(typeof(SharedAPI));
            Kernel.Bind(typeof(IProjectAPI)).To(typeof(ProjectAPI));
            Kernel.Bind(typeof(IExpertAdvisorAPI)).To(typeof(ExpertAdvisorAPI));
            Kernel.Bind(typeof(IMarketDataAPI)).To(typeof(MarketDataAPI));

            // Application

            Kernel.Bind(typeof(ISharedAppService)).To(typeof(SharedAppService));
            Kernel.Bind(typeof(IProjectAppService)).To(typeof(ProjectAppService));
            Kernel.Bind(typeof(IExpertAdvisorAppService)).To(typeof(ExpertAdvisorAppService));
            Kernel.Bind(typeof(IMarketDataAppService)).To(typeof(MarketDataAppService));
            Kernel.Bind(typeof(IConfigurationAppService)).To(typeof(ConfigurationAppService));
            Kernel.Bind(typeof(IAppSettingAppService)).To(typeof(AppSettingAppService));

            // Domain

            Kernel.Bind(typeof(IProjectDomainService)).To(typeof(ProjectDomainService))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));

            Kernel.Bind(typeof(IConfigurationDomainService)).To(typeof(ConfigurationDomainService))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));

            Kernel.Bind(typeof(IMarketDataDomainService)).To(typeof(MarketDataDomainService))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));

            Kernel.Bind(typeof(IAppSettingDomainService)).To(typeof(AppSettingDomainService))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));

            Kernel.Bind(typeof(IExpertAdvisorDomainService)).To(typeof(ExpertAdvisorDomainService))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));
        }
    }
}
