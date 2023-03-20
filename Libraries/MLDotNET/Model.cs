using System;
using System.Runtime.CompilerServices;

namespace MLDotNET
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class OrderAttribute : Attribute
    {
        private readonly int order_;
        public OrderAttribute([CallerLineNumber] int order = 0)
        {
            order_ = order;
        }

        public int Order { get { return order_; } }
    }

    public class ZmqMsgRequestModel
    {
        [Order]
        public string UUID { get; set; }

        [Order]
        public string SYMBOL { get; set; }

        [Order]
        public string Request { get; set; }

        [Order]
        public string OrderType { get; set; }

        [Order]
        public string Action { get; set; }
    }

    public enum ZmqMsgRequestEnum 
    {
        TRADE = 1,
        RATE = 2,
    }

    public enum OrderTypeEnum
    {
        ORDER_TYPE_BUY = 1,
        ORDER_TYPE_SELL = 2
    }
}
