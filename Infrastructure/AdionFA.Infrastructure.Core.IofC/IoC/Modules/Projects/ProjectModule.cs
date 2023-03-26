using AdionFA.Core.API.Contracts.Projects;
using AdionFA.Core.API.Projects;
using AdionFA.Core.Application.Contracts.Projects;
using AdionFA.Core.Application.Services.Projects;
using AdionFA.Core.Domain.Contracts.Projects;
using AdionFA.Core.Domain.Services.Projects;
using AdionFA.Infrastructure.Common.IofC;
using Ninject.Modules;

namespace AdionFA.Infrastructure.Core.IofC.Modules.Projects
{
    public class ProjectModule : NinjectModule
    {
        public override void Load()
        {
            #region API

            Kernel.Bind(typeof(IProjectAPI)).To(typeof(ProjectAPI));

            #endregion API

            #region Application

            Kernel.Bind(typeof(IProjectAppService)).To(typeof(ProjectAppService));
            Kernel.Bind(typeof(IGlobalConfigurationAppService)).To(typeof(GlobalConfigurationAppService));

            #endregion Application

            #region Domain

            Kernel.Bind(typeof(IProjectDomainService)).To(typeof(ProjectDomainService))
                .WithConstructorArgument("tenantId", ctx => IoC.GetArgument(ctx, "tenantId"))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));

            Kernel.Bind(typeof(IGlobalConfigurationDomainService)).To(typeof(GlobalConfigurationDomainService))
                .WithConstructorArgument("tenantId", ctx => IoC.GetArgument(ctx, "tenantId"))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));

            #endregion Domain
        }
    }
}