using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class UnicodeBehaviorHandler: IEscapeRouteBehaviorHandler<UnicodeBehavior>
    {
        // [^\x00-\x7F] matches any non-ASCII character.
        private const string _pattern = @"[^\x00-\x7F]";
        private const string _stripPattern = "";

        public Task<string> EscapeAsync(string raw, UnicodeBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }

        public async Task<string> EscapeAsync(string raw, UnicodeBehavior behavior, IReplacementEngine replacementEngine)
        {
            var escaped = behavior switch
            {
                // Replace non-ASCII character with it's equivalent in the form \uXXXX where XXXX is the character code.
                // https://stackoverflow.com/a/25349901
                UnicodeBehavior.Escape => Regex.Replace(raw, _pattern, c => $@"\u{(int)c.Value[0]:x4}"),
                // Strip out non-ASCII characters.
                UnicodeBehavior.Strip => await replacementEngine.ReplaceAsync(raw, _pattern, _stripPattern),
                _ => throw new ArgumentException($"Not a valid {nameof(UnicodeBehavior)}", nameof(behavior))
            };

            return escaped;
        }
        
        private static string HandleBehavior(string raw, UnicodeBehavior behavior)
        {
            var escaped = behavior switch
            {
                // Replace non-ASCII character with it's equivalent in the form \uXXXX where XXXX is the character code.
                // https://stackoverflow.com/a/25349901
                UnicodeBehavior.Escape => Regex.Replace(raw, _pattern, c => $@"\u{(int) c.Value[0]:x4}"),
                // Strip out non-ASCII characters.
                UnicodeBehavior.Strip => Regex.Replace(raw, _pattern, ""),
                _ => throw new ArgumentException($"Not a valid {nameof(UnicodeBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}