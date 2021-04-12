using System;
using System.IO;
using System.Threading.Tasks;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IReplacementEngine
    {
        Task<string> ReplaceAsync(string raw, string pattern, string replacement);
    }
}