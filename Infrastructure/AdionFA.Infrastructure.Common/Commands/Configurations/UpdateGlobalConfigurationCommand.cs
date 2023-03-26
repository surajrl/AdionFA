namespace AdionFA.Infrastructure.Common.Commands.Configurations
{
    /*#region Get GlobalConfigurationDTO
    public class GetGlobalConfigurationCommand : IRequest<GlobalConfigurationBO>
    {
        public string tenantId { get; set; }
    }

    public class GetGlobalConfigurationCommandHandler : RequestHandler<GetGlobalConfigurationCommand, GlobalConfigurationBO>
    {
        protected override GlobalConfigurationBO Handle(GetGlobalConfigurationCommand request)
        {
            var service = IoC.IoC.Get<IConfigurationAppService>(request.tenantId);
            GlobalConfigurationBO dto = service.GetGlobalConfiguration(true);
            return dto;
        }
    }
    #endregion

    #region Update GlobalConfigurationDTO
    public class UpdateGlobalConfigurationCommand : IRequest
    {
        public string tenantId { get; set; }
        public GlobalConfigurationBO dto { get; set; }
    }

    public class UpdateGlobalConfigurationCommandHandler : RequestHandler<UpdateGlobalConfigurationCommand>
    {
        protected override void Handle(UpdateGlobalConfigurationCommand request)
        {
            var service = IoC.IoC.Get<IConfigurationAppService>(request.tenantId);
            service.UpdateGlobalConfiguration(request.dto);
        }
    }
    #endregion*/
}
