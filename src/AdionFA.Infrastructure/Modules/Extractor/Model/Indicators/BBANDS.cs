﻿using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;
using System.ComponentModel;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class BBANDS : IndicatorBase
    {
        public int PriceType { get; set; }
        public int MAType { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }
        public double OptInNbDevUp { get; set; }
        public double OptInNbDevDn { get; set; }
        public int BBANDSOutput { get; set; }

        public BBANDS()
        {
            Type = IndicatorEnum.BBANDS;
        }
    }

    public enum BBANDSOutputEnum
    {
        [Description("OUTPUT1")]
        RealUpperBand,

        [Description("OUTPUT2")]
        RealMiddleBand,

        [Description("OUTPUT3")]
        RealLowerBand,
    }
}
