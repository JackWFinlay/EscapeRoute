using System;
using System.Collections.Generic;
using System.Linq;

namespace EscapeRoute.Extensions
{
    public static class MemoryExtensions
    {
        public static int IndexOfAnyInPattern(this ReadOnlyMemory<char> span, HashSet<char> pattern)
        {
            for (var i = 0; i < span.Length; i++)
            {
                var slice = span.Slice(i, 1);
                
                if (pattern.Contains(slice.Span[0]) || slice.Span[0] > 127) 
                {
                    return i;
                }
            }

            return -1;
        }
        
        public static Memory<char> CombineMemory(this IReadOnlyCollection<ReadOnlyMemory<char>> memoryList)
        {
            var memoryListTotalLength = memoryList.Sum(m => m.Length);

            Memory<char> combinedMemory = new char[memoryListTotalLength];

            var prevLength = 0;
            foreach (var mem in memoryList)
            {
                if (prevLength > combinedMemory.Length)
                {
                    break;
                }

                mem.CopyTo(combinedMemory.Slice(prevLength));
                prevLength += mem.Length;
            }

            return combinedMemory;
        }

        public static int IndexOfTokenStart(this ReadOnlyMemory<char> span, ReadOnlyMemory<char> tokenStart)
        {
            var index = IndexOfToken(span, tokenStart);

            return index;
        }

        public static int IndexOfTokenEnd(this ReadOnlyMemory<char> span, 
            ReadOnlyMemory<char> tokenStart,
            ReadOnlyMemory<char> tokenEnd)
        {
            var index = IndexOfToken(span, tokenEnd, true);

            return index;
        }

        private static int IndexOfToken(ReadOnlyMemory<char> span, ReadOnlyMemory<char> token, bool getEnd = false)
        {
            for (var i = 0; i < span.Length; i++)
            {
                var slice = span.Slice(i, 1);

                for (var j = 0; j < token.Length; j++)
                {
                    if (span.Length <= (i + j))
                    {
                        break;
                    }

                    if (slice.Span[0] != token.Slice(j, 1).Span[0])
                    {
                        break;
                    }

                    if (j == (token.Length - 1))
                    {
                        return getEnd
                               ? (i + token.Length)
                               : i;
                    }

                    slice = span.Slice(i + j, 1);
                }
            }

            return -1;
        }
    }
}