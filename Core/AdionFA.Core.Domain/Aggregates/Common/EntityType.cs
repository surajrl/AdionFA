using AdionFA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.Common
{
    [Table(nameof(EntityType))]
    public class EntityType : ReferenceDataBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EntityTypeId { get; set; }
    }
}
