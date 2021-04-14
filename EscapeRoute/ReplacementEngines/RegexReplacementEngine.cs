using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.ReplacementEngines
{
    // public class RegexReplacementEngine : IReplacementEngine
    // {
    //     public Task<ReadOnlyMemory<char>> ReplaceAsync(ReadOnlyMemory<char> raw, string pattern, string replacement)
    //     {
    //         var escaped = Regex.Replace(raw.ToString(), pattern, replacement);
    //         return Task.FromResult(escaped.AsMemory());
    //     }
    // }
}