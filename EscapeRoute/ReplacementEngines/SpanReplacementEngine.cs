using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Interfaces;
using EscapeRoute.Extensions;

namespace EscapeRoute.ReplacementEngines
{
    public class SpanReplacementEngine : IReplacementEngine
    {
        public Task<ReadOnlyMemory<char>> ReplaceAsync(ReadOnlyMemory<char> raw, IEscapeRouteConfiguration config)
        {
            if (raw.IsEmpty)
            {
                return Task.FromResult(raw);
            }

            var pattern = GetPattern(config);

            var matchIndexes = GetMatches(raw, pattern).ToList();

            if (!matchIndexes.Any())
            {
                return Task.FromResult(raw);
            }

            var memoryList = CreateEscapedTextList(raw, matchIndexes, config);

            if (!memoryList.Any())
            {
                return Task.FromResult(raw);
            }

            var memoryListTotalLength = memoryList.Sum(m => m.Length);

            Memory<char> combinedMemory = new char[memoryListTotalLength];
            
            foreach (var mem in memoryList)
            {
                mem.CopyTo(combinedMemory);
            }

            return Task.FromResult((ReadOnlyMemory<char>)combinedMemory);
        }

        private static Dictionary<char, ReadOnlyMemory<char>> CreateReplacementMap(IEscapeRouteConfiguration config)
        {
            return new Dictionary<char, ReadOnlyMemory<char>>()
            {
                {config.BackslashEscapeHandler.GetPattern(),
                    config.BackslashEscapeHandler.GetReplacement(config.BackslashBehavior)},
                {config.BackspaceEscapeHandler.GetPattern(),
                    config.BackspaceEscapeHandler.GetReplacement(config.BackspaceBehavior)},
                {config.DoubleQuoteEscapeHandler.GetPattern(),
                    config.DoubleQuoteEscapeHandler.GetReplacement(config.DoubleQuoteBehavior)},
                {config.FormFeedEscapeHandler.GetPattern(),
                    config.FormFeedEscapeHandler.GetReplacement(config.FormFeedBehavior)},
                {config.SingleQuoteEscapeHandler.GetPattern(),
                    config.SingleQuoteEscapeHandler.GetReplacement(config.SingleQuoteBehavior)},
                {config.TabEscapeHandler.GetPattern(),
                    config.TabEscapeHandler.GetReplacement(config.TabBehavior)},
            };
        }

        private ReadOnlyMemory<char> GetPattern(IEscapeRouteConfiguration config)
        {
            ReadOnlyMemory<char> pattern = new char[]
            {
                config.BackslashEscapeHandler.GetPattern(),
                config.BackspaceEscapeHandler.GetPattern(),
                config.DoubleQuoteEscapeHandler.GetPattern(),
                config.FormFeedEscapeHandler.GetPattern(),
                config.SingleQuoteEscapeHandler.GetPattern(),
                config.TabEscapeHandler.GetPattern(),
            };

            return pattern;
        }

        private static IEnumerable<int> GetMatches(ReadOnlyMemory<char> memory, ReadOnlyMemory<char> pattern)
        {

            while (memory.Length > 0)
            {
                var index = memory.Span.IndexOfAny(pattern.Span);
                
                if (index == -1)
                {
                    yield return memory.Length;
                    yield break;
                }
                else
                {
                    yield return index;

                    memory = memory.Slice(index + 1);
                }
            }
        }

        private List<ReadOnlyMemory<char>> CreateEscapedTextList(ReadOnlyMemory<char> raw, 
            IList<int> matchIndexes, IEscapeRouteConfiguration config)
        {
            var memoryList = new List<ReadOnlyMemory<char>>();

            var replacementMap = CreateReplacementMap(config);
            
            if (replacementMap.TryGetValue(raw.Slice(matchIndexes[0], 1).ToArray()[0], out var firstMatch))
            {
                memoryList.Add(firstMatch);
            }
            else
            {
                memoryList.Add(raw.Slice(0, matchIndexes[0]));
            }

            var prevIndex = matchIndexes[0] + 1;
            
            foreach (var matchIndex in matchIndexes.Skip(1))
            {
                if (prevIndex > raw.Length)
                {
                    break;
                }

                var memorySlice = raw.Slice(prevIndex + matchIndex, (matchIndex - prevIndex) + 1);
                var patternMatched = raw.Slice(prevIndex + matchIndex, 1).ToArray()[0];
                replacementMap.TryGetValue(patternMatched, out var replacement);
                
                memoryList.Add(replacement);
                memoryList.Add(memorySlice);
                prevIndex += (matchIndex + 1);

            }

            return memoryList;
        }
    }
}