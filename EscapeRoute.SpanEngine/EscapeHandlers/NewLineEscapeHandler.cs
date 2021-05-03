using System;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine.EscapeHandlers
{
    public class NewLineEscapeHandler: IEscapeRouteEscapeHandler<NewLineType>
    {
        private const char _pattern = '\n';
        private readonly ReadOnlyMemory<char> _replaceSpacePattern = new[] {' '};
        private readonly ReadOnlyMemory<char> _replacePattern = new[] {'\\', 'n'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};

        public char GetPattern() => _pattern;

        public ReadOnlyMemory<char> GetReplacement(NewLineType behavior)
        {
            var escaped = behavior switch
            {
                NewLineType.Space => _replaceSpacePattern,
                NewLineType.Escape => _replacePattern,
                NewLineType.Strip => _stripPattern,
                _ => throw new ArgumentException($"Not a valid {nameof(NewLineType)}", nameof(behavior))
            };

            return escaped;
        }
    }
}