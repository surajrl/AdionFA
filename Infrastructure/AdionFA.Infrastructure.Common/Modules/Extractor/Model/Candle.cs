﻿using System;

namespace AdionFA.Infrastructure.Common.Extractor.Model
{
    public class Candle
    {
        public DateTime Date { get; set; }
        public long Time { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public string Label { get; set; }
    }
}
