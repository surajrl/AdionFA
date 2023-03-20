using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Core
{
    [Table(nameof(CoreRole))]
    public class CoreRole : TimeSensitiveBase
    {
        [Key]
        public int CoreRoleId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
