using System;
using System.Collections.Generic;

namespace EscapeRoute
{
    public class ReadOnlyMemoryComparer : IEqualityComparer<ReadOnlyMemory<char>>
    {
        public bool Equals(ReadOnlyMemory<char> x, ReadOnlyMemory<char> y)
        {
            for (var i = 0; i < x.Length; i++)
            {
                if (x.Span[i] != y.Span[i])
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(ReadOnlyMemory<char> obj)
        {
            var hashCode = 0;
            
            for (var i = 0; i < obj.Length; i++)
            {
                hashCode ^= obj.Span[i].GetHashCode();
            }

            return hashCode;
        }
    }
}