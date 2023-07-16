using AdionFA.UI.Infrastructure.Model.Project;
using System.Collections.Generic;

namespace AdionFA.UI.Module.Services
{
    public interface IShellServiceShell
    {
        List<ProjectVM> GetAllProjects();
    }
}
