using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine.EscapeHandlers
{
    public class CarriageReturnEscapeHandler : IEscapeRouteEscapeHandler<CarriageReturnBehavior>
    {
        private const char _pattern = '\r';
        private readonly ReadOnlyMemory<char> _replacePattern = new[] {'\\', 'r'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};
        public char GetPattern() => _pattern;

        public ReadOnlyMemory<char> GetReplacement(CarriageReturnBehavior behavior)
        {
            var replacement = behavior switch
            {
                CarriageReturnBehavior.Strip => _stripPattern,
                CarriageReturnBehavior.Replace => _replacePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(CarriageReturnBehavior)}", nameof(behavior))
            };

            return replacement;
        }
    }
}