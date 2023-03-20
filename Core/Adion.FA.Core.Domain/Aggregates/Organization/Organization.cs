using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Organization
{

    [Table(nameof(Organization))]
    public class Organization : EntityBase
    {
        [Key]
        public string OrganizationId { get; set; }

        public string ParentOrganizationId { get; set; }
        [ForeignKey(nameof(ParentOrganizationId))]
        public Organization ParentOrganization { get; set; }


        [Required]
        public string Name { get; set; }
        public string LegalName { get; set; }

        /*public int? CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        public int? CultureId { get; set; }
        [ForeignKey(nameof(CultureId))]
        public Culture Culture { get; set; }

        public int? LanguageId { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }

        public int? TimeZoneId { get; set; }
        [ForeignKey(nameof(TimeZoneId))]
        public TimeZone TimeZone { get; set; }

        public int? CurrencyId { get; set; }
        [ForeignKey(nameof(CurrencyId))]
        public Currency Currency { get; set; }*/
    }
}
