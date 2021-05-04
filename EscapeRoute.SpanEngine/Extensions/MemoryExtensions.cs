using System;
using System.Collections.Generic;

namespace EscapeRoute.SpanEngine.Extensions
{
    public static class MemoryExtensions
    {
        public static int IndexOfAnyInPattern(this ReadOnlyMemory<char> span, HashSet<char> pattern)
        {
            for (var i = 0; i < span.Length; i++)
            {
                var slice = span.Slice(i, 1);
                
                if (pattern.Contains(slice.Span[0]) || (slice.Span[0] > 127 && slice.Span[0] <= 0xdfff)) 
                {
                    return i;
                }
            }

            return -1;
        }
    }
}