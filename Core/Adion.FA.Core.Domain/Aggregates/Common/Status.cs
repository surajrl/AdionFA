using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adion.FA.Core.Domain.Aggregates.Common
{
    [Table(nameof(Status))]
    public class Status : ReferenceDataBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

        public int EntityTypeId { get; set; }
        [ForeignKey(nameof(EntityTypeId))]
        public EntityType EntityType { get; set; }
    }
}
