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
            var escaped = HandleCarriageReturnBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }
        
        private static string HandleCarriageReturnBehavior(string raw, FormFeedBehavior behavior)
        {
            string escaped = null;

            switch (behavior)
            {
                case FormFeedBehavior.Escape:
                {
                    // Replace form feed characters with \f.
                    Regex regex = new Regex("\f");
                    escaped = regex.Replace(raw, @"\f");
                    break;
                }
                case FormFeedBehavior.Strip:
                default:
                    // Remove form feed characters.
                    escaped = raw.Replace("\f", "");
                    break;
            }

            return escaped;
        }
    }
}