﻿using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AdionFA.Infrastructure.Common.Extractor.Mappers
{
    public sealed class CMOMap : ClassMap<CMO>
    {
        public CMOMap()
        {
            Map(m => m.PriceType).Index(0).TypeConverter<CMOValueTransform<Enum>>();
            Map(m => m.OptInTimePeriod).Index(1);
        }
    }

    public class CMOValueTransform<T> : DefaultTypeConverter
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
