using System;

namespace DateTimeProject.Attributes
{
    public class CodeLangAttribute : Attribute
    {
        public CodeLangAttribute(string code)
        {
            this.Code = code;
        }
        public string Code { get; private set; }
    }
}
