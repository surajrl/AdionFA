using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Project.Model.Configuration;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Project.Services
{
    public interface IAppProjectService
    {
        // Project Configuration

        Task<ProjectConfigurationModel> GetProjectConfigurationAsync(int projectId, bool includeGraph = false);

        Task<ResponseVM> UpdateProjectConfigurationAsync(ProjectConfigurationModel projectConfiguration);
    }
}