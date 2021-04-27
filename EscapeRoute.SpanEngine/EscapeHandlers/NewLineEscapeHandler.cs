using System;
using EscapeRoute.Abstractions.Enums;
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

        public Func<char, ReadOnlyMemory<char>> GetReplacement(NewLineType behavior)
        {
            var escaped = behavior switch
            {
                NewLineType.Space => new Func<char, ReadOnlyMemory<char>>(c => _replaceSpacePattern),
                NewLineType.Escape => c => _replacePattern,
                NewLineType.Strip => c => _stripPattern,
                NewLineType.Windows => c => _replacePattern,
                NewLineType.Unix => c => _replacePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(NewLineType)}", nameof(behavior))
            };

            return escaped;
        }
    }
}