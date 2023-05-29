namespace AdionFA.Core.Domain.Services
{
    public class DomainServiceBase
    {
        public readonly string _ownerId;
        public readonly string _owner;

        public DomainServiceBase(string ownerId, string owner)
        {
            _ownerId = ownerId;
            _owner = owner;
        }

        public string OwnerId => _ownerId;
        public string Owner => _owner;
    }
}
