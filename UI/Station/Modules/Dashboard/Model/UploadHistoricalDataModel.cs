using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Market;
using AdionFA.UI.Station.Module.Dashboard.Validators;
using FluentValidation.Results;
using System.Collections.Generic;

namespace AdionFA.UI.Station.Module.Dashboard.Model
{
    public class UploadHistoricalDataModel : HistoricalDataVM, IModelValidator
    {
        private string _filePathHistoricalData;

        public string FilePathHistoricalData
        {
            get => _filePathHistoricalData;
            set => SetProperty(ref _filePathHistoricalData, value);
        }

        public IList<HistoricalDataDetailSettingVM> HistoricalDataDetailSettings { get; set; }

        #region Validation

        public ValidationResult GetValidationResult()
        {
            UploadHistoricalDataVMValidator v = new();
            return v.Validate(this);
        }

        #endregion Validation
    }
}
