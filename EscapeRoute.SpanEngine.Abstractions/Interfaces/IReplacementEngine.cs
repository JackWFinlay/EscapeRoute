using System;
using System.Threading.Tasks;

namespace EscapeRoute.SpanEngine.Abstractions.Interfaces
{
    public interface IReplacementEngine
    {
        Task<ReadOnlyMemory<char>> ReplaceAsync(ReadOnlyMemory<char> raw, IEscapeRouteConfiguration config);
    }
}