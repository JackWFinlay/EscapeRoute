using System;
using System.Threading.Tasks;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IEscapeRouteEscapeHandler<in T> where T : Enum
    {
        char GetPattern();
        ReadOnlyMemory<char> GetReplacement(T behavior);
    }
}