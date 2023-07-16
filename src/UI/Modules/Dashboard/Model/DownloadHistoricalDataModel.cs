using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.Infrastructure.Model.MarketData;
using AdionFA.UI.Module.Dashboard.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace AdionFA.UI.Module.Dashboard.Model
{
    public class DownloadHistoricalDataModel : HistoricalDataVM, IModelValidator
    {
        public string FilepathHistoricalData { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public IList<HistoricalDataCandleSettingVM> HistoricalDataCandleSettings { get; set; }

        public ValidationResult GetValidationResult()
        {
            DownloadHistoricalDataVMValidator v = new();
            return v.Validate(this);
        }
    }
}