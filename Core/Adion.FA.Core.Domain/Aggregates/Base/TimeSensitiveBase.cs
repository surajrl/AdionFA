using Adion.FA.Core.Domain.Contracts.Bases;
using System;

namespace Adion.FA.Core.Domain.Aggregates.Base
{
    public abstract class TimeSensitiveBase : EntityBase, ITimeSensitive
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }

        public ITimeSensitive GetClosedTamporalRecord(DateTime? inDate = null)
        {
            if (inDate == null)
                inDate = DateTime.UtcNow.AddDays(-1);

            EndDate = inDate;

            return this;
        }
    }
}
