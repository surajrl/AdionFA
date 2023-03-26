using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Project;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Extensions
{
    public static class EntityExtension
    {
        #region ProjectScheduleConfiguration

        public static void MarketStartTime(this List<ProjectScheduleConfigurationDTO> schedules, MarketRegionEnum region
            , out DateTime starttime, out DateTime endtime)
        {
            var schedule = schedules.FirstOrDefault(gc => gc.MarketRegionId == (int)region);

            starttime = DateTime.MinValue.AddSeconds(schedule.FromTimeInSeconds ?? 0);
            endtime = DateTime.MinValue.AddSeconds(schedule.ToTimeInSeconds ?? 0);
        }

        public static void MarketStartTime(this ProjectScheduleConfigurationDTO projectSchedule, out DateTime starttime, out DateTime endtime)
        {
            starttime = DateTime.MinValue.AddSeconds(projectSchedule.FromTimeInSeconds ?? 0);
            endtime = DateTime.MinValue.AddSeconds(projectSchedule.ToTimeInSeconds ?? 0);
        }

        #endregion

    }
}
