using AdionFA.UI.Station.Infrastructure.Model.Base;

namespace AdionFA.UI.Station.Infrastructure.Model.Common
{
    public class EntityServiceHostVM : EntityBaseVM
    {
        public int EntityServiceHostId { get; set; }

        public int EntityTypeId { get; set; }
        public EntityTypeVM EntityType { get; set; }

        public int EntityId { get; set; }

        public long ProcessId { get; set; }
    }
}
