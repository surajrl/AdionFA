using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Common
{
    [Table(nameof(Setting))]
    public class Setting : EntityBase
    {
        [Key]
        public int SettingId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
