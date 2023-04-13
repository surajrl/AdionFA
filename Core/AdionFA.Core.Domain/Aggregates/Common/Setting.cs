using AdionFA.Core.Domain.Aggregates.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.Common
{
    [Table(nameof(Setting))]
    public class Setting : ReferenceDataBase
    {
        [Key]
        public int SettingId { get; set; }
    }
}
