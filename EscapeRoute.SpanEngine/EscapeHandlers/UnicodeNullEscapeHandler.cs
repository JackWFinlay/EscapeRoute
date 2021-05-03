using System;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine.EscapeHandlers
{
    public class UnicodeNullEscapeHandler : IEscapeRouteEscapeHandler<UnicodeNullBehavior>
    {
        private const char _pattern = '\0';
        private readonly ReadOnlyMemory<char> _escapePattern = new[] {'\\', '0'};
        private readonly ReadOnlyMemory<char> _escapeHexPattern = new[] {'\\', 'u', '0', '0', '0', '0'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};

        public char GetPattern() => _pattern;

        public ReadOnlyMemory<char> GetReplacement(UnicodeNullBehavior behavior)
        {
            var escaped = behavior switch
            {
                UnicodeNullBehavior.Strip => _stripPattern,
                UnicodeNullBehavior.Escape => _escapePattern,
                UnicodeNullBehavior.EscapeHex => _escapeHexPattern,
                _ => throw new ArgumentException($"Not a valid {nameof(TabBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}