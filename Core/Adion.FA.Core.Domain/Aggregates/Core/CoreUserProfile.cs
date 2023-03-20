using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Aggregates.ReferenceData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Core
{
    [Table(nameof(CoreUserProfile))]
    public class CoreUserProfile : TimeSensitiveBase
    {
        [Key]
        public int UserProfileId { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public CoreUser User { get; set; }

        public int AppId { get; set; }
        [ForeignKey(nameof(AppId))]
        public CoreApp App { get; set; }

        public int LanguageId { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }

        public int TimeZoneId { get; set; }
        [ForeignKey(nameof(TimeZoneId))]
        public TimeZone TimeZone { get; set; }
    }
}
