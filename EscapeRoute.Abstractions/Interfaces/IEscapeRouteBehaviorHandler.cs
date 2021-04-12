using System;
using System.Threading.Tasks;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IEscapeRouteBehaviorHandler<in T> where T : Enum
    {
        Task<string> EscapeAsync(string raw, T behavior);
        Task<string> EscapeAsync(string raw, T behavior, IReplacementEngine replacementEngine);
    }
}