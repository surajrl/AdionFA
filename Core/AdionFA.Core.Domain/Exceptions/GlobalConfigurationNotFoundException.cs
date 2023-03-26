using AdionFA.Core.Domain.Resources;
using System;

namespace AdionFA.Core.Domain.Exceptions
{
    [Serializable]
    public class GlobalConfigurationNotFoundException : Exception
    {

        public GlobalConfigurationNotFoundException() : base(ExceptionMessages.GlobalConfigurationNotFound) { }

        public GlobalConfigurationNotFoundException(string message) : base(message) { }

        public GlobalConfigurationNotFoundException(string message, Exception exception) : base(message, exception) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected GlobalConfigurationNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
