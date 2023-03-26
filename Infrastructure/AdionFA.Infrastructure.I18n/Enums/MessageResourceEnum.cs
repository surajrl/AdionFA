using AdionFA.Infrastructure.I18n.Resources;
using System;
using System.Resources;

namespace AdionFA.Infrastructure.I18n.Enums
{
    public class ResourceEnumAttribute : Attribute
    {
        private readonly string _key;
        private readonly ResourceManager _resource;

        public ResourceEnumAttribute(Type resourceType, string key = null)
        {
            _resource = resourceType != null ? new ResourceManager(resourceType) : null;
            _key = key;
        }

        public string Message
        {
            get => _resource?.GetString(_key ?? "");
        }

        public string GetMessage(string key)
        {
            return _resource.GetString(key);
        }
    }

    public static class ResourceEnumExtension
    {
        public static string GetMessage(this MessageResourceEnum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                System.Reflection.FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    ResourceEnumAttribute attr = (ResourceEnumAttribute)Attribute.GetCustomAttribute(field, typeof(ResourceEnumAttribute));
                    if (attr != null)
                    {
                        return attr.Message ?? attr.GetMessage(name);
                    }
                }
            }
            return string.Empty;
        }
    }

    public enum MessageResourceEnum
    {
        #region Common 1 - 999

        #endregion 

        #region Project 1000 - 1999

        [ResourceEnum(typeof(MessageResources))]
        CurrencyPairAndCurrencyPeriodMustBeSame = 1000

        #endregion

        #region Extractor 2000 - 2999

        #endregion

        #region Strategy Builder 3000 - 3999

        #endregion
    }
}
