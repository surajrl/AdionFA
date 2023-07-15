using System;
using System.Runtime.Serialization;

namespace AdionFA.Domain.Exceptions
{
    [Serializable]
    public class TemporalRecordNotFoundException : Exception
    {
        public TemporalRecordNotFoundException()
            : base()
        {

        }

        public TemporalRecordNotFoundException(string message)
            : base(message)
        {

        }

        public TemporalRecordNotFoundException(string message, Exception exception)
            : base(message, exception)
        {

        }

        protected TemporalRecordNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
