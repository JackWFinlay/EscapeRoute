using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EscapeRoute.Configuration
{
    public class TokenReplacementConfigurationBuilder : TokenReplacementConfiguration
    {
        public TokenReplacementConfigurationBuilder AddMapping(string token, 
            string substitution)
        {
            if (SubstitutionMap.TryGetValue(token.AsMemory(), out var value))
            {
                throw new ArgumentException(
                    $"Token '{token}' is already mapped to substitution '{value}'");
            }
            
            SubstitutionMap.Add(token.AsMemory(), substitution.AsMemory());

            return this;
        }

        public TokenReplacementConfigurationBuilder SetTokenStart(string tokenStart)
        {
            if (!TokenStart.IsEmpty)
            {
                throw new ArgumentException(
                    $"TokenStart is already set to '{TokenStart}'");
            }

            TokenStart = tokenStart.AsMemory();

            return this;
        }

        public TokenReplacementConfigurationBuilder SetTokenEnd(string tokenEnd)
        {
            if (!TokenEnd.IsEmpty)
            {
                throw new ArgumentException(
                    $"TokenEnd is already set to '{TokenEnd}'");
            }
            
            TokenEnd = tokenEnd.AsMemory();

            return this;
        }

        public TokenReplacementConfiguration Build()
        {
            if (TokenStart.IsEmpty)
            {
                throw new ArgumentException(
                    $"No token start defined. Use {nameof(SetTokenStart)} to set token start characters.");
            }
            
            if (TokenEnd.IsEmpty)
            {
                throw new ArgumentException(
                    $"No token end defined. Use {nameof(SetTokenEnd)} to set token end characters.");
            }

            var config = new TokenReplacementConfiguration()
            {
                TokenStart = TokenStart,
                TokenEnd = TokenEnd,
                SubstitutionMap = SubstitutionMap
            };

            return config;
        }
        
        public new TokenReplacementConfigurationBuilder UpdateOrAddMapping(string token, string substitution)
        {
            UpdateOrAddMapping(token.AsMemory(), substitution.AsMemory());
            return this;
        }

        public new TokenReplacementConfigurationBuilder UpdateOrAddFromObject(object item)
        {
            base.UpdateOrAddFromObject(item);
            return this;
        }
    }
}