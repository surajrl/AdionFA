using DateTimeProject.Enums;
using DateTimeProject.Extensions;
using System;
using System.ComponentModel;
using System.Resources;
using System.Threading;

namespace DateTimeProject.Attributes
{
    public class I18NDescriptionAttribute : DescriptionAttribute
    {
        private readonly string _resourceKey;
        private readonly ResourceManager _resource;

        public I18NDescriptionAttribute(string resourceKey, Type resourceType)
        {
            _resource = new ResourceManager(resourceType);
            _resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                string displayName = _resource.GetString(_resourceKey, Thread.CurrentThread.CurrentUICulture);

                return string.IsNullOrEmpty(displayName)
                    ? $"[[{_resourceKey}]]"
                    : displayName;
            }
        }

        public string Translate(int languageId)
        {
            var language = (LanguageEnum)languageId;
            var cultureInfo = language.GetCulture();
            var displayName = _resource.GetString(_resourceKey, cultureInfo);

            return string.IsNullOrEmpty(displayName)
                ? _resourceKey
                : displayName;
        }
    }
}
