using AdionFA.Application.Contracts;
using AdionFA.Domain.Entities;
using AdionFA.Infrastructure.Persistence;
using AdionFA.Infrastructure.Services;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
                .Include(e => e.NodeBuilderConfiguration)
                .Include(e => e.AssemblyBuilderConfiguration)
                .Include(e => e.CrossingBuilderConfiguration)
                .FirstOrDefault();

            return Mapper.Map<GlobalConfigurationDTO>(globalConfiguration);
        }

        public ResponseDTO UpdateGlobalConfiguration(GlobalConfigurationDTO globalConfigurationDTO)
        {
            using var dbContext = new AdionFADbContext();

            var response = new ResponseDTO
            {
                IsSuccess = false
            };

            var globalConfiguration = Mapper.Map<GlobalConfiguration>(globalConfigurationDTO);

            // Update

            globalConfiguration.UpdatedOn = DateTime.UtcNow;

            dbContext.Set<GlobalConfiguration>().Update(globalConfiguration);
            Logger.Information("GlobalConfigurationService.UpdateGlobalConfiguration() :: dbContext.Set<GlobalConfiguration>().Update().");

            if (dbContext.SaveChanges() > 0)
            {
                Logger.Information("GlobalConfigurationService.UpdateGlobalConfiguration() :: dbContext.SaveChanges().");
                response.IsSuccess = true;
            }

            return response;
        }
    }
}
