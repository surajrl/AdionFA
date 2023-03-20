using Adion.FA.Core.Domain.Contracts.Repositories;
using Adion.FA.Infrastructure.Common.Comparators;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Common.Logger.Contracts;
using Adion.FA.Infrastructure.Common.Logger.Services;
using Adion.FA.Infrastructure.Common.MediatR;
using Adion.FA.Infrastructure.Common.Transaction.Contracts;
using Adion.FA.Infrastructure.Common.Transaction.Services;
using Adion.FA.Infrastructure.Common.Weka.Contracts;
using Adion.FA.Infrastructure.Common.Weka.Services;
using Adion.FA.Infrastructure.Core.AutoMappers;
using Adion.FA.Infrastructure.Core.Data.Persistence;
using Adion.FA.Infrastructure.Core.Data.Persistence.EFCore;
using Adion.FA.Infrastructure.Core.Data.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using System;

namespace Adion.FA.Infrastructure.Core.IofC
{
    public class ModuleBase : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(typeof(IComparator)).To(typeof(Comparator)).InCallScope();
            Kernel.Bind(typeof(ILoggerHandler)).To(typeof(LoggerHandler)).InCallScope();
            Kernel.Bind<ILoggerFactory>().ToMethod(f => LoggerFactory.Create(b => b.AddDebug())).InSingletonScope();
            
            Kernel.Bind<IMapper>().ToMethod(automapper => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingDomainProfile());
            }).CreateMapper()).InSingletonScope();

            Kernel.Bind(typeof(IMediator)).ToMethod(f => MediatRManager.BuildMediator(new WrappingWriter(Console.Out))).InSingletonScope();

            #region Serilog
            //string templateSerilog = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception} {Properties:j}";
            Kernel.Bind<Serilog.ILogger>().ToMethod(x => new LoggerConfiguration()
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() }))
                .WriteTo.Console()//(outputTemplate: templateSerilog)
                .WriteTo.Debug()//(outputTemplate: templateSerilog)
                .WriteTo.Map("name", "default", (tenant, wt) =>
                    wt.File($"./logs/project-{tenant}/log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10
                    //,outputTemplate: templateSerilog
                    ),
                    sinkMapCountLimit: 0
                )
                .CreateLogger()).InSingletonScope();
            #endregion

            #region Weka
            Kernel.Bind(typeof(IWekaApiClient)).To(typeof(WekaApiClient)).InSingletonScope();
            #endregion

            Kernel.Bind(typeof(IUnitOfWork<>)).To(typeof(UnitOfWork<>)).InCallScope()
                .WithConstructorArgument("tenantId", ctx => IoC.GetArgument(ctx, "tenantId"))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));
            Kernel.Bind(typeof(AdionFADbContext)).ToSelf().WhenInjectedInto(typeof(IUnitOfWork<>)).InParentScope();
            Kernel.Bind(typeof(AdionSecurityDbContext)).ToSelf().WhenInjectedInto(typeof(IUnitOfWork<>)).InParentScope();
            Kernel.Bind(typeof(ITransaction)).To(typeof(Transaction));
            Kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            Kernel.Bind(typeof(ISecurityRepository<>)).To(typeof(SecurityRepository<>));
        }
    }
}
