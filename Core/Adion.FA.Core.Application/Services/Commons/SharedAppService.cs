using Adion.FA.Core.Application.Contracts.Commons;
using Adion.FA.Core.Domain.Aggregates.Common;
using Adion.FA.Core.Domain.Contracts.Repositories;
using Adion.FA.TransferObject.Common;
using Ninject;
using System;
using System.Diagnostics;

namespace Adion.FA.Core.Application.Services.Commons
{
    public class SharedAppService : AppServiceBase, ISharedAppService
    {
        #region Repostories

        [Inject]
        public IRepository<EntityServiceHost> EntityServiceHostRepository { get; set; }

        #endregion

        #region Ctor

        public SharedAppService() : base()
        {
        }

        #endregion

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
