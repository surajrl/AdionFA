using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AdionFA.Infrastructure.Common.Extractor.Mappers
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
