using System;
using System.Threading.Tasks;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IEscapeRouteCustomBehaviorHandler
    {
        Task<ReadOnlyMemory<char>> EscapeAsync(ReadOnlyMemory<char> raw);
    }
}