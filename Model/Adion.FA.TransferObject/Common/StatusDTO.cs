using Adion.FA.TransferObject.Base;

namespace Adion.FA.TransferObject.Common
{
    public class StatusDTO : ReferenceDataBaseDTO
    {
        public int StatusId { get; set; }

        public int EntityTypeId { get; set; }
        public EntityTypeDTO EntityType { get; set; }
    }
}
