using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.EscapeHandlers
{
    public class BackslashEscapeHandler : IEscapeRouteEscapeHandler<BackslashBehavior>
    {
        private const char _pattern = '\\';
        private readonly ReadOnlyMemory<char> _replacePattern = new char[] {'\\', '\\'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};
        
        public char GetPattern()
        {
            return _pattern;
        }
        
        public ReadOnlyMemory<char> GetReplacement(BackslashBehavior behavior)
        {
            var escaped = behavior switch
            {
                // Replace backspace characters with \b.
                BackslashBehavior.Escape => _replacePattern,
                BackslashBehavior.Strip => _stripPattern,
                _ => throw new ArgumentException($"Not a valid {nameof(BackslashBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}