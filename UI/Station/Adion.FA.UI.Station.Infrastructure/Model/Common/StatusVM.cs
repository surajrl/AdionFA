using Adion.FA.UI.Station.Infrastructure.Model.Base;

namespace Adion.FA.UI.Station.Infrastructure.Model.Common
{
    public class StatusVM : ReferenceDataBaseVM
    {
        public int StatusId { get; set; }

        public int EntityTypeId { get; set; }
        public EntityTypeVM EntityType { get; set; }
    }
}
