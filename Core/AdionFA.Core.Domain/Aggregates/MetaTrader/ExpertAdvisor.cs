using AdionFA.Core.Domain.Aggregates.Base;
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
        public ulong MagicNumber { get; set; }
        public string Host { get; set; }
        public ushort ResponsePort { get; set; }
        public ushort PushPort { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project.Project Project { get; set; }
    }
}
