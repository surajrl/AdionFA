using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using System;

namespace AdionFA.Infrastructure.Managements
{
    public static class ProjectDirectoryManager
    {
        public static string DefaultWorkspace { get; set; }

        public static string DefaultDirectory() =>
            string.Format(@"{0}\{1}",
                !string.IsNullOrWhiteSpace(DefaultWorkspace)
                ? DefaultWorkspace
                : Environment.GetFolderPath((Environment.SpecialFolder)ProjectDirectoryEnum.DefaultWorkspace), ProjectDirectoryEnum.DefaultWorkspace.GetDescription());

        public static string ProjectsDirectoryBase() => string.Format(@"{0}\{1}",
            DefaultDirectory(),
            ProjectDirectoryEnum.Projects.GetDescription());

        // Extractor

        public static string ProjectExtractorTemplatesDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}",
                ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.ExtractorTemplate.GetDescription(), projectNameFolder));
        }

        // Nodes

        public static string ProjectNodesUPDirectory(this string projectNameDirectory, string nodeDirectory)
        {
            return string.Format(@"{0}\{1}",
                ProjectsDirectoryBase(),
                string.Format(nodeDirectory, projectNameDirectory));
        }

        public static string ProjectNodesDOWNDirectory(this string projectNameDirectory, string nodeDirectory)
        {
            return string.Format(@"{0}\{1}",
                ProjectsDirectoryBase(),
                string.Format(nodeDirectory, projectNameDirectory));
        }

        // Node Builder

        public static string ProjectNodeBuilderExtractorWithoutScheduleDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}",
                ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.NodeBuilderExtractorWithoutSchedule.GetDescription(),
                projectNameFolder))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectNodeBuilderExtractorEuropeDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}",
                ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.NodeBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                MarketRegionEnum.Europe.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectNodeBuilderExtractorAmericaDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}",
                ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.NodeBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                MarketRegionEnum.America.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectNodeBuilderExtractorAsiaDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}",
                ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.NodeBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                MarketRegionEnum.Asia.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        // Assembly Builder

        public static string ProjectAssemblyBuilderExtractorWithoutScheduleDirectory(this string projectNameFolder, Label label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorWithoutSchedule.GetDescription(),
                projectNameFolder,
                label.GetDescription()))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectAssemblyBuilderExtractorEuropeDirectory(this string projectNameFolder, Label label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                label.GetDescription(),
                MarketRegionEnum.Europe.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectAssemblyBuilderExtractorAmericaDirectory(this string projectNameFolder, Label label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                label.GetDescription(),
                MarketRegionEnum.America.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectAssemblyBuilderExtractorAsiaDirectory(this string projectNameFolder, Label label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                label.GetDescription(),
                MarketRegionEnum.Asia.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        // Crossing Builder

        public static string ProjectCrossingBuilderExtractorWithoutScheduleDirectory(this string projectNameFolder, Label label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorWithoutSchedule.GetDescription(),
                projectNameFolder,
                label.GetDescription()))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectCrossingBuilderExtractorEuropeDirectory(this string projectNameFolder, Label label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                label.GetDescription(),
                MarketRegionEnum.Europe.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectCrossingBuilderExtractorAmericaDirectory(this string projectNameFolder, Label label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                label.GetDescription(),
                MarketRegionEnum.America.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectCrossingBuilderExtractorAsiaDirectory(this string projectNameFolder, Label label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(),
                projectNameFolder,
                label.GetDescription(),
                MarketRegionEnum.Asia.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }
    }
}