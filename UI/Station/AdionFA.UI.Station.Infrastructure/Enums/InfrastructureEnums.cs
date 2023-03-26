using AdionFA.Infrastructure.Enums.Attributes;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.UI.Station.Infrastructure.Enums
{
    public enum HistoricalTimeGroupingEnum
    {
        [Metadata("Pinned", "Pinned", resourceType: typeof(EnumResources))]
        Pinned = 1,
        [Metadata("Today", "Today", resourceType:typeof(EnumResources))]
        Today = 2,
        [Metadata("LastWeek", "LastWeek", resourceType: typeof(EnumResources))]
        LastWeek = 3,
        [Metadata("LastMonth", "LastMonth", resourceType: typeof(EnumResources))]
        LastMonth = 4,
        [Metadata("Older", "Older", resourceType: typeof(EnumResources))]
        Older = 5
    }
}
