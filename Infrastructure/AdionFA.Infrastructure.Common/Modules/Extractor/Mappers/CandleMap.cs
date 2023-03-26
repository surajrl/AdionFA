using AdionFA.Infrastructure.Common.Extractor.Model;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Diagnostics;

namespace AdionFA.Infrastructure.Common.Mappers
{
    public class CandleMap : ClassMap<Candle>
    {
        public CandleMap()
        {
            Map(m => m.Date).Index(0).TypeConverter<CandleValueTransform<DateTime>>();
            Map(m => m.Time).Index(1).TypeConverter<CandleValueTransform<long>>();
            Map(m => m.Open).Index(2);
            Map(m => m.High).Index(3);
            Map(m => m.Low).Index(4);
            Map(m => m.Close).Index(5);
            Map(m => m.Volume).Index(6);
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
