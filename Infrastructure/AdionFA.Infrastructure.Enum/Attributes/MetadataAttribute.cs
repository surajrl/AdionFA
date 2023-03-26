using System;
using System.Resources;

namespace AdionFA.Infrastructure.Enums.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class MetadataAttribute : Attribute
    {
        private string _code;
        private string _name;
        private string _description;

        // Extended
        private string _symbol;
        private readonly string _value;

        private readonly ResourceManager _resource;
        public MetadataAttribute(
            string codeKey = null, 
            string nameKey = null, 
            string descriptionKey = null,

            string symbolKey = null, 
            string valueKey = null, 
            Type resourceType = null)
        {
            _code = codeKey;
            _name = nameKey;
            _description = descriptionKey;

            _symbol = symbolKey;
            _value = valueKey;

            _resource = resourceType != null ? new ResourceManager(resourceType) : null;
        }

        public string Code
        {
            get
            {
                return _code ?? "";
            }
            set { _code = value; }
        }

        public string Name
        {
            get
            {
                string name = _resource?.GetString(_name??"");
                return string.IsNullOrEmpty(name) ? $"{_name??""}" : name;
            }
            set { _name = value; }
        }

        public string Description
        {
            get
            {
                string description = _resource?.GetString(_description ?? "");
                return string.IsNullOrEmpty(description) ? $"{_description ?? ""}" : description;
            }
            set { _description = value; }
        }

        public string Symbol
        {
            get
            {
                return _symbol ?? "";
            }
            set { _symbol = value; }
        }

        public string Value
        {
            get
            {
                string value = _resource?.GetString(_value??"");
                return string.IsNullOrEmpty(value) ? $"{_value??""}" : value;
            }
            set { _description = value; }
        }
    }
}
