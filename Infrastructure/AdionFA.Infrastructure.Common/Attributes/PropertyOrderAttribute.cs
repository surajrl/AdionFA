using System;
using System.Runtime.CompilerServices;

namespace AdionFA.Infrastructure.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class PropertyOrderAttribute : Attribute
    {
        private readonly int order_;
        public PropertyOrderAttribute([CallerLineNumber] int order = 0)
        {
            order_ = order;
        }

        public int Order => order_;
    }
}
