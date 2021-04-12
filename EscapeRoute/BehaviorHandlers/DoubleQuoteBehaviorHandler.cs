using System;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class DoubleQuoteBehaviorHandler: IEscapeRouteBehaviorHandler<DoubleQuoteBehavior>
    {
        private const string _pattern = "\"";
        private const string _replaceSinglePattern = @"\'";
        private const string _replaceDoublePattern = @"\""";
        
        public Task<string> EscapeAsync(string raw, DoubleQuoteBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }

        public async Task<string> EscapeAsync(string raw, DoubleQuoteBehavior behavior, IReplacementEngine replacementEngine)
        {
            var escaped = behavior switch
            {
                DoubleQuoteBehavior.Single => await replacementEngine.ReplaceAsync(raw, _pattern, _replaceSinglePattern),
                DoubleQuoteBehavior.Double => await replacementEngine.ReplaceAsync(raw, _pattern, _replaceDoublePattern),
                _ => throw new ArgumentException($"Not a valid {nameof(DoubleQuoteBehavior)}", nameof(behavior))
            };

            return escaped;
        }

        private static string HandleBehavior(string raw, DoubleQuoteBehavior behavior)
        {
            var escaped = behavior switch
            {
                DoubleQuoteBehavior.Single => raw.Replace(_pattern, @"\'"),
                DoubleQuoteBehavior.Double => raw.Replace(_pattern, @"\"""),
                _ => throw new ArgumentException($"Not a valid {nameof(DoubleQuoteBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}