using System;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class SingleQuoteBehaviorHandler: IEscapeRouteBehaviorHandler<SingleQuoteBehavior>
    {
        public Task<string> EscapeAsync(string raw, SingleQuoteBehavior behavior)
        {
            var escaped = HandleBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }
        
        private static string HandleBehavior(string raw, SingleQuoteBehavior behavior)
        {
            var escaped = behavior switch
            {
                SingleQuoteBehavior.Single => raw.Replace("\'", @"\'"),
                SingleQuoteBehavior.Double => raw.Replace("\'", @"\"""),
                _ => throw new ArgumentException($"Not a valid {nameof(SingleQuoteBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}