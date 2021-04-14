using System;
using System.IO;
using System.Threading.Tasks;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IReplacementEngine
    {
        Task<ReadOnlyMemory<char>> ReplaceAsync(ReadOnlyMemory<char> raw, IEscapeRouteConfiguration config);
    }
}