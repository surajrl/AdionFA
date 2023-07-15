using System;

namespace AdionFA.Domain.Exceptions
{
    [Serializable]
    public class PropertiesWithSameValueAdionException : Exception
    {
        public PropertiesWithSameValueAdionException() : base() { }

        public PropertiesWithSameValueAdionException(string message) : base(message) { }

        public PropertiesWithSameValueAdionException(string message, Exception exception) : base(message, exception) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected PropertiesWithSameValueAdionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
