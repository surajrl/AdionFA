using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Enums.Model;
using AdionFA.Infrastructure.I18n.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdionFA.Infrastructure.Common.Helpers
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

        public static TimeframeEnum GetTimeframeEnum(int code)
        {
            var result = TimeframeEnum.D1;
            switch (code)
            {
                case 1:
                    result = TimeframeEnum.M1;
                    break;

                case 2:
                    result = TimeframeEnum.M5;
                    break;

                case 3:
                    result = TimeframeEnum.M15;
                    break;

                case 4:
                    result = TimeframeEnum.M30;
                    break;

                case 5:
                    result = TimeframeEnum.H1;
                    break;

                case 6:
                    result = TimeframeEnum.H4;
                    break;

                case 7:
                    result = TimeframeEnum.D1;
                    break;
            }

            return result;
        }
    }
}