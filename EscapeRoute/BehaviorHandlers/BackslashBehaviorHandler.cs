using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class BackslashBehaviorHandler : IEscapeRouteBehaviorHandler<BackslashBehavior>
    {
        private const string _pattern = "\\\\";
        private const string _replacePattern = @"\\";
        private const string _stripPattern = "";
        
        public Task<string> EscapeAsync(string raw, BackslashBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }

        public async Task<string> EscapeAsync(string raw, BackslashBehavior behavior, IReplacementEngine replacementEngine)
        {
            var escaped = behavior switch
            {
                // Replace backslash with \\.
                BackslashBehavior.Escape => await replacementEngine.ReplaceAsync(raw, _pattern, _replacePattern),
                BackslashBehavior.Strip => await replacementEngine.ReplaceAsync(raw, _pattern, _stripPattern),
                _ => throw new ArgumentException($"Not a valid {nameof(BackslashBehavior)}", nameof(behavior))
            };

            return escaped;
        }

        private static string HandleBehavior(string raw, BackslashBehavior behavior)
        {
            var escaped = behavior switch
            {
                // Replace backslash with \\.
                BackslashBehavior.Escape => Regex.Replace(raw, "\\\\", @"\\"),
                BackslashBehavior.Strip => raw.Replace("\\", ""),
                _ => throw new ArgumentException($"Not a valid {nameof(BackslashBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}