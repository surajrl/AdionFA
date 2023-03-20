using Adion.FA.Infrastructure.Common.Extractor.Model;
using Adion.FA.Infrastructure.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace Adion.FA.Infrastructure.Common.Extractor.Mappers
{
    public class STOCHRSIMap : ClassMap<STOCHRSI>
    {
        public STOCHRSIMap()
        {
            Map(m => m.PriceType).Index(0).TypeConverter<STOCHRSIValueTransform<Enum>>();
            Map(m => m.OptInTimePeriod).Index(1);
            Map(m => m.OptInFastKPeriod).Index(2);
            Map(m => m.OptInFastDPeriod).Index(3);
            Map(m => m.OptInFastDMAType).Index(4).TypeConverter<STOCHRSIValueTransform<Enum>>();
            Map(m => m.STOCHRSIOutput).Index(5).TypeConverter<STOCHRSIValueTransform<Enum>>();
        }
    }

    public class STOCHRSIValueTransform<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            Enum result = null;
            string dataTrim = text.Replace("[", string.Empty).Replace("]", string.Empty);
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
            else if (memberMapData.Index == 4)
            {
                if (Enum.TryParse(dataTrim.Trim(), out MATypeEnum parse))
                {
                    result = parse;
                }
            }
            else if (memberMapData.Index == 5)
            {
                if (dataTrim == STOCHRSIOutputEnum.FastK.GetDescription())
                {
                    result = STOCHRSIOutputEnum.FastK;
                }
                if (dataTrim == STOCHRSIOutputEnum.FastD.GetDescription())
                {
                    result = STOCHRSIOutputEnum.FastD;
                }
            }

            return result;
        }
    }
}
