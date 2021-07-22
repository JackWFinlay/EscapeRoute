using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Exceptions;
using EscapeRoute.Abstractions.Interfaces;
using EscapeRoute.Configuration;
using EscapeRoute.Extensions;
using EscapeRoute.Models;

namespace EscapeRoute.ReplacementEngines
{
    public class TokenReplacementEngine : IReplacementEngine
    {
        private readonly TokenReplacementConfiguration _config;

        public TokenReplacementEngine(TokenReplacementConfiguration config)
        {
            _config = config;
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

        private IEnumerable<ReadOnlyMemory<char>> CreateEscapedTextList(ReadOnlyMemory<char> raw, IEnumerable<TokenMatch> matches)
        {
            var result = new List<ReadOnlyMemory<char>>();
            
            var prevIndex = 0;

            foreach (var match in matches)
            {
                if (prevIndex >= raw.Length || match.Index >= raw.Length)
                {
                    break;
                }

                if (match.Index > 0)
                {
                    var memorySlice = raw.Slice(prevIndex, match.Index);
                    if (!memorySlice.IsEmpty)
                    {
                        result.Add(memorySlice);
                    }
                }
                
                if (prevIndex + match.Index >= raw.Length)
                {
                    break;
                }

                if (_config.SubstitutionMap.TryGetValue(match.Token, out var substitution))
                {
                    if (!substitution.IsEmpty)
                    {
                        result.Add(substitution);
                    }
                }
                else
                {
                    // This is a substitution token that isn't registered. Don't replace it.
                    result.Add(_config.TokenStart);
                    result.Add(match.Token);
                    result.Add(_config.TokenEnd);
                }

                var tokenLength = _config.TokenStart.Length + match.Token.Length + _config.TokenEnd.Length;
                if (prevIndex == 0)
                {
                    prevIndex = (match.Index + tokenLength);
                }
                else
                {
                    prevIndex += (match.Index + tokenLength);
                }

            }

            return result;
        }

        private IEnumerable<TokenMatch> GetMatches(ReadOnlyMemory<char> memory)
        {
            while (memory.Length > 0)
            {
                var index = memory.IndexOfTokenStart(_config.TokenStart);
                
                if (index == -1)
                {
                    yield return new TokenMatch() { Index = memory.Length};
                    yield break;
                }

                // Take a slice without the token start.
                var slice = memory.Slice((index + _config.TokenStart.Length));
                var token = GetToken(slice, _config.TokenEnd);

                var tokenMatch = new TokenMatch()
                {
                    Index = index,
                    Token = token
                };
                
                yield return tokenMatch;

                // Skip past the entire token.
                var newIndex = index + _config.TokenStart.Length + token.Length + _config.TokenEnd.Length;

                // Token is at end of string.
                if (newIndex >= memory.Length)
                {
                    yield return new TokenMatch() { Index = memory.Length};
                    yield break;
                }

                memory = memory.Slice(newIndex);
            }
        }

        private ReadOnlyMemory<char> GetToken(ReadOnlyMemory<char> slice, ReadOnlyMemory<char> configTokenEnd)
        {
            var startOfTokenEndIndex = slice.IndexOfTokenStart(_config.TokenEnd);

            if (startOfTokenEndIndex < 0)
            {
                // Malformed token. Return empty token.
                return new char[] {};
            }

            // We can just grab the slice of length startOfTokenEndIndex because of zero-based indexing.
            // e.g. where slice = 'abc|*...' with token end '|*' then index of token end is 3, so we take the first 3 chars.
            var token = slice.Slice(0, startOfTokenEndIndex);

            return token;
        }
    }
}