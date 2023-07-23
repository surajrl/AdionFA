using AdionFA.Domain.Entities.Base;
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

        public ICollection<GlobalScheduleConfiguration> GlobalScheduleConfigurations { get; set; }

    }
}