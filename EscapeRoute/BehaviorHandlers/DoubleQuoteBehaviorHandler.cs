using System;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class DoubleQuoteBehaviorHandler: IEscapeRouteBehaviorHandler<DoubleQuoteBehavior>
    {
        public Task<string> EscapeAsync(string raw, DoubleQuoteBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }
        
        private static string HandleBehavior(string raw, DoubleQuoteBehavior behavior)
        {
            var escaped = behavior switch
            {
                DoubleQuoteBehavior.Single => raw.Replace("\"", @"\'"),
                DoubleQuoteBehavior.Double => raw.Replace("\"", @"\"""),
                _ => throw new ArgumentException($"Not a valid {nameof(DoubleQuoteBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}