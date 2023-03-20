using Adion.FA.Core.Domain.Aggregates.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Market
{
    [Table(nameof(MarketData))]
    public class MarketData : TimeSensitiveBase
    {
        #region Properties

        [Key]
        public int MarketDataId { get; set; }

        public int MarketId { get; set; }
        [ForeignKey(nameof(MarketId))]
        public Market Market { get; set; }

        public int CurrencyPairId { get; set; }
        [ForeignKey(nameof(CurrencyPairId))]
        public CurrencyPair CurrencyPair { get; set; }

        public int CurrencyPeriodId { get; set; }
        [ForeignKey(nameof(CurrencyPeriodId))]
        public CurrencyPeriod CurrencyPeriod { get; set; }

        #endregion

        #region Nav

        public ICollection<MarketDataDetail> MarketDataDetails { get; set; }

        #endregion
    }
}
