using System;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.EscapeHandlers
{
    public class SingleQuoteEscapeHandler: IEscapeRouteEscapeHandler<SingleQuoteBehavior>
    {
        private const char _pattern = '\'';
        private readonly ReadOnlyMemory<char> _replaceSinglePattern = new char[] {'\\', '\''};
        private readonly ReadOnlyMemory<char> _replaceDoublePattern = new char[] {'\\','"'};

        public char GetPattern()
        {
            return _pattern;
        }
        
        public ReadOnlyMemory<char> GetReplacement(SingleQuoteBehavior behavior)
        {
            var escaped = behavior switch
            {
                SingleQuoteBehavior.Single => _replaceSinglePattern,
                SingleQuoteBehavior.Double => _replaceDoublePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(SingleQuoteBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}