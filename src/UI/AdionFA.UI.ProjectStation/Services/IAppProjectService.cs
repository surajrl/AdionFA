using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.ProjectStation.Model.Configuration;

namespace AdionFA.UI.ProjectStation.Services
{
    public interface IAppProjectService
    {
        // Project Configuration

        ProjectConfigurationModel GetProjectConfiguration(int projectId, bool includeGraph = false);

        ResponseVM UpdateProjectConfiguration(ProjectConfigurationModel projectConfiguration);
    }
}