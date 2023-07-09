using AdionFA.Core.Application.Contracts.Commons;
using AdionFA.Core.Domain.Aggregates.Common;
using AdionFA.Core.Domain.Contracts.Repositories;
using AdionFA.TransferObject.Common;
using Ninject;
using System;
using System.Diagnostics;

namespace AdionFA.Core.Application.Services.Commons
{
    public class SharedAppService : AppServiceBase, ISharedAppService
    {
        [Inject]
        public IRepository<EntityServiceHost> EntityServiceHostRepository { get; set; }

        public SharedAppService() : base()
        {
        }

        public EntityServiceHostDTO GetEntityServiceHost(int entityTypeId, int entityId)
        {
            try
            {
                var ehost = EntityServiceHostRepository.FirstOrDefault(
                        esh => esh.EntityTypeId == entityTypeId &&
                               esh.EntityId == entityId);

                var dto = Mapper.Map<EntityServiceHostDTO>(ehost);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
