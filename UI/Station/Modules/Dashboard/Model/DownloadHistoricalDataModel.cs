﻿using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Market;
using AdionFA.UI.Station.Module.Dashboard.Validators;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace AdionFA.UI.Station.Module.Dashboard.Model
{
    public class DownloadHistoricalDataModel : HistoricalDataVM, IModelValidator
    {
        public string FilePathHistoricalData { get; set; }

        public IList<HistoricalDataDetailSettingVM> HistoricalDataDetailSettings { get; set; }

        public ValidationResult GetValidationResult()
        {
            DownloadMarketDataVMValidator v = new();
            return v.Validate(this);
        }
    }
}