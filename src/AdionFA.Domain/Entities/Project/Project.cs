using AdionFA.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdionFA.Domain.Entities
{
    [Table(nameof(Project))]
    public class Project : EntityBase
    {
        [Key]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string WorkspacePath { get; set; }

        public int HistoricalDataId { get; set; }
        [ForeignKey(nameof(HistoricalDataId))]
        public HistoricalData HistoricalData { get; set; }

        public ProjectConfiguration ProjectConfiguration { get; set; }
    }
}
