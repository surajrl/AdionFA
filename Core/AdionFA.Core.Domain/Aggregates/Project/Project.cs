using AdionFA.Core.Domain.Aggregates.Base;
using AdionFA.Core.Domain.Aggregates.MetaTrader;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.Project
{
    [Table(nameof(Project))]
    public class Project : EntityBase
    {
        [Key]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        // Navigation

        public ICollection<ProjectConfiguration> ProjectConfigurations { get; set; }
        public ICollection<ExpertAdvisor> ExpertAdvisors { get; set; }

        [NotMapped]
        public DateTime? ProcessLastDate { get; set; }

        [NotMapped]
        public long ProcessId { get; set; }
    }
}
