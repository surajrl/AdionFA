using AdionFA.Application.Contracts;
using AdionFA.Domain.Entities;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.Persistence;
using AdionFA.Infrastructure.Services;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using Microsoft.EntityFrameworkCore;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdionFA.Application.Services.Projects
{
    public class ProjectService : AppServiceBase, IProjectService
    {
        private readonly IProjectDirectoryService _projectDirectoryService;

        public ProjectService()
        {
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
        }

        // Project

        public IList<ProjectDTO> GetAllProject(bool includeGraph)
        {
            using var dbContext = new AdionFADbContext();

            var allProject = new List<Project>();

            if (includeGraph)
            {
                allProject = dbContext.Set<Project>()
                    .Where(e => e.IsDeleted == false)
                    .Include(e => e.ProjectConfiguration)
                        .ThenInclude(e => e.ProjectScheduleConfigurations)
                            .ThenInclude(e => e.MarketRegion)
                    .Include(e => e.HistoricalData)
                        .ThenInclude(e => e.Market)
                    .Include(e => e.HistoricalData)
                        .ThenInclude(e => e.Symbol)
                    .Include(e => e.HistoricalData)
                        .ThenInclude(e => e.Timeframe)
                    .ToList();
            }
            else
            {
                allProject = dbContext.Set<Project>()
                    .Where(e => e.IsDeleted == false)
                    .ToList();
            }

            return Mapper.Map<IList<ProjectDTO>>(allProject);
        }

        public ProjectDTO GetProject(int projectId, bool includeGraph)
        {
            using var dbContext = new AdionFADbContext();

            Project project = new();

            if (includeGraph)
            {
                project = dbContext.Set<Project>()
                    .Where(e => e.ProjectId == projectId)
                    .Include(e => e.ProjectConfiguration)
                        .ThenInclude(e => e.ProjectScheduleConfigurations)
                            .ThenInclude(e => e.MarketRegion)
                    .Include(e => e.HistoricalData)
                        .ThenInclude(e => e.Market)
                    .Include(e => e.HistoricalData)
                        .ThenInclude(e => e.Symbol)
                    .Include(e => e.HistoricalData)
                        .ThenInclude(e => e.Timeframe)
                    .FirstOrDefault();
            }
            else
            {
                project = dbContext.Set<Project>()
                    .FirstOrDefault(e => e.ProjectId == projectId);
            }

            return Mapper.Map<ProjectDTO>(project);
        }

        public async Task<ResponseDTO> CreateProjectAsync(ProjectDTO projectDTO)
        {
            using var dbContext = new AdionFADbContext();

            var responseDTO = new ResponseDTO
            {
                IsSuccess = false
            };


            var project = Mapper.Map<Project>(projectDTO);

            // Assign the workspace path
            project.WorkspacePath = ProjectDirectoryManager.DefaultDirectory();

            // Create project configuration from the global configuration
            var globalConfiguration = dbContext.Set<GlobalConfiguration>()
                .Where(e => !e.IsDeleted)
                .Include(e => e.GlobalScheduleConfigurations)
                    .ThenInclude(e => e.MarketRegion)
                .FirstOrDefault();

            project.ProjectConfiguration = new ProjectConfiguration
            {
                // Period

                FromDateIS = globalConfiguration.FromDateIS,
                ToDateIS = globalConfiguration.ToDateIS,

                FromDateOS = globalConfiguration.FromDateOS,
                ToDateOS = globalConfiguration.ToDateOS,

                WithoutSchedule = globalConfiguration.WithoutSchedule,

                // Extractor

                ExtractorMinVariation = globalConfiguration.ExtractorMinVariation,

                // MetaTrader

                ExpertAdvisorHost = globalConfiguration.ExpertAdvisorHost,
                ExpertAdvisorPublisherPort = globalConfiguration.ExpertAdvisorPublisherPort,
                ExpertAdvisorResponsePort = globalConfiguration.ExpertAdvisorResponsePort,

                // Weka

                TotalInstanceWeka = globalConfiguration.TotalInstanceWeka,
                DepthWeka = globalConfiguration.DepthWeka,
                TotalDecimalWeka = globalConfiguration.TotalDecimalWeka,
                MinimalSeed = globalConfiguration.MinimalSeed,
                MaximumSeed = globalConfiguration.MaximumSeed,
                MaxRatioTree = globalConfiguration.MaxRatioTree,
                NTotalTree = globalConfiguration.NTotalTree,

                // Strategy Builder

                SBMinTotalTradesIS = globalConfiguration.SBMinTotalTradesIS,
                SBMinSuccessRatePercentIS = globalConfiguration.SBMinSuccessRatePercentIS,

                SBMinTotalTradesOS = globalConfiguration.SBMinTotalTradesOS,
                SBMinSuccessRatePercentOS = globalConfiguration.SBMinSuccessRatePercentOS,

                SBMaxSuccessRateVariation = globalConfiguration.SBMaxSuccessRateVariation,

                IsProgressiveness = globalConfiguration.IsProgressiveness,
                MaxProgressivenessVariation = globalConfiguration.MaxProgressivenessVariation,

                SBMaxCorrelationPercent = globalConfiguration.SBMaxCorrelationPercent,

                SBWinningStrategyDOWNTarget = globalConfiguration.SBWinningStrategyDOWNTarget,
                SBWinningStrategyUPTarget = globalConfiguration.SBWinningStrategyUPTarget,
                SBTotalTradesTarget = globalConfiguration.SBTotalTradesTarget,

                // Assembly Builder

                ABMinTotalTradesIS = globalConfiguration.ABMinTotalTradesIS,
                ABMinImprovePercent = globalConfiguration.ABMinImprovePercent,
                ABWekaMaxRatioTree = globalConfiguration.ABWekaMaxRatioTree,
                ABWekaNTotalTree = globalConfiguration.ABWekaNTotalTree,

                ProjectScheduleConfigurations = new List<ProjectScheduleConfiguration>()
            };

            foreach (var globalScheduleConfig in globalConfiguration.GlobalScheduleConfigurations)
            {
                project.ProjectConfiguration.ProjectScheduleConfigurations.Add(new ProjectScheduleConfiguration
                {
                    MarketRegionId = globalScheduleConfig.MarketRegionId,

                    FromTimeInSeconds = globalScheduleConfig.FromTimeInSeconds,
                    ToTimeInSeconds = globalScheduleConfig.ToTimeInSeconds
                });
            }

            dbContext.Add(project);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            responseDTO.IsSuccess = project.ProjectId > 0;

            if (!responseDTO.IsSuccess)
            {
                return responseDTO;
            }

            _projectDirectoryService.CreateDefaultProjectWorkspace(project.ProjectName);

            return responseDTO;
        }

        // Project Configuration

        public ProjectConfigurationDTO GetProjectConfiguration(int projectId, bool includeGraph)
        {
            using var dbContext = new AdionFADbContext();

            ProjectConfiguration projectConfiguration;

            if (includeGraph)
            {
                projectConfiguration = dbContext.Set<ProjectConfiguration>()
                    .Where(e => e.ProjectId == projectId)
                    .Include(e => e.ProjectScheduleConfigurations)
                        .ThenInclude(e => e.MarketRegion)
                    .Include(e => e.Project)
                        .ThenInclude(e => e.HistoricalData)
                            .ThenInclude(e => e.Market)
                    .Include(e => e.Project)
                        .ThenInclude(e => e.HistoricalData)
                            .ThenInclude(e => e.Symbol)
                    .Include(e => e.Project)
                        .ThenInclude(e => e.HistoricalData)
                            .ThenInclude(e => e.Timeframe)
                    .FirstOrDefault();
            }
            else
            {
                projectConfiguration = dbContext.Set<ProjectConfiguration>().FirstOrDefault(e => e.ProjectId == projectId);
            }

            return Mapper.Map<ProjectConfigurationDTO>(projectConfiguration);
        }

        public async Task<ResponseDTO> UpdateProjectConfigurationAsync(ProjectConfigurationDTO updatedProjectConfiguration)
        {
            using var dbContext = new AdionFADbContext();

            var response = new ResponseDTO
            {
                IsSuccess = false
            };

            dbContext.Set<ProjectConfiguration>().Update(Mapper.Map<ProjectConfiguration>(updatedProjectConfiguration));
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            response.IsSuccess = true;

            return response;
        }

        public async Task<ResponseDTO> RestoreProjectConfigurationAsync(int projectId)
        {
            using var dbContext = new AdionFADbContext();

            var response = new ResponseDTO
            {
                IsSuccess = false
            };

            var projectConfiguration = dbContext.Set<ProjectConfiguration>().FirstOrDefault(e => e.ProjectId == projectId);

            projectConfiguration.RestoreConfiguration();

            dbContext.Set<ProjectConfiguration>().Update(projectConfiguration);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            response.IsSuccess = true;

            return response;
        }
    }
}
