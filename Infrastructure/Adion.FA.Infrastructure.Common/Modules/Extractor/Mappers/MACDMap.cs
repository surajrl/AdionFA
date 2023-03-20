using Adion.FA.Infrastructure.Common.Extractor.Model;
using Adion.FA.Infrastructure.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace Adion.FA.Infrastructure.Common.Extractor.Mappers
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
