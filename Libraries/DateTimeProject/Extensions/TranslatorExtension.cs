using DateTimeProject.Attributes;
using DateTimeProject.Enums;
using System;
using System.Globalization;
using System.Resources;

namespace DateTimeProject.Extensions
{
    public static class TranslatorExtension
    {
        public static string GetTranslatedString(this ResourceManager resourceManager, string resourceKey, string langCode = "en")
        {
            var cultureInfo = new CultureInfo(langCode?.Replace("jp", "ja") ?? "");
            var displayName = resourceManager?.GetString(resourceKey, cultureInfo);

            return string.IsNullOrEmpty(displayName)
                ? resourceKey
                : displayName;
        }
        public static string GetTranslatedString(this ResourceManager resourceManager, string resourceKey, int languageId)
        {
            var language = (LanguageEnum)languageId;
            var cultureInfo = language.GetCulture();
            var displayName = resourceManager?.GetString(resourceKey, cultureInfo);

            return string.IsNullOrEmpty(displayName)
                ? resourceKey
                : displayName;
        }
        public static CultureInfo GetCulture(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null) return null;

            var field = type.GetField(name);
            if (field == null) return null;

            var attr = (CodeLangAttribute)Attribute.GetCustomAttribute(field, typeof(CodeLangAttribute));

            return attr != null ? new CultureInfo(attr.Code.Replace("jp", "ja")) : null;
        }
    }
}
