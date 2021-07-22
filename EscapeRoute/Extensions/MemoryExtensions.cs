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

        public static int IndexOfTokenStart(this ReadOnlyMemory<char> span, ReadOnlyMemory<char> token)
        {
            for (var i = 0; i < span.Length; i++)
            {
                for (var j = 0; j < token.Length; j++)
                {
                    if (span.Span[i + j] != token.Span[j])
                    {
                        break;
                    }

                    // Continue if not last item in token.
                    if (j != (token.Length - 1))
                    {
                        continue;
                    }

                    return i;
                }
            }

            return -1;
        }
    }
}