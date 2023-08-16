using AdionFA.TransferObject.Configuration;
using System.Collections.Generic;

namespace AdionFA.TransferObject.Project
{
    public class ProjectConfigurationDTO : Base.ConfigurationBaseDTO
    {
        public int ProjectConfigurationId { get; set; }

        // Project

        public int ProjectId { get; set; }
        public ProjectDTO Project { get; set; }

        // Builder configuration

        public int NodeBuilderConfigurationId { get; set; }
        public NodeBuilderConfigurationDTO NodeBuilderConfiguration { get; set; }

        public int AssemblyBuilderConfigurationId { get; set; }
        public AssemblyBuilderConfigurationDTO AssemblyBuilderConfiguration { get; set; }

        public int CrossingBuilderConfigurationId { get; set; }
        public CrossingBuilderConfigurationDTO CrossingBuilderConfiguration { get; set; }

        // Navigation

        public ICollection<ProjectScheduleConfigurationDTO> ProjectScheduleConfigurations { get; set; }
    }
}
