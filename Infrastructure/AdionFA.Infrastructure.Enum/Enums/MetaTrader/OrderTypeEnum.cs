using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum OrderTypeEnum
    {
        [Metadata("BUY", "BUY", "ORDER_TYPE_BUY", "Market buy order")]
        Buy = 0,

        [Metadata("SELL", "SELL", "ORDER_TYPE_SELL", "Market sell order")]
        Sell = 1,

        [Metadata("CLOSE", "CLOSE", "ORDER_TYPE_CLOSE_BY", "Order to close a position by an opposite one")]
        Close = 8,

        [Metadata("NONE", "NONE", "ORDER_TYPE_NONE", "No operation")]
        None = 9


    }
}
