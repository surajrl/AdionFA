using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public class STOCHMap : ClassMap<STOCH>
    {
        public STOCHMap()
        {
            Map(m => m.OptInFastKPeriod).Index(3).TypeConverter<STOCHValueTransform<int>>(); ;
            Map(m => m.OptInSlowKPeriod).Index(4).TypeConverter<STOCHValueTransform<int>>(); ;
            Map(m => m.OptInSlowKMAType).Index(5).TypeConverter<STOCHValueTransform<Enum>>();
            Map(m => m.OptInSlowDPeriod).Index(6).TypeConverter<STOCHValueTransform<int>>(); ;
            Map(m => m.OptInSlowDMAType).Index(7).TypeConverter<STOCHValueTransform<Enum>>();
        }
    }

    public class STOCHValueTransform<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            object result = null;
            var dataTrim = text.Replace("[", string.Empty).Replace("]", string.Empty);
            if (memberMapData.Index == 5 || memberMapData.Index == 7)
            {
                if (Enum.TryParse(dataTrim.Trim(), out MATypeEnum parse))
                {
                    result = parse;
                }
            }
            else
            {
                result = dataTrim;
            }

            if (typeof(T) == typeof(int) && int.TryParse(result.ToString(), out var r))
                result = r;

            return result;
        }
    }
}
