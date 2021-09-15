using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.DataStructures;
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

        public Task<string> ReplaceAsync(ReadOnlyMemory<char> raw)
        {
            if (raw.IsEmpty)
            {
                return Task.FromResult(raw.ToString());
            }

            var matchIndexes = GetMatches(raw).ToList();

            if (!matchIndexes.Any())
            {
                return Task.FromResult(raw.ToString());
            }

            string output = null;
            try
            {
                output = CreateEscapedTextList(raw, matchIndexes);
            }
            catch (Exception e)
            {
                throw new EscapeRouteParseException($"Cannot parse the string '{raw.ToString()}'", e);
            }

            if (string.IsNullOrWhiteSpace(output))
            {
                return Task.FromResult(raw.ToString());
            }

            return Task.FromResult(output);
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

        private string CreateEscapedTextList(ReadOnlyMemory<char> raw, 
            IEnumerable<int> matchIndexes)
        {
            var memoryList = new StringValueList(raw.Length);

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
                        memoryList.Add(memorySlice.Span);
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
                    
                    memoryList.Add(HandleUnicodeSurrogate(mem, 0).Span);
                    memoryList.Add(HandleUnicodeSurrogate(mem, 1).Span);

                    // Increment as we took two chars.
                    prevIndex++;
                }
                else if (_replacementMap.TryGetValue(patternMatched[0], out var replacement))
                {
                    if (!replacement.IsEmpty)
                    {
                        memoryList.Add(replacement.Span);
                    }
                }
                // Standard Unicode Character
                else if (patternMatched[0] > 127)
                {
                    var mem = raw.Slice(prevIndex + matchIndex, 1); 
                    
                    var unicodeReplacement = _unicodeReplacer(mem);
                    
                    if(!unicodeReplacement.IsEmpty)
                    {
                        memoryList.Add(unicodeReplacement.Span);
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

            return memoryList.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ReadOnlyMemory<char> HandleUnicodeSurrogate(ReadOnlyMemory<char> mem, int index)
        {
            if (_replacementMap.TryGetValue(mem.Span[index], out var replacement))
            {
                return replacement;
            }
            else
            {
                var slice = mem.Slice(index, 1);
                var unicodeSurrogateReplacement = _unicodeSurrogateReplacer(slice);

                _replacementMap.Add(mem.Span[index], replacement);
                
                if (!unicodeSurrogateReplacement.IsEmpty)
                {
                    return unicodeSurrogateReplacement;
                }
            }

            return null;
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