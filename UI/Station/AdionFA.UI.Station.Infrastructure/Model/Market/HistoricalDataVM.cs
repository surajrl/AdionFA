using AdionFA.UI.Station.Infrastructure.Model.Base;
using System.Collections.Generic;

namespace AdionFA.UI.Station.Infrastructure.Model.Market
{
    public class HistoricalDataVM : TimeSensitiveBaseVM
    {
        public int HistoricalDataId { get; set; }

        // Market
        public MarketVM Market { get; set; }

        private int _marketId;
        public int MarketId
        {
            get => _marketId;
            set => SetProperty(ref _marketId, value);
        }

        // Symbol
        public SymbolVM Symbol { get; set; }

        private int _symbolId;
        public int SymbolId
        {
            get => _symbolId;
            set => SetProperty(ref _symbolId, value);
        }

        // Timeframe
        public TimeframeVM Timeframe { get; set; }

        private int _timeframeId;
        public int TimeframeId
        {
            get => _timeframeId;
            set => SetProperty(ref _timeframeId, value);
        }

        // Details

        public IList<HistoricalDataDetailVM> HistoricalDataDetails { get; set; }
    }
}
