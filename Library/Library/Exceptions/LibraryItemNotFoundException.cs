using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions
{
    [Serializable]
    public class LibraryItemNotFoundException : Exception
    {
        public LibraryItemNotFoundException()
        {
        }

        public LibraryItemNotFoundException(string message) : base(message)
        {
        }

        public LibraryItemNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LibraryItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
