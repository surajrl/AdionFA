using System;

namespace AdionFA.Domain.Contracts.Bases
{
    public interface ITimeSensitive
    {
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; }
        string Description { get; set; }

        ITimeSensitive GetClosedTamporalRecord(DateTime? inDate = null);
    }
}
