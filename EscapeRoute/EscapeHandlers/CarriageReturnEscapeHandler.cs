using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.EscapeHandlers
{
    public class CarriageReturnEscapeHandler : IEscapeRouteEscapeHandler<CarriageReturnBehavior>
    {
        private const char _pattern = '\r';
        private readonly ReadOnlyMemory<char> _replacePattern = new[] {'\\', 'r'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};
        private readonly ReadOnlyMemory<char> _ignorePattern = new[] { _pattern};
        
        public char GetPattern() => _pattern;

        public ReadOnlyMemory<char> GetReplacement(CarriageReturnBehavior behavior)
        {
            var replacement = behavior switch
            {
                CarriageReturnBehavior.Strip => _stripPattern,
                CarriageReturnBehavior.Escape => _replacePattern,
                CarriageReturnBehavior.Ignore => _ignorePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(CarriageReturnBehavior)}", nameof(behavior))
            };

            return replacement;
        }
    }
}