namespace Adion.FA.Core.Domain.Contracts.Bases
{
    public interface IIdentityBase
    {
        public string _tenantId { get; }
        public string _ownerId { get; }
        public string _owner { get; }
    }
}
