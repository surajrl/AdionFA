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
        public string Protocol { get; set; }
        public string HostName { get; set; }
        public int REPPort { get; set; }
        public int PUSHPort { get; set; }
        public int Timer { get; set; }
        public int MagicNumber { get; set; }
        public int MaximumOrders { get; set; }
        public double MaximumLotSize { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project.Project Project { get; set; }
    }
}