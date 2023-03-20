using Adion.FA.Core.Application.Contract.Contracts;
using Adion.FA.TransferObject.Common;

namespace Adion.FA.Core.Application.Contracts.Commons
{
    public interface ISharedAppService : IAppContractBase
    {
        public EntityServiceHostDTO GetEntityServiceHost(int entityTypeId, int entityId);
    }
}
