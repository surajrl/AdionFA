using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public sealed class MOMMap : ClassMap<MOM>
    {
        public MOMMap()
        {
            Map(m => m.PriceType).Index(0).TypeConverter<MOMValueTransform<Enum>>();
            Map(m => m.OptInTimePeriod).Index(1);
        }
    }

    public class MOMValueTransform<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            Enum result = null;
            var dataTrim = text.Replace('[', ' ').Replace(']', ' ');
            if (memberMapData.Index == 0)
            {
                var dataSplit = dataTrim.Split(';');
                var data = dataSplit.Length > 0 ? dataSplit[1].Trim() : "1";
                if (Enum.TryParse(data, out PriceTypeEnum parse))
                {
                    switch (parse)
                    {
                        case PriceTypeEnum.OPEN:
                            return (int)PriceTypeEnum.OPEN;
                        case PriceTypeEnum.HIGH:
                            return (int)PriceTypeEnum.HIGH;
                        case PriceTypeEnum.LOW:
                            return (int)PriceTypeEnum.LOW;
                        case PriceTypeEnum.CLOSE:
                            return (int)PriceTypeEnum.CLOSE;
                    }
                }
                else
                {
                    return (int)PriceTypeEnum.CLOSE;
                }
            }

            return result;
        }
    }
}
