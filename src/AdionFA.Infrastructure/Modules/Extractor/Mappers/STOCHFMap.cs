using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public class STOCHFMap : ClassMap<STOCHF>
    {
        public STOCHFMap()
        {
            Map(m => m.OptInFastKPeriod).Index(3).TypeConverter<STOCHFValueTransform<int>>();
            Map(m => m.OptInFastDPeriod).Index(4).TypeConverter<STOCHFValueTransform<int>>();
            Map(m => m.OptInFastDMAType).Index(5).TypeConverter<STOCHFValueTransform<Enum>>();
            Map(m => m.STOCHFOutput).Index(6).TypeConverter<STOCHFValueTransform<Enum>>();
        }
    }

    public class STOCHFValueTransform<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            object result = null;
            var dataTrim = text.Replace("[", string.Empty).Replace("]", string.Empty);
            if (memberMapData.Index == 5)
            {
                if (Enum.TryParse(dataTrim.Trim(), out MATypeEnum parse))
                {
                    result = parse;
                }
            }
            else if (memberMapData.Index == 6)
            {
                if (dataTrim == STOCHFOutputEnum.FastK.GetDescription())
                {
                    result = STOCHFOutputEnum.FastK;
                }
                if (dataTrim == STOCHFOutputEnum.FastD.GetDescription())
                {
                    result = STOCHFOutputEnum.FastD;
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
