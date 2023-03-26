using AdionFA.Infrastructure.Common.Directories.Services;
using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Module.Shell.AutoMapper;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Module.Shell.Services
{
    public class ShellServiceShell : IShellServiceShell
    {
        #region Services

        public IProjectServiceAgent ProjectService;

        #endregion

        #region AutoMapper

        public readonly IMapper MapperShell;

        #endregion

        #region Ctor

        public ShellServiceShell(
            IProjectServiceAgent projectInfrastructureService)
        {
            ProjectService = projectInfrastructureService;

            MapperShell = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingShellProfile());
            }).CreateMapper();
        }

        #endregion

        #region Project

        public async Task<List<Model.ProjectVM>> GetAllProjects()
        {
            try
            {
                var projects = await ProjectService.GetAllProjects();
                var result = (from p in projects
                              let config = p.ProjectConfigurations.FirstOrDefault(c => c.EndDate == null)
                              let workspace = config?.WorkspacePath != null 
                                    ? config?.WorkspacePath + "\\" + ProjectDirectoryEnum.Projects.GetDescription() + "\\" + p.ProjectName : null
                              select new Model.ProjectVM
                              {
                                  ProjectId = p.ProjectId,
                                  Name = p.ProjectName,
                                  WorkspacePath = workspace ?? "...",
                                  WorkspacePathCut = (workspace?.Length ?? 0) > 53 ? workspace.Substring(0, 50) + "..." : workspace ?? "...",
                                  IsFavorite = config?.IsFavorite ?? false,
                                  CurrentProjectStepId = p.ProjectStepId,
                                  LastLoadOn = p.ProcessLastDate ?? DateTime.MinValue,
                                  ProcessId = p.ProcessId,
                                  CreateOn = p.CreatedOn ?? DateTime.MinValue,
                                  UpdateOn = p.UpdatedOn ?? DateTime.MinValue
                              }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion

        #region Shell module

        public async Task<bool> PinnedProject(int projectId, bool isPinned)
        {
            try
            {
                var result = await ProjectService.PinnedProject(projectId, isPinned);
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        #endregion
    }
}
