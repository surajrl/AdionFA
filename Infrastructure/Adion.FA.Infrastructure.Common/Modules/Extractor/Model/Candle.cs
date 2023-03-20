using System;

namespace Adion.FA.Infrastructure.Common.Extractor.Model
{
    public class Candle
    {
        public DateTime date { get; set; }
        public long time { get; set; }
        public double open { get; set; }
        public double max { get; set; }
        public double min { get; set; }
        public double close { get; set; }
        public double volumen { get; set; }
        public string label { get; set; }
    }
}
