using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.ReplacementEngines
{
    public class SpanReplacementEngine : IReplacementEngine
    {
        public Task<string> ReplaceAsync(string raw, string pattern, string replacement)
        {
            if (string.IsNullOrWhiteSpace(pattern))
            {
                return Task.FromResult(raw);
            }

            var patternChar = pattern[0];
            
            var rawMemory = raw.AsMemory();
            
            var matchIndexes = GetMatches(raw, patternChar);

            var memoryList = new List<ReadOnlyMemory<char>>();
            var prevIndex = 0;
            
            foreach (var matchIndex in matchIndexes)
            {
                var memorySlice = rawMemory.Slice(prevIndex, matchIndex);
                prevIndex = matchIndex;
                memoryList.Add(memorySlice);
            }

            var combinedMemory = MemoryConcat(memoryList, replacement);

            var result = string.Create(combinedMemory.Length, combinedMemory, (chars, c) =>
            {
                for (var i = 0; i < c.Length; i++)
                {
                    //chars[i] = c[i];
                }
            });

            return Task.FromResult(result);
        }

        private ReadOnlyMemory<char> MemoryConcat(List<ReadOnlyMemory<char>> memoryList, string replacement)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<int> GetMatches(string raw, char pattern)
        {
            var memory = raw.AsMemory();

            while (memory.Length > 0)
            {
                var index = memory.Span.IndexOf(pattern);
                
                if (index == -1)
                {
                    yield break;
                }
                
                yield return index;
                
                memory = memory.Slice(index);
            }
        }
    }
}