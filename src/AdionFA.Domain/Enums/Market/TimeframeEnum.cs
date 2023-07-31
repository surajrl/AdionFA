using AdionFA.Domain.Attributes;

namespace AdionFA.Domain.Enums.Market
{
    public enum TimeframeEnum
    {
        [Metadata("M1", "1 Minute", "1")]
        M1 = 1,

        [Metadata("M5", "5 Minutes", "5")]
        M5 = 2,

        [Metadata("M15", "15 Minutes", "15")]
        M15 = 3,

        [Metadata("M30", "30 Minutes", "30")]
        M30 = 4,

        [Metadata("H1", "1 Hour", "16385")]
        H1 = 5,

        [Metadata("H4", "4 Hours", "16388")]
        H4 = 6,

        [Metadata("D1", "Daily", "16408")]
        D1 = 7,

        [Metadata("W1", "Weekly", "32769")]
        W1 = 8,

        [Metadata("MN1", "Monthly", "49153")]
        MN1 = 9
    }
}
