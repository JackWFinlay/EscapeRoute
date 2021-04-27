using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine.EscapeHandlers
{
    public class DoubleQuoteEscapeHandler: IEscapeRouteEscapeHandler<DoubleQuoteBehavior>
    {
        private const char _pattern = '"';
        private readonly ReadOnlyMemory<char> _replaceSinglePattern = new[] {'\\', '\''};
        private readonly ReadOnlyMemory<char> _replaceDoublePattern = new[] {'\\','"'};

        public char GetPattern() => _pattern;
        
        public Func<char, ReadOnlyMemory<char>> GetReplacement(DoubleQuoteBehavior behavior)
        {
            var escaped = behavior switch
            {
                DoubleQuoteBehavior.Single => new Func<char, ReadOnlyMemory<char>>(c => _replaceSinglePattern),
                DoubleQuoteBehavior.Double => c => _replaceDoublePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(DoubleQuoteBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}