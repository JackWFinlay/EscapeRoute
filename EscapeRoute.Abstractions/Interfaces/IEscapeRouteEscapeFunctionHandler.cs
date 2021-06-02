using System;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IEscapeRouteEscapeFunctionHandler<T> where T : Enum
    {
        Func<ReadOnlyMemory<char>, ReadOnlyMemory<char>> GetReplacement(T behavior);
    }
}