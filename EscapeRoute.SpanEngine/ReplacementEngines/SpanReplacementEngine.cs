using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;
using EscapeRoute.SpanEngine.Extensions;

namespace EscapeRoute.SpanEngine.ReplacementEngines
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

            var matchIndexes = GetMatches(raw, pattern);

            if (!matchIndexes.Any())
            {
                return Task.FromResult(raw);
            }

            var memoryList = CreateEscapedTextList(raw, matchIndexes, config);

            if (!memoryList.Any())
            {
                return Task.FromResult(raw);
            }

            var combinedMemory = CombinedMemory(memoryList);

            return Task.FromResult((ReadOnlyMemory<char>)combinedMemory);
        }

        private static Memory<char> CombinedMemory(List<ReadOnlyMemory<char>> memoryList)
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

        private static Dictionary<char, Func<char, ReadOnlyMemory<char>>> CreateReplacementMap(IEscapeRouteConfiguration config)
        {
            return new Dictionary<char, Func<char, ReadOnlyMemory<char>>>()
            {
                {config.BackspaceEscapeHandler.GetPattern(),
                    config.BackspaceEscapeHandler.GetReplacement(config.BackspaceBehavior)},
                {config.BackslashEscapeHandler.GetPattern(),
                    config.BackslashEscapeHandler.GetReplacement(config.BackslashBehavior)},
                {config.DoubleQuoteEscapeHandler.GetPattern(),
                    config.DoubleQuoteEscapeHandler.GetReplacement(config.DoubleQuoteBehavior)},
                {config.FormFeedEscapeHandler.GetPattern(),
                    config.FormFeedEscapeHandler.GetReplacement(config.FormFeedBehavior)},
                {config.SingleQuoteEscapeHandler.GetPattern(),
                    config.SingleQuoteEscapeHandler.GetReplacement(config.SingleQuoteBehavior)},
                {config.TabEscapeHandler.GetPattern(),
                    config.TabEscapeHandler.GetReplacement(config.TabBehavior)},
                {config.NewLineEscapeHandler.GetPattern(),
                    config.NewLineEscapeHandler.GetReplacement(config.NewLineType)},
                {config.CarriageReturnEscapeHandler.GetPattern(),
                    config.CarriageReturnEscapeHandler.GetReplacement(config.CarriageReturnBehavior)}
            };
        }

        private HashSet<char> GetPattern(IEscapeRouteConfiguration config)
        {
            var pattern = new HashSet<char>()
            {
                config.BackspaceEscapeHandler.GetPattern(),
                config.BackslashEscapeHandler.GetPattern(),
                config.CarriageReturnEscapeHandler.GetPattern(),
                config.DoubleQuoteEscapeHandler.GetPattern(),
                config.FormFeedEscapeHandler.GetPattern(),
                config.NewLineEscapeHandler.GetPattern(),
                config.SingleQuoteEscapeHandler.GetPattern(),
                config.TabEscapeHandler.GetPattern()
            };

            return pattern;
        }

        private static IEnumerable<int> GetMatches(ReadOnlyMemory<char> memory, HashSet<char> pattern)
        {

            while (memory.Length > 0)
            {
                var index = memory.IndexOfAnyInPattern(pattern);
                
                if (index == -1)
                {
                    yield return memory.Length;
                    yield break;
                }
                
                yield return index;
                memory = memory.Slice(index + 1);

            }
        }

        private static List<ReadOnlyMemory<char>> CreateEscapedTextList(ReadOnlyMemory<char> raw, 
            IEnumerable<int> matchIndexes, IEscapeRouteConfiguration config)
        {
            var memoryList = new List<ReadOnlyMemory<char>>();

            var replacementMap = CreateReplacementMap(config);
            
            var prevIndex = 0;

            foreach (var matchIndex in matchIndexes)
            {
                if (prevIndex > raw.Length)
                {
                    break;
                }

                if (matchIndex > 0)
                {
                    var memorySlice = raw.Slice(prevIndex, matchIndex);
                    if (!memorySlice.IsEmpty)
                    {
                        memoryList.Add(memorySlice);
                    }
                }

                if (prevIndex + matchIndex >= raw.Length)
                {
                    break;
                }

                var patternMatched = raw.Span.Slice(prevIndex + matchIndex, 1)[0];
                if (replacementMap.TryGetValue(patternMatched, out var replace))
                {
                    var replacement = replace(patternMatched);
                    
                    if(!replacement.IsEmpty)
                    {
                        memoryList.Add(replacement);
                    }
                } else if (patternMatched > 127)
                {
                    var replacer = config.UnicodeEscapeHandler
                                                                   .GetReplacement(config.UnicodeBehavior);
                    
                    var replacement = replacer(patternMatched);
                    
                    if(!replacement.IsEmpty)
                    {
                        memoryList.Add(replacement);
                    }
                }

                if (prevIndex == 0)
                {
                    prevIndex = matchIndex + 1;
                }
                else
                {
                    prevIndex += (matchIndex + 1);    
                }
            }

            return memoryList;
        }
    }
}