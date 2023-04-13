using AdionFA.Core.Domain.Aggregates.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.Common
{
    [Table(nameof(EntityServiceHost))]
    public class EntityServiceHost : EntityBase
    {
        [Key]
        public int EntityServiceHostId { get; set; }

        public int EntityTypeId { get; set; }
        [ForeignKey(nameof(EntityTypeId))]
        public EntityType EntityType { get; set; }

        public int EntityId { get; set; }

        public long ProcessId { get; set; }
    }
}
