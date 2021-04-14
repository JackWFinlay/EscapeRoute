using System;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class TrimBehaviorHandler: IEscapeRouteBehaviorHandler<TrimBehavior>
    {
        public Task<ReadOnlyMemory<char>> EscapeAsync(ReadOnlyMemory<char> raw, TrimBehavior behavior, IReplacementEngine replacementEngine)
        {
            return Task.FromResult(HandleBehavior(raw.ToString(), behavior).AsMemory());
        }

        public Task<string> EscapeAsync(string raw, TrimBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }
        
        private static string HandleBehavior(string raw, TrimBehavior behavior)
        {
            var escaped = behavior switch
            {
                TrimBehavior.Start => raw.TrimStart(),
                TrimBehavior.End => raw.TrimEnd(),
                TrimBehavior.Both => raw.Trim(),
                TrimBehavior.None => raw,
                _ => throw new ArgumentException($"Not a valid {nameof(TrimBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}