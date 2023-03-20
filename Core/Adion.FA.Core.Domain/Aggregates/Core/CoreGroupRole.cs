using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Core
{
    [Table(nameof(CoreGroupRole))]
    public class CoreGroupRole : TimeSensitiveBase
    {
        [Key]
        public int CoreGroupRoleId { get; set; }

        public int CoreGroupId { get; set; }
        [ForeignKey(nameof(CoreGroupId))]
        public CoreGroup CoreGroup { get; set; }

        public int CoreRoleId { get; set; }
        [ForeignKey(nameof(CoreRoleId))]
        public CoreRole CoreRole { get; set; }
    }
}
