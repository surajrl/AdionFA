using AdionFA.Infrastructure.Common.Attributes;
using System;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Infrastructures.MetaTrader.Model
{
    public class ZmqMsgRequestModel
    {
        [PropertyOrder]
        public string UUID { get; set; }

        [PropertyOrder]
        public string SYMBOL { get; set; }

        [PropertyOrder]
        public string Request { get; set; }

        [PropertyOrder]
        public string OrderType { get; set; }

        [PropertyOrder]
        public string Action { get; set; }

        public string Message => string.Join("|", (from prop in GetType().GetProperties()
                                                   where Attribute.IsDefined(prop, typeof(PropertyOrderAttribute))
                                                   orderby ((PropertyOrderAttribute)prop.GetCustomAttributes(typeof(PropertyOrderAttribute), false).Single()).Order
                                                   select prop.GetValue(this)).ToList());
    }
}
