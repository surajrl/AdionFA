using System;

namespace BestDoctors.DirectInsurance.Infrastructure.Contracts
{
    /// <summary>
    /// Interface to check overlapping and others things related to dates
    /// </summary>
    public interface ITimeSensitive
    {
        DateTime CreatedDate { get; set; }

        DateTime? StartDate { get; set; }

        DateTime? EndDate { get; set; }
    }
}
