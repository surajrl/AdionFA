﻿using Adion.FA.Infrastructure.Common.Extractor.Model;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Diagnostics;

namespace Adion.FA.Infrastructure.Common.Mappers
{
    public class CandleMap : ClassMap<Candle>
    {
        public CandleMap()
        {
            Map(m => m.date).Index(0).TypeConverter<CandleValueTransform<DateTime>>();
            Map(m => m.time).Index(1).TypeConverter<CandleValueTransform<long>>();
            Map(m => m.open).Index(2);
            Map(m => m.max).Index(3);
            Map(m => m.min).Index(4);
            Map(m => m.close).Index(5);
            Map(m => m.volumen).Index(6);
        }

        public class CandleValueTransform<T> : DefaultTypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                try
                {
                    switch (memberMapData.Index)
                    {
                        case 0:
                            string dtFormat = text.Replace('.', '/');
                            return Convert.ToDateTime(dtFormat);
                        case 1:
                            string timeFormat = text.Split(':')[0];
                            long.TryParse(timeFormat, out long time);
                            return time;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    throw;
                }
            }
        }
    }
}
