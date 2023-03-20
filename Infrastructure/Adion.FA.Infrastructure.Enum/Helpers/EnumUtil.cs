using Adion.FA.Infrastructure.Enums;
using Adion.FA.Infrastructure.Enums.Model;
using Adion.FA.Infrastructure.I18n.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adion.FA.Infrastructure.Common.Helpers
{
    public static class EnumUtil
    {
        public static IEnumerable<Metadata> ToEnumerable<T>(bool insertNullable = false)
        {
            MethodInfo m = typeof(EnumExtension).GetMethod("GetMetadata");
            if (m == null)
                throw new NullReferenceException("Method GetMetadata not found...");

            IList<Metadata> result = Array.Empty<Metadata>().ToList();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var metadata = (Metadata)m.Invoke(item, new object[] { item });
                if (metadata != null)
                {
                    result.Add(metadata);
                }
            }

            if (insertNullable && result.Any())
            {
                result.Insert(0, new Metadata
                {
                    Id = 0,
                    Name = CommonResources.SelectItem
                });
            }

            return result.AsEnumerable();
        }


        public static CurrencyPeriodEnum GetPeriodEnum(int code)
        {
            var result = CurrencyPeriodEnum.Daily;
            switch (code)
            {
                case 1:
                    result = CurrencyPeriodEnum.M1;
                    break;
                case 5:
                    result = CurrencyPeriodEnum.M5;
                    break;
                case 15:
                    result = CurrencyPeriodEnum.M15;
                    break;
                case 30:
                    result = CurrencyPeriodEnum.M30;
                    break;
                case 16385:
                    result = CurrencyPeriodEnum.H1;
                    break;
                case 16388:
                    result = CurrencyPeriodEnum.H4;
                    break;
                case 16408:
                    result = CurrencyPeriodEnum.Daily;
                    break;
            }

            return result;
        }
    }
}
