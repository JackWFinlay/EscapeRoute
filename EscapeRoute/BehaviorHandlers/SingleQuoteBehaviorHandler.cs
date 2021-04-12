using System;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class SingleQuoteBehaviorHandler: IEscapeRouteBehaviorHandler<SingleQuoteBehavior>
    {
        private const string _pattern = "\'";
        private const string _replaceSinglePattern = @"\'";
        private const string _replaceDoublePattern = @"\""";
        
        public Task<string> EscapeAsync(string raw, SingleQuoteBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }

        public async Task<string> EscapeAsync(string raw, SingleQuoteBehavior behavior, IReplacementEngine replacementEngine)
        {
            var escaped = behavior switch
            {
                SingleQuoteBehavior.Single => await replacementEngine.ReplaceAsync(raw, _pattern, _replaceSinglePattern),
                SingleQuoteBehavior.Double => await replacementEngine.ReplaceAsync(raw, _pattern, _replaceDoublePattern),
                _ => throw new ArgumentException($"Not a valid {nameof(SingleQuoteBehavior)}", nameof(behavior))
            };

            return escaped;
        }

        private static string HandleBehavior(string raw, SingleQuoteBehavior behavior)
        {
            var escaped = behavior switch
            {
                SingleQuoteBehavior.Single => raw.Replace(_pattern, @"\'"),
                SingleQuoteBehavior.Double => raw.Replace(_pattern, @"\"""),
                _ => throw new ArgumentException($"Not a valid {nameof(SingleQuoteBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}