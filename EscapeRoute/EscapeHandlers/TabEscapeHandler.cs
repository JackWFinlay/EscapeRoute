using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.EscapeHandlers
{
    public class TabEscapeHandler: IEscapeRouteEscapeHandler<TabBehavior>
    {
        private const char _pattern = '\t';
        private readonly ReadOnlyMemory<char> _replacePattern = new[] {'\\', 't'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};
        private readonly ReadOnlyMemory<char> _ignorePattern = new[] { _pattern };

        public char GetPattern() => _pattern;

        public ReadOnlyMemory<char> GetReplacement(TabBehavior behavior)
        {
            var escaped = behavior switch
            {
                TabBehavior.Escape => _replacePattern,
                TabBehavior.Strip => _stripPattern,
                TabBehavior.Ignore => _ignorePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(TabBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}