using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions
{
    [Serializable]
    public class UnknownElementTagException : Exception
    {
        public UnknownElementTagException()
        {
        }

        public UnknownElementTagException(string message) : base(message)
        {
        }

        public UnknownElementTagException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownElementTagException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
