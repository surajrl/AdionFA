using System;

namespace AdionFA.TransferObject.Base
{
    public abstract class EntityBaseDTO
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; }

    }
}
