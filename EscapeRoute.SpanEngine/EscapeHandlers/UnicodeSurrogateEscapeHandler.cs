using System;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine.EscapeHandlers
{
    public class UnicodeSurrogateEscapeHandler : IEscapeRouteEscapeFunctionHandler<UnicodeSurrogateBehavior>
    {
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] { };
        
        public Func<ReadOnlyMemory<char>, ReadOnlyMemory<char>> GetReplacement(UnicodeSurrogateBehavior behavior)
        {
            var result = behavior switch
            {
                UnicodeSurrogateBehavior.Ignore => new Func<ReadOnlyMemory<char>, ReadOnlyMemory<char>>(c => c),
                UnicodeSurrogateBehavior.Strip => c => _stripPattern,
                UnicodeSurrogateBehavior.Escape => c => $"\\u{(int)c.Span[0]:x4}\\u{(int)c.Span[1]:x4}".AsMemory(),
                _ => throw new ArgumentException($"Not a valid {nameof(UnicodeSurrogateBehavior)}", nameof(behavior))
            };

            return result;
        }
    }
}