using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Exceptions;
using EscapeRoute.Abstractions.Interfaces;
using EscapeRoute.Extensions;

namespace EscapeRoute.ReplacementEngines
{
    public class SpanReplacementEngine : IReplacementEngine
    {
        private readonly IEscapeRouteConfiguration _config;
        private readonly HashSet<char> _pattern;
        private readonly Dictionary<char, ReadOnlyMemory<char>> _replacementMap;
        private readonly Func<ReadOnlyMemory<char>, ReadOnlyMemory<char>> _unicodeReplacer;
        private readonly Func<ReadOnlyMemory<char>, ReadOnlyMemory<char>> _unicodeSurrogateReplacer;

        public SpanReplacementEngine(IEscapeRouteConfiguration config)
        {
            _config = config;
            _pattern = GetPattern(config);
            _replacementMap = CreateReplacementMap(config);
            _unicodeReplacer = _config.UnicodeEscapeHandler
                                      .GetReplacement(_config.UnicodeBehavior);
            _unicodeSurrogateReplacer = _config.UnicodeSurrogateEscapeHandler
                                               .GetReplacement(_config.UnicodeSurrogateBehavior);
        }

        public Task<ReadOnlyMemory<char>> ReplaceAsync(ReadOnlyMemory<char> raw)
        {
            if (raw.IsEmpty)
            {
                return Task.FromResult(raw);
            }

            var matchIndexes = GetMatches(raw).ToList();

            if (!matchIndexes.Any())
            {
                return Task.FromResult(raw);
            }

            var memoryList = new List<ReadOnlyMemory<char>>();
            try
            {
                memoryList.AddRange(CreateEscapedTextList(raw, matchIndexes));
            }
            catch (Exception e)
            {
                throw new EscapeRouteParseException($"Cannot parse the string '{raw.ToString()}'", e);
            }

            if (!memoryList.Any())
            {
                return Task.FromResult(raw);
            }

            var combinedMemory = memoryList.CombineMemory();

            return Task.FromResult((ReadOnlyMemory<char>)combinedMemory);
        }

        private IEnumerable<int> GetMatches(ReadOnlyMemory<char> memory)
        {

            while (memory.Length > 0)
            {
                var index = memory.IndexOfAnyInPattern(_pattern);
                
                if (index == -1)
                {
                    yield return memory.Length;
                    yield break;
                }
                
                yield return index;

                // Skip the second character in the surrogate pair.
                if (memory.Span[index] >= 0xd800 && memory.Span[index] <= 0xdfff)
                {
                    index++;
                }

                memory = memory.Slice(index + 1);

            }
        }

        private List<ReadOnlyMemory<char>> CreateEscapedTextList(ReadOnlyMemory<char> raw, 
            IEnumerable<int> matchIndexes)
        {
            var memoryList = new List<ReadOnlyMemory<char>>();
            
            var prevIndex = 0;

            foreach (var matchIndex in matchIndexes)
            {
                if (prevIndex >= raw.Length || matchIndex >= raw.Length)
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

                var patternMatched = raw.Span.Slice(prevIndex + matchIndex, 1);

                
                // Surrogate Pair First Byte
                if (patternMatched[0] >= 0xd800 && patternMatched[0] <= 0xdfff)
                {
                    var mem = raw.Slice(prevIndex + matchIndex, 2);
                    
                    HandleUnicodeSurrogate(mem, 0, memoryList);
                    HandleUnicodeSurrogate(mem, 1, memoryList);

                    // Increment as we took two chars.
                    prevIndex++;
                }
                else if (_replacementMap.TryGetValue(patternMatched[0], out var replacement))
                {
                    if (!replacement.IsEmpty)
                    {
                        memoryList.Add(replacement);
                    }
                }
                // Standard Unicode Character
                else if (patternMatched[0] > 127)
                {
                    var mem = raw.Slice(prevIndex + matchIndex, 1); 
                    
                    var unicodeReplacement = _unicodeReplacer(mem);
                    
                    if(!unicodeReplacement.IsEmpty)
                    {
                        memoryList.Add(unicodeReplacement);
                    }
                    
                    _replacementMap.Add(patternMatched[0], unicodeReplacement);
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

        private void HandleUnicodeSurrogate(ReadOnlyMemory<char> mem, int index, IList<ReadOnlyMemory<char>> memoryList)
        {
            if (_replacementMap.TryGetValue(mem.Span[index], out var replacement))
            {
                memoryList.Add(replacement);
            }
            else
            {
                var slice = mem.Slice(index, 1);
                var unicodeSurrogateReplacement = _unicodeSurrogateReplacer(slice);

                if (!unicodeSurrogateReplacement.IsEmpty)
                {
                    memoryList.Add(unicodeSurrogateReplacement);
                }
                
                _replacementMap.Add(mem.Span[index], replacement);
            }
        }

        private static HashSet<char> GetPattern(IEscapeRouteConfiguration config)
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
                config.TabEscapeHandler.GetPattern(),
                config.UnicodeNullEscapeHandler.GetPattern()
            };

            return pattern;
        }
        
        private static Dictionary<char, ReadOnlyMemory<char>> CreateReplacementMap(IEscapeRouteConfiguration config)
        {
            return new Dictionary<char, ReadOnlyMemory<char>>()
            {
                {
                    config.BackspaceEscapeHandler.GetPattern(),
                    config.BackspaceEscapeHandler.GetReplacement(config.BackspaceBehavior)
                    
                },
                {
                    config.BackslashEscapeHandler.GetPattern(),
                    config.BackslashEscapeHandler.GetReplacement(config.BackslashBehavior)
                    
                },
                {
                    config.DoubleQuoteEscapeHandler.GetPattern(),
                    config.DoubleQuoteEscapeHandler.GetReplacement(config.DoubleQuoteBehavior)
                    
                },
                {
                    config.FormFeedEscapeHandler.GetPattern(),
                    config.FormFeedEscapeHandler.GetReplacement(config.FormFeedBehavior)
                    
                },
                {
                    config.SingleQuoteEscapeHandler.GetPattern(),
                    config.SingleQuoteEscapeHandler.GetReplacement(config.SingleQuoteBehavior)
                    
                },
                {
                    config.TabEscapeHandler.GetPattern(),
                    config.TabEscapeHandler.GetReplacement(config.TabBehavior)
                    
                },
                {
                    config.NewLineEscapeHandler.GetPattern(),
                    config.NewLineEscapeHandler.GetReplacement(config.NewLineBehavior)
                    
                },
                {
                    config.CarriageReturnEscapeHandler.GetPattern(),
                    config.CarriageReturnEscapeHandler.GetReplacement(config.CarriageReturnBehavior)
                    
                },
                {
                    config.UnicodeNullEscapeHandler.GetPattern(),
                    config.UnicodeNullEscapeHandler.GetReplacement(config.UnicodeNullBehavior)
                }
            };
        }
    }
}