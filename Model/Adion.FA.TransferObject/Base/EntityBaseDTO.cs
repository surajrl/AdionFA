using System;

namespace Adion.FA.TransferObject.Base
{
    public class EntityBaseDTO
    {
        public bool IsDeleted { get; set; }

        public bool Inaccesible { get; set; }

        public string TenantId { get; set; }

        public string CreatedById { get; set; }
        public string CreatedByUserName { get; set; }

        public string UpdatedById { get; set; }
        public string UpdatedByUserName { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
