using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class FormFeedBehaviorHandler : IEscapeRouteBehaviorHandler<FormFeedBehavior>
    {
        private const string _pattern = "\f";
        private const string _replacePattern = @"\f";
        private const string _stripPattern = "";
        public Task<string> EscapeAsync(string raw, FormFeedBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }

        public async Task<string> EscapeAsync(string raw, FormFeedBehavior behavior, IReplacementEngine replacementEngine)
        {
            var escaped = behavior switch
            {
                FormFeedBehavior.Escape => await replacementEngine.ReplaceAsync(raw, _pattern, _replacePattern),
                FormFeedBehavior.Strip => await replacementEngine.ReplaceAsync(raw, _pattern, _stripPattern),
                _ => throw new ArgumentException($"Not a valid {nameof(FormFeedBehavior)}", nameof(behavior))
            };

            return escaped;
        }

        private static string HandleBehavior(string raw, FormFeedBehavior behavior)
        {
            var escaped = behavior switch
            {
                FormFeedBehavior.Escape => Regex.Replace(raw, _pattern,@"\f"),
                FormFeedBehavior.Strip => raw.Replace(_pattern, ""),
                _ => throw new ArgumentException($"Not a valid {nameof(FormFeedBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}