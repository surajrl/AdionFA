using AdionFA.UI.Station.Infrastructure.Model.Base;
using System;

namespace AdionFA.UI.Station.Infrastructure.Model.Core
{
    public class CoreUserVM : EntityBaseVM
    {
        public string UserId { get; set; }

        public string UserTypeId { get; set; }
        public CoreUserTypeVM UserType { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
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
