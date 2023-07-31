using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.Infrastructure.Model.MarketData;
using AdionFA.UI.Infrastructure.Validators;
using FluentValidation.Results;

namespace AdionFA.UI.Infrastructure.Model.Project
{
    public class ProjectScheduleConfigurationVM : EntityBaseVM, IModelValidator
    {
        public int ProjectScheduleConfigurationId { get; set; }

        public int ProjectConfigurationId { get; set; }
        public ProjectConfigurationVM ProjectConfiguration { get; set; }

        public int MarketRegionId { get; set; }
        public MarketRegionVM MarketRegion { get; set; }

        public int FromTimeInSeconds { get; set; }
        public int ToTimeInSeconds { get; set; }

        // Validation

        public ValidationResult GetValidationResult()
        {
            var v = new ProjectScheduleConfigurationVMValidator();
            return v.Validate(this);
        }

    }
}