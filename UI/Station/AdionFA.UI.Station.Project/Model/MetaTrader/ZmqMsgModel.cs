using AdionFA.UI.Station.Infrastructure.Base;
using System;

namespace AdionFA.UI.Station.Project.Model.MetaTrader
{
    public class ZmqMsgModel : ViewModelBase
    {
        public int Id { get; set; }

        public bool IsCurrentCandle { get; set; }

        public string DateFormat { get; set; }

        // Candle Data

        public DateTime Date { get; set; }
        public string Time { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public decimal Spread { get; set; }

    }
}