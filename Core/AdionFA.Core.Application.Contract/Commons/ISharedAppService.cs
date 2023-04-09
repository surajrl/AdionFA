using AdionFA.TransferObject.Common;

namespace AdionFA.Core.Application.Contracts.Commons
{
    public interface ISharedAppService : IAppContractBase
    {
        public EntityServiceHostDTO GetEntityServiceHost(int entityTypeId, int entityId);
    }
}
