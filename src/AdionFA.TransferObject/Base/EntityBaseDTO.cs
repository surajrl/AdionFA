using System;

namespace AdionFA.TransferObject.Base
{
    public abstract class EntityBaseDTO
    {
        public string CreatedById { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime CreatedOn { get; set; }

        public string UpdatedById { get; set; }
        public string UpdatedByUserName { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; }

    }
}
