﻿using AdionFA.Infrastructure.Extractor.Attributes;
using AdionFA.Domain.Enums;

namespace AdionFA.Infrastructure.Extractor.Model
{
    public class MOM : IndicatorBase
    {
        public int PriceType { get; set; }

        [IndicatorPeriod]
        public int OptInTimePeriod { get; set; }

        public MOM()
        {
            Type = IndicatorEnum.MOM;
            OptInTimePeriod = 10;
        }
    }
}
