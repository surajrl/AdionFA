using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.Project;
using AdionFA.UI.Module.Dashboard.Validators;
using FluentValidation.Results;

namespace AdionFA.UI.Module.Dashboard.Model
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
            CreateProjectVMValidator v = new();
            return v.Validate(this);
        }
    }
}
