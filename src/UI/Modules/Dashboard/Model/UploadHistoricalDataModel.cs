using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.MarketData;
using AdionFA.UI.Module.Dashboard.Validators;
using FluentValidation.Results;
using System.Collections.Generic;

namespace AdionFA.UI.Module.Dashboard.Model
{
    public class UploadHistoricalDataModel : HistoricalDataVM, IModelValidator
    {
        private string _filepathHistoricalData;
        public string FilepathHistoricalData
        {
            get => _filepathHistoricalData;
            set => SetProperty(ref _filepathHistoricalData, value);
        }

        public IList<HistoricalDataCandleSettingVM> HistoricalDataCandleSettings { get; set; }

        // Validation

        public ValidationResult GetValidationResult()
        {
            UploadHistoricalDataVMValidator v = new();
            return v.Validate(this);
        }
    }
}