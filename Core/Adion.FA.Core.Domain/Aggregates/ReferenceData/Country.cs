using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.ReferenceData
{
    [Table(nameof(Country))]
    public class Country : ReferenceDataBaseI18n
    {
        [Key]
        public int CountryId { get; set; }

        [StringLength(5)]
        public string Extension { get; set; }

        public int? GeoAreaId { get; set; }
        [ForeignKey(nameof(GeoAreaId))]
        public GeoArea GeoArea { get; set; }
    }
}
