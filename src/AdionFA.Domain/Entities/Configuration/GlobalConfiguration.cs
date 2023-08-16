using AdionFA.Domain.Entities.Base;
using AdionFA.Domain.Entities.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(GlobalConfiguration))]
    public class GlobalConfiguration : ConfigurationBase
    {
        [Key]
        public int GlobalConfigurationId { get; set; }

        // Builder configuration

        public int NodeBuilderConfigurationId { get; set; }
        [ForeignKey(nameof(NodeBuilderConfigurationId))]
        public NodeBuilderConfiguration NodeBuilderConfiguration { get; set; }

        public int AssemblyBuilderConfigurationId { get; set; }
        [ForeignKey(nameof(AssemblyBuilderConfigurationId))]
        public AssemblyBuilderConfiguration AssemblyBuilderConfiguration { get; set; }

        public int CrossingBuilderConfigurationId { get; set; }
        [ForeignKey(nameof(CrossingBuilderConfigurationId))]
        public CrossingBuilderConfiguration CrossingBuilderConfiguration { get; set; }

        // Navigation

        public ICollection<GlobalScheduleConfiguration> GlobalScheduleConfigurations { get; set; }
    }
}