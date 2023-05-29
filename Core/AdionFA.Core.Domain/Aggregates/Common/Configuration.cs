using AdionFA.Core.Domain.Aggregates.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.Common
{
    [Table(nameof(Configuration))]
    public class Configuration : ConfigurationBase
    {
        [Key]
        public int ConfigurationId { get; set; }

        public ICollection<ScheduleConfiguration> ScheduleConfigurations { get; set; }

    }
}