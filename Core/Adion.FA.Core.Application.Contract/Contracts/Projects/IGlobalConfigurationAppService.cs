using Adion.FA.Core.Application.Contract.Contracts;
using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Project;
using System.Collections.Generic;

namespace Adion.FA.Core.Application.Contracts.Projects
{
    public interface IGlobalConfigurationAppService : IAppContractBase
    {
        IList<ProjectGlobalConfigurationDTO> GetAllGlobalConfigurations(bool includeGraph = false);
        ProjectGlobalConfigurationDTO GetGlobalConfiguration(int? configurationId = null, bool includeGraph = false);
        ResponseDTO UpdateGlobalConfiguration(ProjectGlobalConfigurationDTO configuration);
    }
}
                                                                            