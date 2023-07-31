using AdionFA.Domain.Enums;
using System;

namespace AdionFA.UI.ProjectStation.Model.MetaTrader
{
    public class ZmqMsgModel
    {
        public int Id { get; set; }

        public bool CheckTrade { get; set; }

        public bool IsCurrentBar { get; set; }

        public string Symbol { get; set; }

        public OrderTypeEnum OrderType { get; set; }

        public string DateFormat { get; set; }

        // Candle Data

        public DateTime Date { get; set; }
        public string Time { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public double Spread { get; set; }
    }
}