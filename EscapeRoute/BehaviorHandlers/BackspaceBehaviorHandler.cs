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
            var escaped = HandleBackspaceBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }
        
        private static string HandleBackspaceBehavior(string raw, BackspaceBehavior behavior)
        {
            string escaped = null;

            switch (behavior)
            {
                case BackspaceBehavior.Escape:
                {    
                    // Replace backspace characters with \b.
                    Regex regex = new Regex("\b");
                    escaped = regex.Replace(raw, @"\b");
                    break;
                }
                case BackspaceBehavior.Strip:
                default:
                {
                    // Remove backspace characters.
                    escaped = raw.Replace("\b", "");
                    break;
                }
            }

            return escaped;
        }
    }
}