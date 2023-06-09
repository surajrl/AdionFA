﻿using AdionFA.UI.Station.Module.Shell.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Module.Shell.Services
{
    public interface IShellServiceShell
    {
        Task<List<ProjectVM>> GetAllProjects();

        Task<bool> PinnedProject(int projectId, bool isPinned);
    }
}
