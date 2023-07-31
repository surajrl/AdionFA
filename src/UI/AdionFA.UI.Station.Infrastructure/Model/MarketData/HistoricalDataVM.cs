using AdionFA.UI.Infrastructure.Model.Base;
using System.Collections.Generic;

namespace AdionFA.UI.Infrastructure.Model.MarketData
{
    public class HistoricalDataVM : EntityBaseVM
    {
        public int HistoricalDataId { get; set; }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public MarketVM Market { get; set; }
        private int _marketId;

        public int MarketId
        {
            get => _marketId;
            set => SetProperty(ref _marketId, value);
        }

        public SymbolVM Symbol { get; set; }
        private int _symbolId;

        public int SymbolId
        {
            get => _symbolId;
            set => SetProperty(ref _symbolId, value);
        }

        public TimeframeVM Timeframe { get; set; }
        private int _timeframeId;

        public int TimeframeId
        {
            get => _timeframeId;
            set => SetProperty(ref _timeframeId, value);
        }

        public IList<HistoricalDataCandleVM> HistoricalDataCandles { get; set; }
    }
}