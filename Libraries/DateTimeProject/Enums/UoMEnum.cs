using DateTimeProject.Attributes;
using DateTimeProject.Resources;

namespace DateTimeProject.Enums
{
    public enum UoMEnum
    {
        [I18NDescription("Year", typeof(EnumResources))]
        Year = 1,
        [I18NDescription("Month", typeof(EnumResources))]
        Month = 2,
        [I18NDescription("Day", typeof(EnumResources))]
        Day = 3,
        [I18NDescription("Week", typeof(EnumResources))]
        Week = 4,
        [I18NDescription("Hour", typeof(EnumResources))]
        Hour = 5,
        [I18NDescription("Minute", typeof(EnumResources))]
        Minute = 6,
        [I18NDescription("Second", typeof(EnumResources))]
        Second = 7
    }
}
