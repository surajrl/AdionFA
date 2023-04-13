using AdionFA.Core.Domain.Aggregates.Base;
using AdionFA.Core.Domain.Aggregates.Project;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Core.Domain.Aggregates.MetaTrader
{
    [Table(nameof(ExpertAdvisor))]
    public class ExpertAdvisor : EntityBase
    {
        [Key]
        public int ExpertAdvisorId { get; set; }

        public string Name { get; set; }
        public string MagicNumber { get; set; }
        public string Host { get; set; }
        public string ResponsePort { get; set; }
        public string PushPort { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project.Project Project { get; set; }
    }
}
