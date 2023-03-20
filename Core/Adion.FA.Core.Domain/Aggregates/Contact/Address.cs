using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Aggregates.Common;
using Adion.FA.Core.Domain.Aggregates.ReferenceData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Contact
{
    [Table(nameof(Address))]
    public class Address : TimeSensitiveBase
    {
        [Key]
        public int AddressId { get; set; }

        public int AddressTypeId { get; set; }
        [ForeignKey(nameof(AddressTypeId))]
        public AddressType AddressType { get; set; }

        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        
        public int? CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        
        public int? EntityId { get; set; }
        public int? EntityTypeId { get; set; }
        [ForeignKey(nameof(EntityTypeId))]
        public EntityType EntityType { get; set; }

    }
}
