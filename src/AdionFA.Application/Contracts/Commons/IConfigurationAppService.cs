using AdionFA.Application.Contracts;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System.Collections.Generic;

namespace AdionFA.Application.Contract.Commons
{
    public interface IConfigurationAppService : IAppContractBase
    {
        IList<ConfigurationDTO> GetAllConfiguration(bool includeGraph = false);
        ConfigurationDTO GetConfiguration(int? configurationId = null, bool includeGraph = false);
        ResponseDTO UpdateConfiguration(ConfigurationDTO configurationDto);
    }
}
