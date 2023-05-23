using AdionFA.Infrastructure.Enums;
using System;

namespace AdionFA.Infrastructure.Common.Managements
{
    public static class ProjectDirectoryManager
    {
        public static string DefaultWorkspace { get; set; }

        public static string DefaultDirectory() =>
            string.Format(@"{0}\{1}", !string.IsNullOrWhiteSpace(DefaultWorkspace)
                ? DefaultWorkspace
                : Environment.GetFolderPath((Environment.SpecialFolder)ProjectDirectoryEnum.DefaultWorkspace)
            , ProjectDirectoryEnum.DefaultWorkspace.GetDescription());

        public static string ProjectsDirectoryBase() => string.Format(@"{0}\{1}", DefaultDirectory(), ProjectDirectoryEnum.Projects.GetDescription());

        public static string ProjectDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(), projectNameFolder);
        }

        // Extractor

        public static string ProjectExtractorDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.Extractor.GetDescription(), projectNameFolder));
        }

        public static string ProjectExtractorEuropeDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.ExtractorMarket.GetDescription(), projectNameFolder, MarketRegionEnum.Europe.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectExtractorAmericaDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.ExtractorMarket.GetDescription(), projectNameFolder, MarketRegionEnum.America.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectExtractorAsiaDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.ExtractorMarket.GetDescription(), projectNameFolder, MarketRegionEnum.Asia.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectExtractorWithoutScheduleDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.ExtractorWithoutSchedule.GetDescription(), projectNameFolder))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectExtractorTemplatesDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.ExtractorTemplate.GetDescription(), projectNameFolder));
        }

        // Strategy Builder

        public static string ProjectStrategyBuilderDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.StrategyBuilder.GetDescription(), projectNameFolder));
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

        // Assembled Builder

        public static string ProjectAssembledBuilderDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilder.GetDescription(), projectNameFolder));
        }

        // Assembled Builder Extractor

        public static string ProjectAssembledBuilderExtractorUPDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorUP.GetDescription(), projectNameFolder));
        }

        public static string ProjectAssembledBuilderExtractorDOWNDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorDOWN.GetDescription(), projectNameFolder));
        }

        public static string ProjectAssembledBuilderExtractorWithoutScheduleDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorWithoutSchedule.GetDescription(), projectNameFolder,
                label.ToLower() == "up" ? "UP" : "DOWN"))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectAssembledBuilderExtractorEuropeDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorMarket.GetDescription(), projectNameFolder
                , label.ToLower() == "up" ? "UP" : "DOWN"
                , MarketRegionEnum.Europe.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectAssembledBuilderExtractorAmericaDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorMarket.GetDescription(), projectNameFolder
                , label.ToLower() == "up" ? "UP" : "DOWN"
                , MarketRegionEnum.America.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectAssembledBuilderExtractorAsiaDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorMarket.GetDescription(), projectNameFolder
                , label.ToLower() == "up" ? "UP" : "DOWN"
                , MarketRegionEnum.Asia.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        // Assembled Builder Nodes

        public static string ProjectAssembledBuilderNodesDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderNodes.GetDescription(), projectNameFolder));
        }

        public static string ProjectAssembledBuilderNodesUPDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderNodesUP.GetDescription(), projectNameFolder));
        }

        public static string ProjectAssembledBuilderNodesDOWNDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderNodesDOWN.GetDescription(), projectNameFolder));
        }
    }
}