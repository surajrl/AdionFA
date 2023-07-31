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
    }
}
