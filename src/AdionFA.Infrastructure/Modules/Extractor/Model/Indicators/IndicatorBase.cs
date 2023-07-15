using AdionFA.Domain.Enums;
using System;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class IndicatorBase
    {
        public string Name => Type != null ? Enum.GetName(typeof(IndicatorEnum), Type) : "";
        public IndicatorEnum? Type { get; set; }

        public int OutBegIdx { get; set; }
        public int OutNBElement { get; set; }
        public MathOperatorEnum? Operator { get; set; }
        public double Value { get; set; }

        public IntervalLabel[] IntervalLabels { get; set; }
        public double[] Output { get; set; }
    }

    public class IntervalLabel
    {
        public DateTime Interval { get; set; }
        public string Label { get; set; }
    }
}
