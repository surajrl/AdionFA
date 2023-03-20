using Adion.FA.TransferObject.Base;
using System.Collections.Generic;

namespace Adion.FA.TransferObject.Market
{
    public class MarketDataDTO : TimeSensitiveBaseDTO
    {
        public int MarketDataId { get; set; }

        public int MarketId { get; set; }
        public MarketDTO Market { get; set; }

        public int CurrencyPairId { get; set; }
        public CurrencyPairDTO CurrencyPair { get; set; }

        public int CurrencyPeriodId { get; set; }
        public CurrencyPeriodDTO CurrencyPeriod { get; set; }

        public IList<MarketDataDetailDTO> MarketDataDetails { get; set; }
    }
}
