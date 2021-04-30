using System;

namespace EscapeRoute.SpanEngine.Abstractions.Interfaces
{
    public interface IEscapeRouteEscapeHandler<in T> where T : Enum
    {
        char GetPattern();
        ReadOnlyMemory<char> GetReplacement(T behavior);
    }
}