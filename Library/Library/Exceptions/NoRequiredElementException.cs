using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions
{
    [Serializable]
    public class NoRequiredElementException : Exception
    {
        public NoRequiredElementException()
        {
        }

        public NoRequiredElementException(string message) : base(message)
        {
        }

        public NoRequiredElementException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoRequiredElementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
