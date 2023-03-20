using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Common
{
    [Table(nameof(File))]
    public class File : EntityBase
    {
        [Key]
        public int FileId { get; set; }

        [Required]
        public string FileName { get; set; }
        public string FileNameOnStorage { get; set; }
    }
}
