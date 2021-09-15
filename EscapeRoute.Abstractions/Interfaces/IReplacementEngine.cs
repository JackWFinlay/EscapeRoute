using System;
using System.Threading.Tasks;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IReplacementEngine
    {
        Task<string> ReplaceAsync(ReadOnlyMemory<char> raw);
    }
}