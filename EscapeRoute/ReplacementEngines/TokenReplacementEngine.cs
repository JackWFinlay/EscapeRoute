using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Exceptions;
using EscapeRoute.Abstractions.Interfaces;
using EscapeRoute.Configuration;
using EscapeRoute.DataStructures;
using EscapeRoute.Extensions;

namespace EscapeRoute.ReplacementEngines
{
    public class TokenReplacementEngine : IReplacementEngine
    {
        private readonly TokenReplacementConfiguration _config;

        public TokenReplacementEngine(TokenReplacementConfiguration config)
        {
            _config = config;
        }

        public Task<ReadOnlyMemory<ReadOnlyMemory<char>>> ReplaceAsync(ReadOnlyMemory<char> raw, out int length)
        {
            length = 0;
            
            if (raw.IsEmpty)
            {
                return Task.FromResult<ReadOnlyMemory<ReadOnlyMemory<char>>>(null);
            }

            // var matchIndexes = GetMatches(raw);
            //
            // if (matchIndexes.IsEmpty)
            // {
            //     return Task.FromResult<ReadOnlyMemory<ReadOnlyMemory<char>>>(null);
            // }
            
            try
            {
                var (memory, totalLength) = CreateEscapedTextList(raw);
                
                if (memory.IsEmpty)
                {
                    return Task.FromResult<ReadOnlyMemory<ReadOnlyMemory<char>>>(null);
                }

                length = totalLength;

                return Task.FromResult(memory);
            }
            catch (Exception e)
            {
                throw new EscapeRouteParseException($"Cannot parse the string '{raw.ToString()}'", e);
            }
        }

        private (ReadOnlyMemory<ReadOnlyMemory<char>> memorySpan, int length) CreateEscapedTextList(ReadOnlyMemory<char> raw)
        {
            var result = new ValueList<ReadOnlyMemory<char>>();
            var length = 0;
            var prevIndex = 0;

            while (prevIndex < raw.Length)
            {
                var slice = raw.Slice(prevIndex);

                var indexOfTokenStart = slice.IndexOfTokenStart(_config.TokenStart);

                if (indexOfTokenStart < 0)
                {
                    break;
                }

                if (prevIndex + indexOfTokenStart >= raw.Length)
                {
                    prevIndex = raw.Length;
                    break;
                }
                
                // Add chars preceding token.
                if (indexOfTokenStart > 0)
                {
                    var memorySlice = raw.Slice(prevIndex, indexOfTokenStart);
                    if (!memorySlice.IsEmpty)
                    {
                        result.Add(memorySlice);
                        length += memorySlice.Length;
                    }
                }

                var startOfTokenEndIndex = raw.Slice(prevIndex + indexOfTokenStart + _config.TokenStart.Length).IndexOfTokenStart(_config.TokenEnd);

                if (startOfTokenEndIndex < 0)
                {
                    // Malformed token. Return empty token.
                    return (null, 0);
                }

                // We can just grab the slice of length startOfTokenEndIndex because of zero-based indexing.
                // e.g. where slice = 'abc|*...' with token end '|*' then index of token end is 3, so we take the first 3 chars.
                var token = raw.Slice(prevIndex + indexOfTokenStart + _config.TokenStart.Length, startOfTokenEndIndex);

                if (_config.SubstitutionMap.TryGetValue(token, out var substitution))
                {
                    if (!substitution.IsEmpty)
                    {
                        result.Add(substitution);
                        length += substitution.Length;
                    }
                }
                else
                {
                    // This is a substitution token that isn't registered. Don't replace it.
                    result.Add(_config.TokenStart);
                    result.Add(token);
                    result.Add(_config.TokenEnd);
                    length += (_config.TokenStart.Length + token.Length + _config.TokenEnd.Length);
                }

                // Skip past what we know to be the token.
                var tokenLength = _config.TokenStart.Length + token.Length + _config.TokenEnd.Length;
                if (prevIndex == 0)
                {
                    prevIndex = (indexOfTokenStart + tokenLength);
                }
                else
                {
                    prevIndex += (indexOfTokenStart + tokenLength);
                }
            }
            
            // Add on end of raw.
            if (prevIndex < raw.Length)
            {
                var endSlice = raw.Slice(prevIndex);
                result.Add(endSlice);
                length += endSlice.Length;
            }
            
            return (result.AsMemory(), length);
        }

        private ReadOnlySpan<TokenMatch> GetMatches(ReadOnlyMemory<char> memory)
        {
            var valueList = new ValueList<TokenMatch>();
            
            while (memory.Length > 0)
            {
                var index = memory.IndexOfTokenStart(_config.TokenStart);
                
                if (index == -1)
                {
                    valueList.Add(new TokenMatch() { Index = memory.Length});
                    break;
                }

                // Take a slice without the token start.
                var slice = memory.Slice((index + _config.TokenStart.Length));
                var token = GetToken(slice, _config.TokenEnd);

                var tokenMatch = new TokenMatch()
                {
                    Index = index,
                    Token = token
                };
                
                valueList.Add(tokenMatch);

                // Skip past the entire token.
                var newIndex = index + _config.TokenStart.Length + token.Length + _config.TokenEnd.Length;

                // Token is at end of string.
                if (newIndex >= memory.Length)
                {
                    valueList.Add(new TokenMatch() { Index = memory.Length});
                    break;
                }

                memory = memory.Slice(newIndex);
            }

            return valueList.AsSpan();
        }

        private ReadOnlyMemory<char> GetToken(ReadOnlyMemory<char> slice, ReadOnlyMemory<char> configTokenEnd)
        {
            var startOfTokenEndIndex = slice.IndexOfTokenStart(_config.TokenEnd);

            if (startOfTokenEndIndex < 0)
            {
                // Malformed token. Return empty token.
                return new char[0];
            }

            // We can just grab the slice of length startOfTokenEndIndex because of zero-based indexing.
            // e.g. where slice = 'abc|*...' with token end '|*' then index of token end is 3, so we take the first 3 chars.
            var token = slice.Slice(0, startOfTokenEndIndex);

            return token;
        }
    }
}