using AdionFA.Core.Application.Contract.Contracts;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using System.Collections.Generic;

namespace AdionFA.Core.Application.Contracts.Projects
{
    public interface IGlobalConfigurationAppService : IAppContractBase
    {
        IList<ProjectGlobalConfigurationDTO> GetAllGlobalConfigurations(bool includeGraph = false);
        ProjectGlobalConfigurationDTO GetGlobalConfiguration(int? configurationId = null, bool includeGraph = false);
        ResponseDTO UpdateGlobalConfiguration(ProjectGlobalConfigurationDTO configuration);
    }
}
                                                                            