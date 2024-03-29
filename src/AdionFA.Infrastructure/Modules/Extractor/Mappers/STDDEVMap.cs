﻿using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public sealed class STDDEVMap : ClassMap<STDDEV>
    {
        public STDDEVMap()
        {
            Map(m => m.PriceType).Index(0).TypeConverter<STDDEVValueTransform<Enum>>();
            Map(m => m.OptInTimePeriod).Index(1);
            Map(m => m.OptInNbDev).Index(2);
        }
    }

    public class STDDEVValueTransform<T> : DefaultTypeConverter
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
