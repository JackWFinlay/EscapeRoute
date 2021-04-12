using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class TabBehaviorHandler: IEscapeRouteBehaviorHandler<TabBehavior>
    {
        private const string _pattern = "\t";
        private const string _replacePattern = @"\t";
        private const string _stripPattern = "";
        
        public Task<string> EscapeAsync(string raw, TabBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }

        public async Task<string> EscapeAsync(string raw, TabBehavior behavior, IReplacementEngine replacementEngine)
        {
            var escaped = behavior switch
            {
                TabBehavior.Escape => await replacementEngine.ReplaceAsync(raw, _pattern, _replacePattern),
                TabBehavior.Strip => await replacementEngine.ReplaceAsync(raw, _pattern, _stripPattern),
                _ => throw new ArgumentException($"Not a valid {nameof(TabBehavior)}", nameof(behavior))
            };

            return escaped;
        }

        private static string HandleBehavior(string raw, TabBehavior behavior)
        {
            var escaped = behavior switch
            {
                TabBehavior.Escape => Regex.Replace(raw, _pattern, @"\t"),
                TabBehavior.Strip => raw.Replace(_pattern, ""),
                _ => throw new ArgumentException($"Not a valid {nameof(TabBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}