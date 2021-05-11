using System;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine.EscapeHandlers
{
    public class BackspaceEscapeHandler : IEscapeRouteEscapeHandler<BackspaceBehavior>
    {
        private const char _pattern = '\b';
        private static readonly ReadOnlyMemory<char> _replacePattern = new[] {'\\', 'b'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};
        private readonly ReadOnlyMemory<char> _ignorePattern = new[] { _pattern };


        public char GetPattern() => _pattern;
        
        public ReadOnlyMemory<char> GetReplacement(BackspaceBehavior behavior)
        {
            var escaped = behavior switch
            {
                // Replace backspace characters with \b.
                BackspaceBehavior.Escape => _replacePattern,
                BackspaceBehavior.Strip => _stripPattern,
                BackspaceBehavior.Ignore => _ignorePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(BackspaceBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}