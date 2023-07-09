using AdionFA.Infrastructure.Enums;
using System;

namespace AdionFA.Infrastructure.Common.MetaTrader.Model
{
    public class ZmqMsgRequestModel
    {
        public string Symbol { get; set; }
        public OrderTypeEnum OrderType { get; set; }
        public double Volume { get; set; }

    }

    public class ZmqDownloadHistoricalDataRequest
    {
        public string CMD => "DHD";
        public string Symbol { get; set; }
        public TimeframeEnum Timeframe { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }

    public class ZmqLoadSymbolListRequest
    {
        public string CMD => "LSL";
    }

    public class ZmqResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
}