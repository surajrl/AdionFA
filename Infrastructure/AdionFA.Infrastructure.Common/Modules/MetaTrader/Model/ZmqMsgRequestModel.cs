using AdionFA.Infrastructure.Common.Attributes;
using AdionFA.Infrastructure.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Common.MetaTrader.Model
{
    public class ZmqMsgRequestModel
    {
        [PropertyOrder]
        public string UUID { get; set; }

        [PropertyOrder]
        public string Action { get; set; }

        [PropertyOrder]
        public string Symbol { get; set; }

        [PropertyOrder]
        public string Volume { get; set; }

        [PropertyOrder]
        public OrderTypeEnum OrderType { get; set; }

        [PropertyOrder]
        public string Comment { get; set; }

        public string Message => string.Join("|", (from prop in GetType().GetProperties()
                                                   where Attribute.IsDefined(prop, typeof(PropertyOrderAttribute))
                                                   orderby ((PropertyOrderAttribute)prop.GetCustomAttributes(typeof(PropertyOrderAttribute), false).Single()).Order
                                                   select prop.GetValue(this)).ToList());
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
        public int Status { get; set; } // 0 Error, 1 OK
        public string Message { get; set; }
        public string Data { get; set; } // Each item separated by ','
    }
}
