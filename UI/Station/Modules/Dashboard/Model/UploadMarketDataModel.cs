using Adion.FA.UI.Station.Infrastructure.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Market;
using Adion.FA.UI.Station.Module.Dashboard.Validators;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Adion.FA.UI.Station.Module.Dashboard.Model
{
    public class UploadMarketDataModel : MarketDataVM, IModelValidator
    {

        string pathFileMarketData;
        public string PathFileMarketData
        {
            get { return pathFileMarketData; }
            set { SetProperty(ref pathFileMarketData, value); }
        }

        public IList<MarketDataDetailSettingVM> MarketDataDetailSettings { get; set; }

        #region Validation

        public ValidationResult GetValidationResult()
        {
            UploadMarketDataVMValidator v = new UploadMarketDataVMValidator();
            return v.Validate(this);
        }

        #endregion
    }
}
