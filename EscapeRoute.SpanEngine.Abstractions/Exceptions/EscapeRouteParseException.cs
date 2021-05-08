using System;

namespace EscapeRoute.SpanEngine.Abstractions.Exceptions
{
    public class EscapeRouteParseException : Exception
    {
        public EscapeRouteParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}