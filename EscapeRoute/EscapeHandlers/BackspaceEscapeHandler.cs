using System;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.EscapeHandlers
{
    public class BackspaceEscapeHandler : IEscapeRouteEscapeHandler<BackspaceBehavior>
    {
        private const char _pattern = '\b';
        private readonly ReadOnlyMemory<char> _replacePattern = new char[] {'\\', 'b'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};

        public char GetPattern()
        {
            return _pattern;
        }
        
        public ReadOnlyMemory<char> GetReplacement(BackspaceBehavior behavior)
        {
            var escaped = behavior switch
            {
                // Replace backspace characters with \b.
                BackspaceBehavior.Escape => _replacePattern,
                BackspaceBehavior.Strip => _stripPattern,
                _ => throw new ArgumentException($"Not a valid {nameof(BackspaceBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}