using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Aggregates.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Contact
{
    [Table(nameof(Phone))]
    public class Phone : TimeSensitiveBase
    {
        [Key]
        public int PhoneId { get; set; }

        public int PhoneTypeId { get; set; }
        [ForeignKey(nameof(PhoneTypeId))]
        public PhoneType PhoneType { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
        public bool SmsAllowed { get; set; }

        public int? EntityId { get; set; }
        public int? EntityTypeId { get; set; }
        [ForeignKey(nameof(EntityTypeId))]
        public EntityType EntityType { get; set; }
    }
}
