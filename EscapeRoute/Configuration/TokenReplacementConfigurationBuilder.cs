using System;
using System.Collections.Generic;
using System.Linq;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.Configuration
{
    public class TokenReplacementConfigurationBuilder : ITokenReplacementConfigurationBuilder
    {
        private readonly IDictionary<ReadOnlyMemory<char>, ReadOnlyMemory<char>> _substitutionMap =
            new Dictionary<ReadOnlyMemory<char>, ReadOnlyMemory<char>>();

        private ReadOnlyMemory<char> _tokenStart;
        private ReadOnlyMemory<char> _tokenEnd;
        
        public ITokenReplacementConfigurationBuilder AddMapping(string token, 
            string substitution)
        {
            if (_substitutionMap.ContainsKey(token.AsMemory()))
            {
                throw new ArgumentException(
                    $"Token '{token}' is already mapped to substitution '{substitution}'", nameof(token));
            }
            
            _substitutionMap.Add(token.AsMemory(), substitution.AsMemory());

            return this;
        }

        public ITokenReplacementConfigurationBuilder SetTokenStart(string tokenStart)
        {
            _tokenStart = tokenStart.AsMemory();

            return this;
        }

        public ITokenReplacementConfigurationBuilder SetTokenEnd(string tokenEnd)
        {
            _tokenEnd = tokenEnd.AsMemory();

            return this;
        }

        public TokenReplacementConfiguration Build()
        {
            if (!_substitutionMap.Any())
            {
                throw new ArgumentException(
                    $"No token substitution mappings have been added. Use {nameof(AddMapping)}() to add mapping.");
            }

            if (_tokenStart.IsEmpty)
            {
                throw new ArgumentException(
                    $"No token start defined. Use {nameof(SetTokenStart)} to set token start characters.");
            }
            
            if (_tokenEnd.IsEmpty)
            {
                throw new ArgumentException(
                    $"No token end defined. Use {nameof(SetTokenEnd)} to set token end characters.");
            }

            var config = new TokenReplacementConfiguration()
            {
                TokenStart = _tokenStart,
                TokenEnd = _tokenEnd,
                SubstitutionMap = _substitutionMap
            };

            return config;
        }
    }
}