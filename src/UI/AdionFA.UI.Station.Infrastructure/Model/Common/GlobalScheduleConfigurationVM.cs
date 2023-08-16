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

        private int _fromTimeInSeconds;
        public int FromTimeInSeconds
        {
            get => _fromTimeInSeconds;
            set => SetProperty(ref _fromTimeInSeconds, value);
        }

        private int _toTimeInSeconds;
        public int ToTimeInSeconds
        {
            get => _toTimeInSeconds;
            set => SetProperty(ref _toTimeInSeconds, value);
        }

        // Validation

        public ValidationResult GetValidationResult()
        {
            var v = new GlobalScheduleConfigurationVMValidator();
            return v.Validate(this);
        }
    }
}