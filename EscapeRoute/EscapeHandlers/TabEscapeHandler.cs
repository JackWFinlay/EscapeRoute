using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.EscapeHandlers
{
    public class TabEscapeHandler: IEscapeRouteEscapeHandler<TabBehavior>
    {
        private const char _pattern = '\t';
        private readonly ReadOnlyMemory<char> _replacePattern = new char[] {'\\', 't'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};

        public char GetPattern()
        {
            return _pattern;
        }

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