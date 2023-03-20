using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Adion.FA.UI.Station.Infrastructure.Model.ReferenceData;

namespace Adion.FA.UI.Station.Infrastructure.Model.Market
{
    public class CurrencyPairVM : ReferenceDataBaseVM
    {
        public int CurrencyPairId { get; set; }

        public int CurrencyFromId { get; set; }
        public CurrencyVM CurrencyFrom { get; set; }

        public int CurrencyToId { get; set; }
        public CurrencyVM CurrencyTo { get; set; }

        public string CurrencyPairName => $"{CurrencyFrom?.Code ?? "N/A"}{CurrencyTo?.Code ?? "N/A"}. {CurrencyFrom?.Name ?? "N/A"} vs {CurrencyTo?.Name ?? "N/A"}"; 
    }
}
