using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public class MACDMap : ClassMap<MACD>
    {
        public MACDMap()
        {
            Map(m => m.PriceType).Index(0).TypeConverter<MACDValueTransform<Enum>>();
            Map(m => m.OptInFastPeriod).Index(1);
            Map(m => m.OptInSlowPeriod).Index(2);
            Map(m => m.OptInSignalPeriod).Index(3);
            Map(m => m.MACDOutput).Index(4).TypeConverter<MACDValueTransform<Enum>>();
        }
    }

    public class MACDValueTransform<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            Enum result = null;
            var dataTrim = text.Replace("[", string.Empty).Replace("]", string.Empty);
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
            else if (memberMapData.Index == 4)
            {
                if (dataTrim == MACDOutputEnum.Macd.GetDescription())
                {
                    result = MACDOutputEnum.Macd;
                }
                if (dataTrim == MACDOutputEnum.MacdSignal.GetDescription())
                {
                    result = MACDOutputEnum.MacdSignal;
                }
                if (dataTrim == MACDOutputEnum.MacdHist.GetDescription())
                {
                    result = MACDOutputEnum.MacdHist;
                }
            }

            return result;
        }
    }
}
