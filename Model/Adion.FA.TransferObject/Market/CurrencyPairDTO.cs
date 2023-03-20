using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.ReferenceData;

namespace Adion.FA.TransferObject.Market
{
    public class CurrencyPairDTO : ReferenceDataBaseDTO
    {
        public int CurrencyPairId { get; set; }

        public int CurrencyFromId { get; set; }
        public CurrencyDTO CurrencyFrom { get; set; }

        public int CurrencyToId { get; set; }
        public CurrencyDTO CurrencyTo { get; set; }

        public string CurrencyPairName => $"{CurrencyFrom?.Code ?? "N/A"}{CurrencyTo?.Code ?? "N/A"}. {CurrencyFrom?.Name ?? "N/A"} vs {CurrencyTo?.Name ?? "N/A"}"; 
    }
}
