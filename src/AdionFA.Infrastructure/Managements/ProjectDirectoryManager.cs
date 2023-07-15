using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using System;

namespace AdionFA.Infrastructure.Managements
{
    public static class ProjectDirectoryManager
    {
        public static string DefaultWorkspace { get; set; }

        public static string DefaultDirectory() =>
            string.Format(@"{0}\{1}", !string.IsNullOrWhiteSpace(DefaultWorkspace)
                ? DefaultWorkspace
                : Environment.GetFolderPath((Environment.SpecialFolder)ProjectDirectoryEnum.DefaultWorkspace), ProjectDirectoryEnum.DefaultWorkspace.GetDescription());

        public static string ProjectsDirectoryBase() => string.Format(@"{0}\{1}", DefaultDirectory(), ProjectDirectoryEnum.Projects.GetDescription());

        public static string ProjectDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(), projectNameFolder);
        }

        // Extractor

        public static string ProjectExtractorTemplatesDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.ExtractorTemplate.GetDescription(), projectNameFolder));
        }

        // Strategy Builder

        // Strategy Builder Extractor

        public static string ProjectStrategyBuilderExtractorWithoutScheduleDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.StrategyBuilderExtractorWithoutSchedule.GetDescription(), projectNameFolder))
                + (!string.IsNullOrWhiteSpace(withFileName)
                ? @$"\{withFileName}"
                : string.Empty);
        }

        public static string ProjectStrategyBuilderExtractorEuropeDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.StrategyBuilderExtractorMarket.GetDescription(), projectNameFolder, MarketRegionEnum.Europe.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName)
                ? @$"\{withFileName}"
                : string.Empty);
        }

        public static string ProjectStrategyBuilderExtractorAmericaDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.StrategyBuilderExtractorMarket.GetDescription(), projectNameFolder, MarketRegionEnum.America.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName)
                ? @$"\{withFileName}"
                : string.Empty);
        }

        public static string ProjectStrategyBuilderExtractorAsiaDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.StrategyBuilderExtractorMarket.GetDescription(), projectNameFolder, MarketRegionEnum.Asia.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName)
                ? @$"\{withFileName}"
                : string.Empty);
        }

        // Strategy Builder Nodes

        public static string ProjectStrategyBuilderNodesDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.StrategyBuilderNodes.GetDescription(), projectNameFolder));
        }

        public static string ProjectStrategyBuilderNodesUPDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}",
                ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.StrategyBuilderNodesUP.GetDescription(), projectNameFolder));
        }

        public static string ProjectStrategyBuilderNodesDOWNDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}",
                ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.StrategyBuilderNodesDOWN.GetDescription(), projectNameFolder));
        }

        // Assembly Builder

        // Assembly Builder Extractor

        public static string ProjectAssemblyBuilderExtractorWithoutScheduleDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorWithoutSchedule.GetDescription(), projectNameFolder,
                label.ToLowerInvariant() == "up" ? "UP" : "DOWN"))
                + (!string.IsNullOrWhiteSpace(withFileName)
                ? @$"\{withFileName}"
                : string.Empty);
        }

        public static string ProjectAssemblyBuilderExtractorEuropeDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                label.ToLowerInvariant() == "up" ? "UP" : "DOWN",
                MarketRegionEnum.Europe.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectAssemblyBuilderExtractorAmericaDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                label.ToLowerInvariant() == "up" ? "UP" : "DOWN",
                MarketRegionEnum.America.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectAssemblyBuilderExtractorAsiaDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectNameFolder
                , label.ToLowerInvariant() == "up" ? "UP" : "DOWN"
                , MarketRegionEnum.Asia.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName)
                ? @$"\{withFileName}"
                : string.Empty);
        }

        // Assembly Builder Nodes

        public static string ProjectAssemblyBuilderNodesUPDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssemblyBuilderNodesUP.GetDescription(), projectNameFolder));
        }

        public static string ProjectAssemblyBuilderNodesDOWNDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssemblyBuilderNodesDOWN.GetDescription(), projectNameFolder));
        }

        // Crossing Builder

        // Crossing Builder Extractor

        public static string ProjectCrossingBuilderExtractorWithoutScheduleDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorWithoutSchedule.GetDescription(), projectNameFolder,
                label.ToLowerInvariant() == "up" ? "UP" : "DOWN"))
                + (!string.IsNullOrWhiteSpace(withFileName)
                ? @$"\{withFileName}"
                : string.Empty);
        }

        public static string ProjectCrossingBuilderExtractorEuropeDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                label.ToLowerInvariant() == "up" ? "UP" : "DOWN",
                MarketRegionEnum.Europe.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectCrossingBuilderExtractorAmericaDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                label.ToLowerInvariant() == "up" ? "UP" : "DOWN",
                MarketRegionEnum.America.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectCrossingBuilderExtractorAsiaDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectNameFolder
                , label.ToLowerInvariant() == "up" ? "UP" : "DOWN"
                , MarketRegionEnum.Asia.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName)
                ? @$"\{withFileName}"
                : string.Empty);
        }

        // Crossing Builder Nodes

        public static string ProjectCrossingBuilderNodesDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}",
                ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.CrossingBuilderNodes.GetDescription(), projectNameFolder));
        }

        public static string ProjectCrossingBuilderNodesUPDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}",
                ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.CrossingBuilderNodesUP.GetDescription(), projectNameFolder));
        }

        public static string ProjectCrossingBuilderNodesDOWNDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}",
                ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.CrossingBuilderNodesDOWN.GetDescription(), projectNameFolder));
        }
    }
}