using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Aggregates.Contact;
using Adion.FA.Core.Domain.Aggregates.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Organization
{
    [Table(nameof(Employee))]
    public class Employee : PersonalDataBase
    {
        [Key]
        public int EmployeeId { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public CoreUser User { get; set; }

        public int? PrimaryPhoneId { get; set; }
        [ForeignKey(nameof(PrimaryPhoneId))]
        public Phone PrimaryPhone { get; set; }

        public int? PrimaryEmailId { get; set; }
        [ForeignKey(nameof(PrimaryEmailId))]
        public Email PrimaryEmail { get; set; }
    }
}
