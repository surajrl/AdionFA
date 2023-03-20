using Adion.FA.Core.Domain.Aggregates.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Base
{
    public abstract class EntityStatusBase : TimeSensitiveBase
    {
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }

        public string Comments { get; set; }
    }
}
