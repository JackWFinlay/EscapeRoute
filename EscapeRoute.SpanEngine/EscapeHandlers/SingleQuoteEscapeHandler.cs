using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine.EscapeHandlers
{
    public class SingleQuoteEscapeHandler: IEscapeRouteEscapeHandler<SingleQuoteBehavior>
    {
        private const char _pattern = '\'';
        private readonly ReadOnlyMemory<char> _replaceSinglePattern = new[] {'\\', '\''};
        private readonly ReadOnlyMemory<char> _replaceDoublePattern = new[] {'\\','"'};

        public char GetPattern() => _pattern;

        public Func<char, ReadOnlyMemory<char>> GetReplacement(SingleQuoteBehavior behavior)
        {
            var escaped = behavior switch
            {
                SingleQuoteBehavior.Single => 
                    new Func<char, ReadOnlyMemory<char>>(c => _replaceSinglePattern),
                SingleQuoteBehavior.Double => c => _replaceDoublePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(SingleQuoteBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}