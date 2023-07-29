using AdionFA.Application.Contracts;
using AdionFA.Domain.Entities;
using AdionFA.Infrastructure.Persistence;
using AdionFA.Infrastructure.Services;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AdionFA.Application.Services.Commons
{
    public class GlobalConfigurationService : AppServiceBase, IGlobalConfigurationService
    {
        public GlobalConfigurationService()
            : base()
        {
        }

        public GlobalConfigurationDTO GetGlobalConfiguration()
        {
            using var dbContext = new AdionFADbContext();

            var globalConfiguration = dbContext.Set<GlobalConfiguration>()
                .Where(e => !e.IsDeleted)
                .Include(e => e.GlobalScheduleConfigurations)
                    .ThenInclude(e => e.MarketRegion)
                .FirstOrDefault();

            return Mapper.Map<GlobalConfigurationDTO>(globalConfiguration);
        }

        public async Task<ResponseDTO> UpdateGlobalConfigurationAsync(GlobalConfigurationDTO globalConfigurationDTO)
        {
            using var dbContext = new AdionFADbContext();

            var response = new ResponseDTO
            {
                IsSuccess = false
            };

            var globalConfiguration = Mapper.Map<GlobalConfiguration>(globalConfigurationDTO);

            dbContext.Set<GlobalConfiguration>().Update(globalConfiguration);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            response.IsSuccess = true;

            return response;
        }
    }
}
