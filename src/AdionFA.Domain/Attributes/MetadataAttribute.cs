using System;
using System.Resources;

namespace AdionFA.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class MetadataAttribute : Attribute
    {
        private readonly ResourceManager _resource;

        public MetadataAttribute(
            string codeKey = null,
            string nameKey = null,
            string valueKey = null,
            string descriptionKey = null,
            Type resourceType = null)
        {
            _code = codeKey;
            _name = nameKey;
            _value = valueKey;
            _description = descriptionKey;

            _resource = resourceType != null ? new ResourceManager(resourceType) : null;
        }

        private string _code;
        public string Code
        {
            get => _code ?? "";
            set => _code = value;
        }

        private string _name;
        public string Name
        {
            get
            {
                var name = _resource?.GetString(_name ?? "");
                return string.IsNullOrEmpty(name) ? $"{_name ?? ""}" : name;
            }
            set => _name = value;
        }

        private string _value;
        public string Value
        {
            get
            {
                var value = _resource?.GetString(_value ?? "");
                return string.IsNullOrEmpty(value) ? $"{_value ?? ""}" : value;
            }
            set => _value = value;
        }

        private string _description;
        public string Description
        {
            get
            {
                var description = _resource?.GetString(_description ?? "");
                return string.IsNullOrEmpty(description) ? $"{_description ?? ""}" : description;
            }
            set => _description = value;
        }
    }
}
