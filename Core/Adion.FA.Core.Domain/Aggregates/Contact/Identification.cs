using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Aggregates.Common;
using Adion.FA.Core.Domain.Aggregates.ReferenceData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Contact
{
    [Table(nameof(Identification))]
    public class Identification : TimeSensitiveBase
    {
        [Key]
        public int IdentificationId { get; set; }

        public int IdentificationTypeId { get; set; }
        [ForeignKey(nameof(IdentificationTypeId))]
        public IdentificationType IdentificationType { get; set; }
        
        [Required]
        public string IdentificationNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? IssueDate { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool NoExpirationDate { get; set; }

        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        public int? EntityId { get; set; }
        public int? EntityTypeId { get; set; }
        [ForeignKey(nameof(EntityTypeId))]
        public EntityType EntityType { get; set; }

        public ICollection<File> ProofsOfId { get; set; }
    }
}
