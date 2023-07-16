using AdionFA.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(Configuration))]
    public class Configuration : ConfigurationBase
    {
        [Key]
        public int ConfigurationId { get; set; }

        public ICollection<ScheduleConfiguration> ScheduleConfigurations { get; set; }

    }
}