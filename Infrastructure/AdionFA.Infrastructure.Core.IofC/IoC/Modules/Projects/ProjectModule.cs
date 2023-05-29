using AdionFA.Core.API.Contracts.Projects;
using AdionFA.Core.API.Projects;
using AdionFA.Core.Application.Contract.Commons;
using AdionFA.Core.Application.Contracts.Projects;
using AdionFA.Core.Application.Services.Commons;
using AdionFA.Core.Application.Services.Projects;
using AdionFA.Core.Domain.Contracts.Commons;
using AdionFA.Core.Domain.Contracts.Projects;
using AdionFA.Core.Domain.Services.Commons;
using AdionFA.Core.Domain.Services.Projects;

using AdionFA.Infrastructure.Common.IofC;

using Ninject.Modules;

namespace AdionFA.Infrastructure.Core.IofC.Modules.Projects
{
    public class ProjectModule : NinjectModule
    {
        public override void Load()
        {
            // API

            Kernel.Bind(typeof(IProjectAPI)).To(typeof(ProjectAPI));

            // Application

            Kernel.Bind(typeof(IProjectAppService)).To(typeof(ProjectAppService));
            Kernel.Bind(typeof(IConfigurationAppService)).To(typeof(ConfigurationAppService));

            // Domain

            Kernel.Bind(typeof(IProjectDomainService)).To(typeof(ProjectDomainService))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));

            Kernel.Bind(typeof(IConfigurationDomainService)).To(typeof(ConfigurationDomainService))
                .WithConstructorArgument("ownerId", ctx => IoC.GetArgument(ctx, "ownerId"))
                .WithConstructorArgument("owner", ctx => IoC.GetArgument(ctx, "owner"));
        }
    }
}
