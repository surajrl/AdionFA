using AdionFA.UI.Infrastructure.Base;
using System;

namespace AdionFA.UI.Infrastructure.Model.Base
{
    public class EntityBaseVM : ViewModelBase
    {
        public bool IsDeleted { get; set; }

        public string CreatedById { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime CreatedOn { get; set; }

        public string UpdatedById { get; set; }
        public string UpdatedByUserName { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
