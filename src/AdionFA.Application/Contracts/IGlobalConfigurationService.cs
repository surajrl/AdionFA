using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;

namespace AdionFA.Application.Contracts
{
    public interface IGlobalConfigurationService
    {
        GlobalConfigurationDTO GetGlobalConfiguration();
        ResponseDTO UpdateGlobalConfiguration(GlobalConfigurationDTO globalConfigurationDTO);
    }
}