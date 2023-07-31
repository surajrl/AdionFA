using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System.Threading.Tasks;

namespace AdionFA.Application.Contracts
{
    public interface ISettingService
    {
        SettingDTO GetSetting(int settingId);
        Task<ResponseDTO> UpdateSettingAsync(SettingDTO settingDTO);
    }
}
