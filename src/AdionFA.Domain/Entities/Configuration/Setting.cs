using AdionFA.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(Setting))]
    public class Setting : ReferenceDataBase
    {
        [Key]
        public int SettingId { get; set; }
    }
}
