using System;
using System.Collections.Generic;
using System.Text;

namespace SRS.Core.Exceptions
{
    public class InvalidPhraseException : Exception
    {        
        protected InvalidPhraseException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public InvalidPhraseException(string message) : base(message)
        {
        }

        public InvalidPhraseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
