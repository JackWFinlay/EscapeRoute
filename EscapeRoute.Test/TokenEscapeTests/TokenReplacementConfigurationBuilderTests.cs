using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EscapeRoute.Configuration;
using FluentAssertions;
using Xunit;

namespace EscapeRoute.Test.TokenEscapeTests
{
    public class TokenReplacementConfigurationBuilderTests
    {
        [Fact]
        public void TokenConfigBuilder_ValidConfig_CreatesValidConfig()
        {
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                .SetTokenEnd("}")
                .AddMapping("key", "substitution")
                .Build();

            config.TokenStart
                .ToString()
                .Should()
                .BeEquivalentTo("{");

            config.TokenEnd
                .ToString()
                .Should()
                .BeEquivalentTo("}");

            config.SubstitutionMap
                .Should()
                .ContainKey("key".AsMemory())
                .And
                .ContainValue("substitution".AsMemory());
        }

        [Fact]
        public void TokenConfigBuilder_DuplicateKey_ThrowsArgumentException()
        {
            Action action = () => new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                .SetTokenEnd("}")
                .AddMapping("key", "substitution")
                .AddMapping("key", "substitution2")
                .Build();

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage("Token 'key' is already mapped to substitution 'substitution'");
        }

        [Fact]
        public void TokenConfigBuilder_NoTokenStart_ThrowsArgumentException()
        {
            Action action = () => new TokenReplacementConfigurationBuilder()
                .SetTokenEnd("}")
                .AddMapping("key", "substitution")
                .Build();

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage("No token start defined. Use SetTokenStart to set token start characters.");
        }

        [Fact]
        public void TokenConfigBuilder_NoTokenEnd_ThrowsArgumentException()
        {
            Action action = () => new TokenReplacementConfigurationBuilder()
                .SetTokenStart("{")
                .AddMapping("key", "substitution")
                .Build();

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage("No token end defined. Use SetTokenEnd to set token end characters.");
        }

        [Fact]
        public void TokenConfigBuilder_DuplicateTokenStart_ThrowsArgumentException()
        {
            Action action = () => new TokenReplacementConfigurationBuilder()
                .SetTokenStart("{")
                .SetTokenStart("{")
                .Build();

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage("TokenStart is already set to '{'");
        }

        [Fact]
        public void TokenConfigBuilder_DuplicateTokenEnd_ThrowsArgumentException()
        {
            Action action = () => new TokenReplacementConfigurationBuilder()
                .SetTokenEnd("}")
                .SetTokenEnd("}")
                .Build();

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage("TokenEnd is already set to '}'");
        }

        [Fact]
        public void TokenConfigBuilder_UpdateMapping_CreatesValidConfig()
        {
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                .SetTokenEnd("}")
                .AddMapping("key", "substitution")
                .UpdateOrAddMapping("key", "substitution2")
                .Build();

            config.TokenStart
                .ToString()
                .Should()
                .BeEquivalentTo("{");

            config.TokenEnd
                .ToString()
                .Should()
                .BeEquivalentTo("}");

            config.SubstitutionMap
                .Should()
                .ContainKey("key".AsMemory())
                .And
                .ContainValue("substitution2".AsMemory());
        }

        [Fact]
        public void TokenConfigBuilder_UpdateOrAddFromObject_CreatesValidConfig()
        {
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                .SetTokenEnd("}")
                .AddMapping("key", "substitution")
                .Build();

            config.UpdateOrAddFromObject(new { Key = "substitution2" });

            config.TokenStart
                .ToString()
                .Should()
                .BeEquivalentTo("{");

            config.TokenEnd
                .ToString()
                .Should()
                .BeEquivalentTo("}");

            config.SubstitutionMap
                .Should()
                .ContainKey("key".AsMemory())
                .And
                .ContainValue("substitution2".AsMemory());
        }
    }
}