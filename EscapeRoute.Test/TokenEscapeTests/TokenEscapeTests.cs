using System;
using System.Threading.Tasks;
using EscapeRoute.Configuration;
using FluentAssertions;
using Xunit;

namespace EscapeRoute.Test.TokenEscapeTests
{
    public class TokenEscapeTests
    {
        [Fact]
        public async Task TestTokenReplacementEngine_MatchingStartEndTokens_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                .SetTokenEnd("}")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = substitution;
            const string testString = "{key}";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_MatchingStartEndTokensTextAtStart_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                .SetTokenEnd("}")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = "something substitution";
            const string testString = "something {key}";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_MatchingStartEndTokensTextAtEnd_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                .SetTokenEnd("}")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = "substitution something";
            const string testString = "{key} something";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_MatchingStartEndTokensTextAtStartAndEnd_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                .SetTokenEnd("}")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = "something substitution something";
            const string testString = "something {key} something";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
    }
}