using System;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.EscapeHandlers
{
    public class DoubleQuoteEscapeHandler: IEscapeRouteEscapeHandler<DoubleQuoteBehavior>
    {
        private const char _pattern = '"';
        private readonly ReadOnlyMemory<char> _replaceSinglePattern = new char[] {'\\', '\''};
        private readonly ReadOnlyMemory<char> _replaceDoublePattern = new char[] {'\\','"'};

        public char GetPattern()
        {
            return _pattern;
        }
        
        public ReadOnlyMemory<char> GetReplacement(DoubleQuoteBehavior behavior)
        {
            var escaped = behavior switch
            {
                DoubleQuoteBehavior.Single => _replaceSinglePattern,
                DoubleQuoteBehavior.Double => _replaceDoublePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(DoubleQuoteBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}