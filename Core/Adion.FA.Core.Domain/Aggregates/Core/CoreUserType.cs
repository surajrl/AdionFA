using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Core
{
    [Table(nameof(CoreUserType))]
    public class CoreUserType : ReferenceDataBase
    {
        [Key]
        public int UserTypeId { get; set; }
    }
}
