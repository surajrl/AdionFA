using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Core
{
    [Table(nameof(CoreComponent))]
    public class CoreComponent : EntityBase
    {
        [Key]
        public int CoreComponentId { get; set; }

        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool ShowInMenu { get; set; }
    }
}
