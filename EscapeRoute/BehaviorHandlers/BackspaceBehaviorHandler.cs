using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class BackspaceBehaviorHandler : IEscapeRouteBehaviorHandler<BackspaceBehavior>
    {
        public Task<string> EscapeAsync(string raw, BackspaceBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }
        
        private static string HandleBehavior(string raw, BackspaceBehavior behavior)
        {
            var escaped = behavior switch
            {
                // Replace backspace characters with \b.
                BackspaceBehavior.Escape => Regex.Replace(raw, "\b", @"\b"),
                BackspaceBehavior.Strip => raw.Replace("\b", ""),
                _ => throw new ArgumentException($"Not a valid {nameof(BackspaceBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}