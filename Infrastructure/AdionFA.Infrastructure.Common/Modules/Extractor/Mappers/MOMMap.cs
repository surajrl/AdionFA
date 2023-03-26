using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AdionFA.Infrastructure.Common.Extractor.Mappers
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
            string dataTrim = text.Replace('[',' ').Replace(']',' ');
            if (memberMapData.Index == 0)
            {
                string[] dataSplit = dataTrim.Split(';');
                string data = dataSplit.Length > 0 ? dataSplit[1].Trim() : "1";
                if (Enum.TryParse(data, out PriceTypeEnum parse))
                {
                    switch (parse)
                    {
                        case PriceTypeEnum.OPEN_PRICE:
                            return (int)PriceTypeEnum.OPEN_PRICE;
                        case PriceTypeEnum.HIGH_PRICE:
                            return (int)PriceTypeEnum.HIGH_PRICE;
                        case PriceTypeEnum.LOW_PRICE:
                            return (int)PriceTypeEnum.LOW_PRICE;
                        case PriceTypeEnum.CLOSE_PRICE:
                            return (int)PriceTypeEnum.CLOSE_PRICE;
                    }
                }
                else
                {
                    return (int)PriceTypeEnum.CLOSE_PRICE;
                }
            }

            return result;
        }
    }
}
