using System;

namespace EscapeRoute.SpanEngine.Abstractions.Interfaces
{
    public interface IEscapeRouteEscapeFunctionHandler<T> where T : Enum
    {
        Func<char, ReadOnlyMemory<char>> GetReplacement(T behavior);
    }
}