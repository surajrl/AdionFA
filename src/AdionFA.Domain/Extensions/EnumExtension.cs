using AdionFA.Domain.Attributes;
using AdionFA.Domain.Model;
using System;
using System.ComponentModel;

namespace AdionFA.Domain.Extensions
{
    public static class EnumExtension
    {
        public static Metadata GetMetadata(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    var attr = (MetadataAttribute)Attribute.GetCustomAttribute(field, typeof(MetadataAttribute));
                    if (attr != null)
                    {
                        return new Metadata(attr.Code, attr.Name, attr.Value, (int)field.GetValue(value));
                    }
                }
            }

            return null;
        }

        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }

            return string.Empty;
        }
    }
}
