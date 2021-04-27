using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine.EscapeHandlers
{
    public class TabEscapeHandler: IEscapeRouteEscapeHandler<TabBehavior>
    {
        private const char _pattern = '\t';
        private readonly ReadOnlyMemory<char> _replacePattern = new[] {'\\', 't'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};

        public char GetPattern() => _pattern;

        public Func<char, ReadOnlyMemory<char>> GetReplacement(TabBehavior behavior)
        {
            var escaped = behavior switch
            {
                TabBehavior.Escape => 
                    new Func<char, ReadOnlyMemory<char>>(c => _replacePattern),
                TabBehavior.Strip => c => _stripPattern,
                _ => throw new ArgumentException($"Not a valid {nameof(TabBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}