using Adion.FA.Core.API.Contracts.Projects;
using Adion.FA.Core.API.Projects;
using Adion.FA.Core.Application.Contracts.Projects;
using Adion.FA.Core.Application.Services.Projects;
using Adion.FA.Core.Domain.Contracts.Projects;
using Adion.FA.Core.Domain.Services.Projects;
using Adion.FA.Infrastructure.Common.IofC;
using Ninject.Modules;

namespace Adion.FA.Infrastructure.Core.IofC.Modules.Projects
{
    public class ProjectModule : NinjectModule
    {
        public override void Load()
        {
            #region API
            Kernel.Bind(typeof(IProjectAPI)).To(typeof(ProjectAPI));
            #endregion

            #region Application
            Kernel.Bind(typeof(IProjectAppService)).To(typeof(ProjectAppService));
            Kernel.Bind(typeof(IGlobalConfigurationAppService)).To(typeof(GlobalConfigurationAppService));
            #endregion

            #region Domain
            Kernel.Bind(typeof(IProjectDomainService)).To(typeof(ProjectDomainService))
                .WithConstructorArgument("tenantId", ctx => IoC.GetArgument(ctx, "tenantId"))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));

            Kernel.Bind(typeof(IGlobalConfigurationDomainService)).To(typeof(GlobalConfigurationDomainService))
                .WithConstructorArgument("tenantId", ctx => IoC.GetArgument(ctx, "tenantId"))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));
            #endregion
        }
    }
}
