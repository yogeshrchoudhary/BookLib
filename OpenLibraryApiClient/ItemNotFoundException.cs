using System;
using System.Runtime.Serialization;

namespace OpenLibraryApiClient
{
    [Serializable]
    public class IsbnNotFoundException : Exception
    {
        public IsbnNotFoundException()
        {
        }

        public IsbnNotFoundException(string message) : base(message)
        {
        }

        public IsbnNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IsbnNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}