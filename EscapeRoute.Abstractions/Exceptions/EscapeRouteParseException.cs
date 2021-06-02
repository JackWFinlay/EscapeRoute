using System;

namespace EscapeRoute.Abstractions.Exceptions
{
    public class EscapeRouteParseException : Exception
    {
        public EscapeRouteParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}