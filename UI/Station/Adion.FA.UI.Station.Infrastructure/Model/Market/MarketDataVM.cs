using Adion.FA.UI.Station.Infrastructure.Model.Base;
using System.Collections.Generic;

namespace Adion.FA.UI.Station.Infrastructure.Model.Market
{
    public class MarketDataVM : TimeSensitiveBaseVM
    {
        public int MarketDataId { get; set; }

        #region Market

        int marketId;
        public int MarketId
        { 
            get => marketId; 
            set => SetProperty(ref marketId, value);
        }
        public MarketVM Market { get; set; }

        #endregion

        #region CurrencyPairId 

        int currencyPairId;
        public int CurrencyPairId 
        {
            get => currencyPairId;
            set => SetProperty(ref currencyPairId, value);
        }
        public CurrencyPairVM CurrencyPair { get; set; }

        #endregion

        #region CurrencyPeriod

        int currencyPeriodId;
        public int CurrencyPeriodId 
        {
            get => currencyPeriodId;
            set => SetProperty(ref currencyPeriodId, value);
        }
        public CurrencyPeriodVM CurrencyPeriod { get; set; }

        #endregion

        public IList<MarketDataDetailVM> MarketDataDetails { get; set; }
    }
}
