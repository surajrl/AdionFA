using System;
using System.Linq;
using System.Reflection;

namespace AdionFA.Infrastructure.Extensions
{
    public static class TypeExtension
    {
        public static PropertyInfo[] GetFilteredProperties<T>(this Type type, bool noAttributes = false) where T : Attribute
        {
            var result = Array.Empty<PropertyInfo>();

            if (noAttributes)
            {
                result = type.GetProperties().Where(p => p.GetCustomAttribute<T>() != null).ToArray();
            }
            else
            {
                result = type.GetProperties().Where(p => p.GetCustomAttribute<T>() == null).ToArray();
            }

            return result;
        }

        public static PropertyInfo[] GetByAttributeProperties(this Type type, Type attributte)
        {
            return type.GetProperties().Where(pi => pi.GetCustomAttributes(attributte, true).Length > 0).ToArray();
        }
    }
}
