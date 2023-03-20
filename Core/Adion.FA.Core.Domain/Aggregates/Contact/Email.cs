using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Aggregates.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Contact
{
    [Table(nameof(Email))]
    public class Email : TimeSensitiveBase
    {
        [Key]
        public int EmailId { get; set; }

        public int EmailTypeId { get; set; }
        [ForeignKey(nameof(EmailTypeId))]
        public EmailType EmailType { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        
        public int? EntityId { get; set; }
        public int? EntityTypeId { get; set; }
        [ForeignKey(nameof(EntityTypeId))]
        public EntityType EntityType { get; set; }
    }
}
