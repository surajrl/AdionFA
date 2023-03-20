using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Core
{
    [Table(nameof(CoreFeature))]
    public class CoreFeature : EntityBase
    {
        [Key]
        public int CoreFeatureId { get; set; }
    }
}
