using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Aggregates.Common;
using Adion.FA.Core.Domain.Aggregates.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Contact
{
    [Table(nameof(Contact))]
    public class Contact : PersonalDataBase
    {
        [Key]
        public int ContactId { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public CoreUser User { get; set; }

        public string Description { get; set; }


        //[Index]
        //[max]
        public int? EntityId { get; set; }
        public int? EntityTypeId { get; set; }
        [ForeignKey(nameof(EntityTypeId))]
        public EntityType EntityType { get; set; }

        public int? GenderId { get; set; }
        [ForeignKey(nameof(GenderId))]
        public Gender Gender { get; set; }

        public int? ContactMethodId { get; set; }
        [ForeignKey(nameof(ContactMethodId))]
        public ContactMethod ContactMethod { get; set; }


        public int? PrimaryAddressId { get; set; }
        [ForeignKey(nameof(PrimaryAddressId))]
        public Address PrimaryAddress { get; set; }

        public int? PrimaryPhoneId { get; set; }
        [ForeignKey(nameof(PrimaryPhoneId))]
        public Phone PrimaryPhone { get; set; }
        
        public int? PrimaryEmailId { get; set; }
        [ForeignKey(nameof(PrimaryEmailId))]
        public Email PrimaryEmail { get; set; }
        
        public int? PrimaryIdentificationId { get; set; }
        [ForeignKey(nameof(PrimaryIdentificationId))]
        public Identification PrimaryIdentification { get; set; }

        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public bool HasApplicationAccess { get; set; }
    }

    [Table(nameof(ContactMethod))]
    public class ContactMethod : ReferenceDataBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContactMethodId { get; set; }
    }
}
