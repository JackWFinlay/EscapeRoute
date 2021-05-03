using System;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine.EscapeHandlers
{
    public class TabEscapeHandler: IEscapeRouteEscapeHandler<TabBehavior>
    {
        private const char _pattern = '\t';
        private readonly ReadOnlyMemory<char> _replacePattern = new[] {'\\', 't'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};

        public char GetPattern() => _pattern;

        public ReadOnlyMemory<char> GetReplacement(TabBehavior behavior)
        {
            var escaped = behavior switch
            {
                TabBehavior.Escape => _replacePattern,
                TabBehavior.Strip => _stripPattern,
                _ => throw new ArgumentException($"Not a valid {nameof(TabBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}