using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Module.Dashboard.Validators;
using FluentValidation.Results;

namespace AdionFA.UI.Station.Module.Dashboard.Model
{
    public class CreateProjectModel : ProjectVM, IModelValidator
    {
        private int? _configurationId;

        public int? ConfigurationId
        {
            get => _configurationId;
            set => SetProperty(ref _configurationId, value);
        }

        private int? _historicalDataId;

        public int? HistoricalDataId
        {
            get => _historicalDataId;
            set => SetProperty(ref _historicalDataId, value);
        }

        // Validation

        public ValidationResult GetValidationResult()
        {
            CrateProjectVMValidator v = new();
            return v.Validate(this);
        }
    }
}