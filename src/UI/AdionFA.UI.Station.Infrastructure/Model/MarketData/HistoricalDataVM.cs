using AdionFA.UI.Station.Infrastructure.Model.Base;
using System.Collections.Generic;

namespace AdionFA.UI.Station.Infrastructure.Model.MarketData
{
    public class HistoricalDataVM : TimeSensitiveBaseVM
    {
        public int HistoricalDataId { get; set; }

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