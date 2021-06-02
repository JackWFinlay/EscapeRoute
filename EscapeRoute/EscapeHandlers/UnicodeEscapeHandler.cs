using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.EscapeHandlers
{
    public class UnicodeEscapeHandler: IEscapeRouteEscapeFunctionHandler<UnicodeBehavior>
    {
        private static readonly ReadOnlyMemory<char> _stripPattern = new char[] {};

        public Func<ReadOnlyMemory<char>, ReadOnlyMemory<char>> GetReplacement(UnicodeBehavior behavior)
        {
            var escaped = behavior switch
            {
                UnicodeBehavior.Escape => 
                    new Func<ReadOnlyMemory<char>, ReadOnlyMemory<char>>(c => ($"\\u{(int)c.Span[0]:x4}").AsMemory()),
                // Strip out non-ASCII characters.
                UnicodeBehavior.Strip => c => _stripPattern,
                UnicodeBehavior.Ignore => c => c,
                _ => throw new ArgumentException($"Not a valid {nameof(UnicodeBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}