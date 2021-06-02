using System;
using System.Threading.Tasks;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IReplacementEngine
    {
        Task<ReadOnlyMemory<char>> ReplaceAsync(ReadOnlyMemory<char> raw);
    }
}