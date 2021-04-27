using System;

namespace EscapeRoute.SpanEngine.Abstractions.Interfaces
{
    public interface IEscapeRouteEscapeHandler<in T> where T : Enum
    {
        char GetPattern();
        Func<char,ReadOnlyMemory<char>> GetReplacement(T behavior);
    }
}