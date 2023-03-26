using AdionFA.Infrastructure.Common.Attributes;
using AdionFA.Infrastructure.Enums;
using System;

namespace AdionFA.Infrastructure.Common.Extractor.Model
{
    public class IndicatorBase
    {
        public string Name => Type != null ? Enum.GetName(typeof(IndicatorEnum), Type) : "";
        public IndicatorEnum? Type { get; set; }

        [IgnoreReflection]
        public int OutBegIdx { get; set; }
        [IgnoreReflection]
        public int OutNBElement { get; set; }
        [IgnoreReflection]
        public MathOperatorEnum? Operator { get; set; }
        [IgnoreReflection]
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
