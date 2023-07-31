using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.Base;
using AdionFA.UI.Infrastructure.Model.MarketData;
using AdionFA.UI.Infrastructure.Validators;
using FluentValidation.Results;

namespace AdionFA.UI.Infrastructure.Model.Common
{
    public class GlobalScheduleConfigurationVM : EntityBaseVM, IModelValidator
    {
        public int GlobalScheduleConfigurationId { get; set; }

        public int GlobalConfigurationId { get; set; }
        public GlobalConfigurationVM GlobalConfiguration { get; set; }

        public int MarketRegionId { get; set; }
        public MarketRegionVM MarketRegion { get; set; }

        public int FromTimeInSeconds { get; set; }
        public int ToTimeInSeconds { get; set; }

        // Validation

        public ValidationResult GetValidationResult()
        {
            var v = new GlobalScheduleConfigurationVMValidator();
            return v.Validate(this);
        }
    }
}