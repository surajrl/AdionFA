using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Core
{
    [Table(nameof(CoreApp))]
    public class CoreApp : ReferenceDataBase
    {
        [Key]
        public string AppId { get; set; }
    }
}
