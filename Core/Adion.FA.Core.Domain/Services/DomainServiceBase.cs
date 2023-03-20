namespace Adion.FA.Core.Domain.Services
{
    public class DomainServiceBase
    {
        public readonly string _tenantId;
        public readonly string _ownerId;
        public readonly string _owner;

        public DomainServiceBase(string tenantId, string ownerId, string owner)
        {
            _tenantId = tenantId;
            _ownerId = ownerId;
            _owner = owner;
        }

        public string TenantId => _tenantId;
        public string OwnerId => _ownerId;
        public string Owner => _owner;
    }
}
