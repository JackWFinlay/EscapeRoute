using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EscapeRoute.Extensions
{
    public static class ListExtensions
    {
        public static ReadOnlyMemory<char> Join(this IList<ReadOnlyMemory<char>> memoryList, ReadOnlyMemory<char> separator)
        {
            var combinedMemoryLength = memoryList.Sum(m => m.Length);

            Memory<char> result = new char[combinedMemoryLength];
            
            if (combinedMemoryLength <= 0)
            {
                return result;
            }
            
            memoryList[0].CopyTo(result);
            var prevLength = memoryList[0].Length;

            foreach (var m in memoryList.Skip(1))
            {
                if (prevLength > result.Length)
                {
                    break;
                }

                separator.CopyTo(result.Slice(prevLength));
                m.CopyTo(result.Slice(prevLength + separator.Length));
                prevLength += (m.Length + separator.Length);
            }

            ReadOnlyMemory<char> output = result;

            return output;
        }
    }
}