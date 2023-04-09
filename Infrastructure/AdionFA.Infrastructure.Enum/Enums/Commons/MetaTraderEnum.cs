using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum OrderTypeEnum
    {
        [Metadata(codeKey: "ORDER_TYPE_BUY", nameKey: "BUY", descriptionKey: "Market Buy order")]
        Buy = 0,

        [Metadata(codeKey: "ORDER_TYPE_SELL", nameKey: "SELL", descriptionKey: "Market Sell order")]
        Sell = 1,

        [Metadata(codeKey: "ORDER_TYPE_CLOSE_BY", nameKey: "CLOSE", descriptionKey: "Order to close a position by an opposite one")]
        Close = 8
    }
}
