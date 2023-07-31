using AdionFA.Domain.Extensions;
using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public class AROONMap : ClassMap<AROON>
    {
        public AROONMap()
        {
            Map(m => m.OptInTimePeriod).Index(2);
            Map(m => m.AROONDownUp).Index(3).TypeConverter<AROONValueTransform<Enum>>();
        }
    }

    public class AROONValueTransform<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            Enum result = null;
            var dataTrim = text.Replace("[", string.Empty).Replace("]", string.Empty);
            if (memberMapData.Index == 3)
            {
                if (dataTrim == AROONDownUpEnum.DOWN.GetDescription())
                {
                    result = AROONDownUpEnum.DOWN;
                }
                if (dataTrim == AROONDownUpEnum.UP.GetDescription())
                {
                    result = AROONDownUpEnum.UP;
                }
            }

            return result;
        }
    }
}
