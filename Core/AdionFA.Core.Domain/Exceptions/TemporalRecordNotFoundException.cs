using AdionFA.Core.Domain.Resources;
using System;

namespace AdionFA.Core.Domain.Exceptions
{
    [Serializable]
    public class TemporalRecordNotFoundException : Exception
    {
        public TemporalRecordNotFoundException() : base(ExceptionMessages.TemporalRecordNotFoundException) { }

        public TemporalRecordNotFoundException(string message) : base(message) { }

        public TemporalRecordNotFoundException(string message, Exception exception) : base(message, exception) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected TemporalRecordNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
