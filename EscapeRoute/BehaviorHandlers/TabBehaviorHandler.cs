using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class TabBehaviorHandler: IEscapeRouteBehaviorHandler<TabBehavior>
    {
        public Task<string> EscapeAsync(string raw, TabBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }
        
        private static string HandleBehavior(string raw, TabBehavior behavior)
        {
            var escaped = behavior switch
            {
                TabBehavior.Escape => Regex.Replace(raw, "\t", @"\t"),
                TabBehavior.Strip => raw.Replace("\t", ""),
                _ => throw new ArgumentException($"Not a valid {nameof(TabBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}