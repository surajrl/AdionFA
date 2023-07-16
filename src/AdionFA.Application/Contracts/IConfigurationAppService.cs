using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System.Collections.Generic;

namespace AdionFA.Application.Contracts
{
    public interface IConfigurationAppService
    {
        IList<ConfigurationDTO> GetAllConfiguration(bool includeGraph = false);
        ConfigurationDTO GetConfiguration(int? configurationId = null, bool includeGraph = false);
        ResponseDTO UpdateConfiguration(ConfigurationDTO configurationDto);
    }
}
