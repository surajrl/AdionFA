﻿using AdionFA.UI.Station.Infrastructure.Base;
using System;

namespace AdionFA.UI.Station.Project.Model.MetaTrader
{
    public class ZmqMsgModel : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public bool IsNewCandle { get; set; }

        public int Temporality { get; set; }
        public string TemporalityName { get; set; }

        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string DateFormat { get; set; }

        public int PutType { get; set; }
        public string PutTypeName { get; set; }

        public decimal Volume { get; set; }
        public int PositionType { get; set; }
        public string PositionTypeName { get; set; }

        public string Description { get; set; }

        public decimal High { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal Low { get; set; }
    }
}
