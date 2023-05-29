using AdionFA.Core.Domain.Aggregates.Common;
using System.Collections.Generic;

namespace AdionFA.Core.Domain.Contracts.Commons
{
    public interface IConfigurationDomainService
    {
        // Configuration

        IList<Configuration> GetAllConfiguration(bool includeGraph = false);
        Configuration GetConfiguration(int? configurationId = null, bool includeGraph = false);
        int CreateConfiguration(Configuration configuration);
        void UpdateConfiguration(Configuration configuration);

        // Schedule Configuration

        ScheduleConfiguration GetScheduleConfiguration(int marketRegionId);
        int CreateScheduleConfiguration(ScheduleConfiguration scheduleConfiguration);
        void UpdateScheduleConfiguration(ScheduleConfiguration scheduleConfiguration);

    }
}
