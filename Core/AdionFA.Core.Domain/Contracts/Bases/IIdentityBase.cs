namespace AdionFA.Core.Domain.Contracts.Bases
{
    public interface IIdentityBase
    {
        public string _ownerId { get; }
        public string _owner { get; }
    }
}
