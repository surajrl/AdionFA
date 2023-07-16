using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Infrastructure.IofC;
using AdionFA.UI.Infrastructure.Model.Project;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdionFA.UI.Module.Services
{
    public class ShellServiceShell : IShellServiceShell
    {
        private readonly IProjectAppService _projectService;

        public ShellServiceShell()
        {
            _projectService = IoC.Kernel.Get<IProjectAppService>();
        }

        public List<ProjectVM> GetAllProjects()
        {
            try
            {
                var projects = _projectService.GetAllProject();
                var result = (from p in projects
                              let config = p.ProjectConfigurations.FirstOrDefault(c => c.EndDate == null)
                              let workspace = config?.WorkspacePath != null
                                    ? config?.WorkspacePath + "\\" + ProjectDirectoryEnum.Projects.GetDescription() + "\\" + p.ProjectName : null
                              select new ProjectVM
                              {
                                  ProjectId = p.ProjectId,
                                  ProjectName = p.ProjectName,
                                  CreatedOn = p.CreatedOn ?? DateTime.MinValue,
                                  UpdatedOn = p.UpdatedOn ?? DateTime.MinValue
                              }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
