using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine.EscapeHandlers
{
    public class UnicodeEscapeHandler: IEscapeRouteEscapeHandler<UnicodeBehavior>
    {
        private const char _pattern = '\u0000';
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};

        public char GetPattern() => _pattern;

        public Func<char, ReadOnlyMemory<char>> GetReplacement(UnicodeBehavior behavior)
        {
            var escaped = behavior switch
            {
                UnicodeBehavior.Escape => 
                    new Func<char, ReadOnlyMemory<char>>(c => ($"\\u{(int)c:x4}").AsMemory()),
                // Strip out non-ASCII characters.
                UnicodeBehavior.Strip => c => _stripPattern,
                _ => throw new ArgumentException($"Not a valid {nameof(UnicodeBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}