using AdionFA.Core.Domain.Aggregates.Project;
using System.Collections.Generic;

namespace AdionFA.Core.Domain.Contracts.Projects
{
    public interface IGlobalConfigurationDomainService
    {
        #region Project Global Configurations

        IList<ProjectGlobalConfiguration> GetAllProjectGlobalConfigurations(bool includeGraph = false);
        ProjectGlobalConfiguration GetProjectGlobalConfiguration(int? configurationId = null, bool includeGraph = false);
        int CreateProjectGlobalConfiguration(ProjectGlobalConfiguration configuration);
        void UpdateProjectGlobalConfiguration(ProjectGlobalConfiguration configuration);

        #endregion

        #region Schedule Configuration

        ProjectGlobalScheduleConfiguration GetProjectGlobalScheduleConfiguration(int marketRegionId);
        int CreateProjectGlobalScheduleConfiguration(ProjectGlobalScheduleConfiguration scheduleConfiguration);
        void UpdateProjectGlobalScheduleConfiguration(ProjectGlobalScheduleConfiguration scheduleConfiguration);

        #endregion
    }
}
