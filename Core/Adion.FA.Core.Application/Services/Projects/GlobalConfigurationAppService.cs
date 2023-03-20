using Adion.FA.Core.Application.Contracts.Projects;
using Adion.FA.Core.Domain.Aggregates.Project;
using Adion.FA.Core.Domain.Contracts.Projects;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Adion.FA.TransferObject.Project;
using Adion.FA.TransferObject.Base;

namespace Adion.FA.Core.Application.Services.Projects
{
    public class GlobalConfigurationAppService : AppServiceBase, IGlobalConfigurationAppService
    {
        #region Domain Services

        [Inject]
        public IGlobalConfigurationDomainService GlobalConfigurationDomainService { get; set; }

        #endregion

        #region Ctor

        public GlobalConfigurationAppService() : base()
        { 
        }

        #endregion

        #region Global Configurations

        public IList<ProjectGlobalConfigurationDTO> GetAllGlobalConfigurations(bool includeGraph = false)
        {
            try
            {
                IList<ProjectGlobalConfiguration> configurations = GlobalConfigurationDomainService.GetAllProjectGlobalConfigurations(includeGraph);
                IList<ProjectGlobalConfigurationDTO> dto = Mapper
                    .Map<IList<ProjectGlobalConfigurationDTO>>(configurations);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<GlobalConfigurationAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ProjectGlobalConfigurationDTO GetGlobalConfiguration(int? configurationId = null, bool includeGraph = false)
        {
            try
            {
                ProjectGlobalConfiguration gc = GlobalConfigurationDomainService.GetProjectGlobalConfiguration(configurationId, includeGraph);
                ProjectGlobalConfigurationDTO dto = Mapper.Map<ProjectGlobalConfigurationDTO>(gc);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<GlobalConfigurationAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateGlobalConfiguration(ProjectGlobalConfigurationDTO configuration)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                ProjectGlobalConfiguration gc = Mapper.Map<ProjectGlobalConfiguration>(configuration);

                GlobalConfigurationDomainService.UpdateProjectGlobalConfiguration(gc);

                response.IsSuccess = true;
                
                if(response.IsSuccess)
                    LogInfoUpdate<ProjectGlobalConfigurationDTO>();

                return response;
            }
            catch (Exception ex)
            {
                LogException<GlobalConfigurationAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion
    }
}
