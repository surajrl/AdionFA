using AdionFA.Domain.Attributes;

namespace AdionFA.Domain.Enums
{
    public enum CurrencyPairEnum
    {
        [Metadata("EURUSD", "EURUSD")]
        EURUSD = 1,
    }

    public enum TimeframeEnum
    {
        [Metadata("M1", "M1", "1")]
        M1 = 1,

        [Metadata("M5", "M5", "5")]
        M5 = 2,

        [Metadata("M15", "M15", "15")]
        M15 = 3,

        [Metadata("M30", "M30", "30")]
        M30 = 4,

        [Metadata("H1", "H1", "16385")]
        H1 = 5,

        [Metadata("H4", "H4", "16388")]
        H4 = 6,

        [Metadata("D1", "D1", "16408")]
        D1 = 7,

        [Metadata("W1", "W1", "32769")]
        W1 = 8,

        [Metadata("MN1", "MN1", "49153")]
        MN1 = 9
    }
}
