using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class BackslashBehaviorHandler : IEscapeRouteBehaviorHandler<BackslashBehavior>
    {
        public Task<string> EscapeAsync(string raw, BackslashBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
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