using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.EscapeHandlers
{
    public class BackslashEscapeHandler : IEscapeRouteEscapeHandler<BackslashBehavior>
    {
        private const char _pattern = '\\';
        private readonly ReadOnlyMemory<char> _replacePattern = new[] {'\\', '\\'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};
        private readonly ReadOnlyMemory<char> _ignorePattern = new[] { _pattern };

        public char GetPattern() => _pattern;
        
        public ReadOnlyMemory<char> GetReplacement(BackslashBehavior behavior)
        {
            var escaped = behavior switch
            {
                // Replace backspace characters with \b.
                BackslashBehavior.Escape => _replacePattern,
                BackslashBehavior.Strip => _stripPattern,
                BackslashBehavior.Ignore => _ignorePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(BackslashBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}