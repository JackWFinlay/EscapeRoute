using System;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine.EscapeHandlers
{
    public class NewLineEscapeHandler: IEscapeRouteEscapeHandler<NewLineBehavior>
    {
        private const char _pattern = '\n';
        private readonly ReadOnlyMemory<char> _replaceSpacePattern = new[] {' '};
        private readonly ReadOnlyMemory<char> _replacePattern = new[] {'\\', 'n'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};
        private readonly ReadOnlyMemory<char> _ignorePattern = new[] { _pattern };

        public char GetPattern() => _pattern;

        public ReadOnlyMemory<char> GetReplacement(NewLineBehavior behavior)
        {
            var escaped = behavior switch
            {
                NewLineBehavior.Space => _replaceSpacePattern,
                NewLineBehavior.Escape => _replacePattern,
                NewLineBehavior.Strip => _stripPattern,
                NewLineBehavior.Ignore => _ignorePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(NewLineBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}