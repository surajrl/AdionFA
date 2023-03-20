using Adion.FA.Core.Domain.Aggregates.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Core
{
    [Table(nameof(CoreFeatureCategory))]
    public class CoreFeatureCategory : EntityBase
    {
        public int CoreFeatureCategoryId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int ParentCoreFeatureCategoryId { get; set; }
        [ForeignKey(nameof(CoreFeatureCategory))]
        [InverseProperty(nameof(ParentCoreFeatureCategories))]
        public CoreFeatureCategory ParentCoreFeatureCategory { get; set; }


        #region Nav

        public ICollection<CoreFeatureCategory> ParentCoreFeatureCategories { get; set; }

        #endregion
    }
}
