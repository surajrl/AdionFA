using AdionFA.Application.Contracts;
using AdionFA.Domain.Entities;
using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Persistence;
using AdionFA.Infrastructure.Services;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;
using Microsoft.EntityFrameworkCore;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Application.Services.Projects
{
    public class ProjectService : AppServiceBase, IProjectService
    {
        private readonly ISettingService _settingService;
        private readonly IProjectDirectoryService _projectDirectoryService;

        public ProjectService()
        {
            _settingService = IoC.Kernel.Get<ISettingService>();
            _projectDirectoryService = IoC.Kernel.Get<IProjectDirectoryService>();
        }

        // Project

        public IList<ProjectDTO> GetAllProject(bool includeGraph)
        {
            Logger.Information("ProjectService.GetAllProject() :: Call.");

            using var dbContext = new AdionFADbContext();

            var allProject = new List<Project>();

            if (includeGraph)
            {
                allProject = dbContext.Set<Project>()
                    .Where(e => !e.IsDeleted)
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
                    .Where(e => !e.IsDeleted)
                    .ToList();
            }

            return Mapper.Map<IList<ProjectDTO>>(allProject);
        }

        public ProjectDTO GetProject(int projectId, bool includeGraph)
        {
            Logger.Information("ProjectService.GetProject() :: Call.");

            using var dbContext = new AdionFADbContext();

            Project project = new();

            if (includeGraph)
            {
                project = dbContext.Set<Project>()
                    .Where(e => e.ProjectId == projectId && !e.IsDeleted)
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
                    .FirstOrDefault(e => e.ProjectId == projectId && !e.IsDeleted);
            }

            return Mapper.Map<ProjectDTO>(project);
        }

        public ResponseDTO CreateProject(ProjectDTO projectDTO)
        {
            Logger.Information("ProjectService.CreateProject() :: Call.");

            using var dbContext = new AdionFADbContext();

            var responseDTO = new ResponseDTO
            {
                IsSuccess = false
            };

            var project = Mapper.Map<Project>(projectDTO);

            // Assign the workspace path
            project.WorkspacePath = _settingService.GetSetting((int)SettingEnum.DefaultWorkspace).Value;

            // Create project configuration from the global configuration
            var globalConfiguration = dbContext.Set<GlobalConfiguration>()
                .Where(e => !e.IsDeleted)
                .Include(e => e.GlobalScheduleConfigurations)
                    .ThenInclude(e => e.MarketRegion)
                .FirstOrDefault();

            project.ProjectConfiguration = new ProjectConfiguration
            {
                // ProjectId set automatically

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

                // Schedule Configuration

                ProjectScheduleConfigurations = new List<ProjectScheduleConfiguration>(),

                // Entity Base

                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            foreach (var globalScheduleConfig in globalConfiguration.GlobalScheduleConfigurations)
            {
                project.ProjectConfiguration.ProjectScheduleConfigurations.Add(new ProjectScheduleConfiguration
                {
                    // ProjectConfigurationId set automatically

                    MarketRegionId = globalScheduleConfig.MarketRegionId,

                    FromTimeInSeconds = globalScheduleConfig.FromTimeInSeconds,
                    ToTimeInSeconds = globalScheduleConfig.ToTimeInSeconds,

                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false
                });
            }

            // Add

            project.CreatedOn = DateTime.UtcNow;
            project.IsDeleted = false;

            dbContext.Set<Project>().Add(project);
            Logger.Information("ProjectService.CreateProject() :: dbContext.Set<Project>().Add().");

            dbContext.SaveChanges();
            Logger.Information("ProjectService.CreateProject() :: dbContext.SaveChanges().");

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
            Logger.Information("ProjectService.GetProjectConfiguration() :: Call.");

            using var dbContext = new AdionFADbContext();

            ProjectConfiguration projectConfiguration;

            if (includeGraph)
            {
                projectConfiguration = dbContext.Set<ProjectConfiguration>()
                    .Where(e => e.ProjectId == projectId && !e.IsDeleted)
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
                projectConfiguration = dbContext.Set<ProjectConfiguration>()
                    .FirstOrDefault(e => e.ProjectId == projectId && !e.IsDeleted);
            }

            return Mapper.Map<ProjectConfigurationDTO>(projectConfiguration);
        }

        public ResponseDTO UpdateProjectConfiguration(ProjectConfigurationDTO projectConfigurationDTO)
        {
            Logger.Information("ProjectService.UpdateProjectConfiguration() :: Call.");

            using var dbContext = new AdionFADbContext();

            var response = new ResponseDTO
            {
                IsSuccess = false
            };

            var projectConfiguration = Mapper.Map<ProjectConfiguration>(projectConfigurationDTO);

            // Update

            projectConfiguration.UpdatedOn = DateTime.UtcNow;
            dbContext.Set<ProjectConfiguration>().Update(projectConfiguration);
            Logger.Information("ProjectService.UpdateProjectConfiguration() :: dbContext.Set<ProjectConfiguration>().Update().");

            dbContext.SaveChanges();
            Logger.Information("ProjectService.UpdateProjectConfiguration() :: dbContext.SaveChanges().");

            response.IsSuccess = true;

            return response;
        }

        public ResponseDTO RestoreProjectConfiguration(int projectId)
        {
            Logger.Information("ProjectService.RestoreProjectConfiguration() :: Call.");

            using var dbContext = new AdionFADbContext();

            var response = new ResponseDTO
            {
                IsSuccess = false
            };

            var projectConfiguration = dbContext.Set<ProjectConfiguration>().FirstOrDefault(e => e.ProjectId == projectId);

            // Restore

            projectConfiguration.RestoreConfiguration();

            // Update

            projectConfiguration.UpdatedOn = DateTime.UtcNow;
            dbContext.Set<ProjectConfiguration>().Update(projectConfiguration);
            Logger.Information("ProjectService.RestoreProjectConfiguration() :: dbContext.Set<ProjectConfiguration>().Update().");

            dbContext.SaveChanges();
            Logger.Information("ProjectService.RestoreProjectConfiguration() :: dbContext.SaveChanges().");

            response.IsSuccess = true;

            return response;
        }
    }
}
