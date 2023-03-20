using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Contact
{
    [Table(nameof(AddressType))]
    public class AddressType : ReferenceDataBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AddressTypeId { get; set; }
    }

    [Table(nameof(MaritalStatus))]
    public class MaritalStatus : ReferenceDataBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaritalStatusId { get; set; }
    }

    [Table(nameof(Occupation))]
    public class Occupation : ReferenceDataBase
    {
        [Key]
        public int OccupationId { get; set; }
    }


    [Table(nameof(Gender))]
    public class Gender : ReferenceDataBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GenderId { get; set; }
    }

    [Table(nameof(PhoneType))]
    public class PhoneType : ReferenceDataBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PhoneTypeId { get; set; }
    }

    [Table(nameof(EmailType))]
    public class EmailType : ReferenceDataBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmailTypeId { get; set; }
    }

    [Table(nameof(IdentificationType))]
    public class IdentificationType : ReferenceDataBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdentificationTypeId { get; set; }
    }
}
