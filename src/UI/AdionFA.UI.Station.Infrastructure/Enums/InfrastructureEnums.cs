using AdionFA.Domain.Attributes;

namespace AdionFA.UI.Station.Infrastructure.Enums
{
    public enum HistoricalTimeGroupingEnum
    {
        [Metadata("Pinned", "Pinned")]
        Pinned = 1,

        [Metadata("Today", "Today")]
        Today = 2,

        [Metadata("LastWeek", "LastWeek")]
        LastWeek = 3,

        [Metadata("LastMonth", "LastMonth")]
        LastMonth = 4,

        [Metadata("Older", "Older")]
        Older = 5
    }
}
