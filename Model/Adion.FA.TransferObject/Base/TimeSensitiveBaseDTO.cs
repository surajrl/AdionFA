using System;

namespace Adion.FA.TransferObject.Base
{
    public abstract class TimeSensitiveBaseDTO : EntityBaseDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
    }
}
