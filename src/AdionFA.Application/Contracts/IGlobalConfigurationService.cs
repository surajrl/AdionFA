using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System.Threading.Tasks;

namespace AdionFA.Application.Contracts
{
    public interface IGlobalConfigurationService
    {
        GlobalConfigurationDTO GetGlobalConfiguration();
        Task<ResponseDTO> UpdateGlobalConfigurationAsync(GlobalConfigurationDTO configurationDto);
    }
}
