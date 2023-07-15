using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Domain.Helpers
{
    public static class EnumUtil
    {
        public static IEnumerable<Metadata> ToEnumerable<T>(bool insertNullable = false)
        {
            var m = typeof(EnumExtension).GetMethod("GetMetadata") ?? throw new NullReferenceException("Method GetMetadata not found...");
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
                    Name = "Select Item"
                });
            }

            return result.AsEnumerable();
        }

        public static TimeframeEnum GetTimeframeEnum(int code)
        {
            var result = code switch
            {
                1 => TimeframeEnum.M1,
                5 => TimeframeEnum.M5,
                15 => TimeframeEnum.M15,
                30 => TimeframeEnum.M30,
                16385 => TimeframeEnum.H1,
                16388 => TimeframeEnum.H4,
                16408 => TimeframeEnum.D1,
                32769 => TimeframeEnum.W1,
                49153 => TimeframeEnum.M1,
                _ => TimeframeEnum.D1,
            };
            return result;
        }
    }
}
