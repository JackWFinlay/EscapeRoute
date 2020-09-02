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
            var escaped = HandleBackslashBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }
        
        private static string HandleBackslashBehavior(string raw, BackslashBehavior behavior)
        {
            string escaped = null;
            
            switch (behavior)
            {
                case BackslashBehavior.Escape:
                {
                    // Replace backslash with \\.
                    Regex regex = new Regex("\\\\");
                    escaped = regex.Replace(raw, @"\\");
                    break;
                }
                case BackslashBehavior.Strip:
                default:
                {
                    // Remove backslash characters.
                    escaped = raw.Replace("\\", "");
                    break;
                }
            }

            return escaped;
        }
    }
}