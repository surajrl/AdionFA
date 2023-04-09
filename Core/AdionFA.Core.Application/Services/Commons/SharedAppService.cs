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
        #region Repostories

        [Inject]
        public IRepository<EntityServiceHost> EntityServiceHostRepository { get; set; }

        #endregion Repostories

        #region Ctor

        public SharedAppService() : base()
        {
        }

        #endregion Ctor

        public EntityServiceHostDTO GetEntityServiceHost(int entityTypeId, int entityId)
        {
            try
            {
                EntityServiceHost ehost = EntityServiceHostRepository.FirstOrDefault(
                        esh => esh.EntityTypeId == entityTypeId &&
                               esh.EntityId == entityId);

                EntityServiceHostDTO dto = Mapper.Map<EntityServiceHostDTO>(ehost);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<SharedAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
