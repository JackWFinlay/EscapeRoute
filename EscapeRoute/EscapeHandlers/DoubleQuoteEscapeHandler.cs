using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.EscapeHandlers
{
    public class DoubleQuoteEscapeHandler: IEscapeRouteEscapeHandler<DoubleQuoteBehavior>
    {
        private const char _pattern = '"';
        private readonly ReadOnlyMemory<char> _replaceSinglePattern = new[] {'\\', '\''};
        private readonly ReadOnlyMemory<char> _replaceDoublePattern = new[] {'\\','"'};
        private readonly ReadOnlyMemory<char> _ignorePattern = new[] { _pattern };

        public char GetPattern() => _pattern;
        
        public ReadOnlyMemory<char> GetReplacement(DoubleQuoteBehavior behavior)
        {
            var escaped = behavior switch
            {
                DoubleQuoteBehavior.Single =>  _replaceSinglePattern,
                DoubleQuoteBehavior.Double => _replaceDoublePattern,
                DoubleQuoteBehavior.Ignore => _ignorePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(DoubleQuoteBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}