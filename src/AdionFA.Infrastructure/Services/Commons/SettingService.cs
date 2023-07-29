using AdionFA.Application.Contracts;
using AdionFA.Domain.Entities;
using AdionFA.Infrastructure.Persistence;
using AdionFA.Infrastructure.Services;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using System.Linq;
using System.Threading.Tasks;

namespace AdionFA.Application.Services.Commons
{
    public class SettingService : AppServiceBase, ISettingService
    {
        public SettingService()
            : base()
        {
        }

        public SettingDTO GetSetting(int settingId)
        {
            using var dbContext = new AdionFADbContext();

            var setting = dbContext.Set<Setting>().FirstOrDefault(e => e.SettingId == settingId && !e.IsDeleted);

            return Mapper.Map<SettingDTO>(setting);
        }

        public async Task<ResponseDTO> UpdateSettingAsync(SettingDTO settingDTO)
        {
            using var dbContext = new AdionFADbContext();

            var response = new ResponseDTO
            {
                IsSuccess = false
            };

            var setting = Mapper.Map<Setting>(settingDTO);

            dbContext.Set<Setting>().Update(setting);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            response.IsSuccess = true;

            return response;
        }
    }
}
