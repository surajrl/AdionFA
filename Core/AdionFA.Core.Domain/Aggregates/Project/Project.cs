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
        #region Properties

        [Key]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int ProjectStepId { get; set; }
        [ForeignKey(nameof(ProjectStepId))]
        public ProjectStep ProjectStep { get; set; }

        #endregion

        #region Navegation Properties

        public ICollection<ProjectConfiguration> ProjectConfigurations { get; set; }
        public ICollection<ExpertAdvisor> ExpertAdvisors { get; set; }

        #endregion

        #region Not Mapped

        [NotMapped]
        public DateTime? ProcessLastDate { get; set; }

        [NotMapped]
        public long ProcessId { get; set; }

        #endregion
    }
}
