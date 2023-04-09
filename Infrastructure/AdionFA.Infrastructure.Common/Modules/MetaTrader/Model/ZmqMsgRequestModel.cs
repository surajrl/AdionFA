using AdionFA.Infrastructure.Common.Attributes;
using AdionFA.Infrastructure.Enums;
using System;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Infrastructures.MetaTrader.Model
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
}
