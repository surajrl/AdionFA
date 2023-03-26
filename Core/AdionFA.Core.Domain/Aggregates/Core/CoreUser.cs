using AdionFA.Core.Domain.Aggregates.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.Core
{
    [Table(nameof(CoreUser))]
    public class CoreUser : EntityBase
    {
        [Key]
        public string UserId { get; set; }

        public int UserTypeId { get; set; }
        [ForeignKey(nameof(UserTypeId))]
        public CoreUserType UserType { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public bool AccessDisabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime? LastAccessFailed { get; set; }
        public DateTime? LastAccess { get; set; }
        public string HashCodeResetPassword { get; set; }
        public bool ConfirmedEmail { get; set; }
        public string ConfirmationToken { get; set; }
        public bool CodeOfConductRead { get; set; }
    }
}
