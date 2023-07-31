using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.MarketData;
using AdionFA.UI.Module.Dashboard.Validators;
using FluentValidation.Results;

namespace AdionFA.UI.Module.Dashboard.Model
{
    public class UploadHistoricalDataModel : HistoricalDataVM, IModelValidator
    {
        private string _filePathHistoricalData;
        public string FilePathHistoricalData
        {
            get => _filePathHistoricalData;
            set => SetProperty(ref _filePathHistoricalData, value);
        }

        // Validation

        public ValidationResult GetValidationResult()
        {
            UploadHistoricalDataModelValidator v = new();
            return v.Validate(this);
        }
    }
}