using AdionFA.UI.Station.Infrastructure.Model.Base;
using System.Collections.Generic;

namespace AdionFA.UI.Station.Infrastructure.Model.Market
{
    public class HistoricalDataVM : TimeSensitiveBaseVM
    {
        public int HistoricalDataId { get; set; }

        // Market

        private int _marketId;

        public int MarketId
        {
            get => _marketId;
            set => SetProperty(ref _marketId, value);
        }

        public MarketVM Market { get; set; }

        // Symbol

        private int _symbolId;

        public int SymbolId
        {
            get => _symbolId;
            set => SetProperty(ref _symbolId, value);
        }

        public SymbolVM Symbol { get; set; }

        // Timeframe

        private int _timeframeId;

        public int TimeframeId
        {
            get => _timeframeId;
            set => SetProperty(ref _timeframeId, value);
        }

        public TimeframeVM Timeframe { get; set; }

        // Details

        public IList<HistoricalDataDetailVM> HistoricalDataDetails { get; set; }
    }
}