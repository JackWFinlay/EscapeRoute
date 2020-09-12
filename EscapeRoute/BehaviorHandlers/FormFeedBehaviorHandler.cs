using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class FormFeedBehaviorHandler : IEscapeRouteBehaviorHandler<FormFeedBehavior>
    {
        public Task<string> EscapeAsync(string raw, FormFeedBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }
        
        private static string HandleBehavior(string raw, FormFeedBehavior behavior)
        {
            var escaped = behavior switch
            {
                FormFeedBehavior.Escape => Regex.Replace(raw, "\f",@"\f"),
                FormFeedBehavior.Strip => raw.Replace("\f", ""),
                _ => throw new ArgumentException($"Not a valid {nameof(FormFeedBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}