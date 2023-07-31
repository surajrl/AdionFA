using AdionFA.Domain.Attributes;

namespace AdionFA.Domain.Enums
{
    public enum OrderTypeEnum
    {
        [Metadata("BUY", "BUY", "ORDER_TYPE_BUY")]
        Buy = 0,

        [Metadata("SELL", "SELL", "ORDER_TYPE_SELL")]
        Sell = 1,

        [Metadata("CLOSE", "CLOSE", "ORDER_TYPE_CLOSE_BY")]
        Close = 8,

        [Metadata("NONE", "NONE", "ORDER_TYPE_NONE")]
        None = 9


    }
}
