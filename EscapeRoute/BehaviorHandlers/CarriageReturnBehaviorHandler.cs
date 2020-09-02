using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class CarriageReturnBehaviorHandler : IEscapeRouteBehaviorHandler<CarriageReturnBehavior>
    {
        public Task<string> EscapeAsync(string raw, CarriageReturnBehavior behavior)
        {
            var escaped = HandleCarriageReturnBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }
        
        private static string HandleCarriageReturnBehavior(string raw, CarriageReturnBehavior behavior)
        {
            string escaped = null;

            switch (behavior)
            {
                case CarriageReturnBehavior.Escape:
                {
                    // Replace carriage return characters with \r.
                    Regex regex = new Regex("\r");
                    escaped = regex.Replace(raw, @"\r");
                    break;
                }
                case CarriageReturnBehavior.Strip:
                default:
                    // Remove carriage return characters.
                    escaped = raw.Replace("\r", "");
                    break;
            }

            return escaped;
        }
    }
}