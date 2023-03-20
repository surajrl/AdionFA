using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.ReferenceData
{
    [Table(nameof(Language))]
    public class Language : ReferenceDataBase
    {
        [Key]
        public int LanguageId { get; set; }
    }
}
