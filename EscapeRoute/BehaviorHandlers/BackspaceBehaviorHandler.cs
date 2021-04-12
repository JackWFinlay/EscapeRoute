using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class BackspaceBehaviorHandler : IEscapeRouteBehaviorHandler<BackspaceBehavior>
    {
        private const string _pattern = "\b";
        private const string _replacePattern = @"\b";
        private const string _stripPattern = "";
        
        public Task<string> EscapeAsync(string raw, BackspaceBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }

        public async Task<string> EscapeAsync(string raw, BackspaceBehavior behavior, IReplacementEngine replacementEngine)
        {
            var escaped = behavior switch
            {
                // Replace backspace characters with \b.
                BackspaceBehavior.Escape => await replacementEngine.ReplaceAsync(raw, _pattern, _replacePattern),
                BackspaceBehavior.Strip => await replacementEngine.ReplaceAsync(raw, _pattern, _stripPattern),
                _ => throw new ArgumentException($"Not a valid {nameof(BackspaceBehavior)}", nameof(behavior))
            };

            return escaped;
        }

        private static string HandleBehavior(string raw, BackspaceBehavior behavior)
        {
            var escaped = behavior switch
            {
                // Replace backspace characters with \b.
                BackspaceBehavior.Escape => Regex.Replace(raw, _pattern, @"\b"),
                BackspaceBehavior.Strip => raw.Replace(_pattern, ""),
                _ => throw new ArgumentException($"Not a valid {nameof(BackspaceBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}