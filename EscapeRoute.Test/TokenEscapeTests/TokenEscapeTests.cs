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
        
        [Fact]
        public async Task TestTokenReplacementEngine_LongerTokenStartEnd_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{{")
                .SetTokenEnd("}}")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = substitution;
            const string testString = "{{key}}";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_LongerTokenStartEndTextAtStart_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{{")
                .SetTokenEnd("}}")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = "something substitution";
            const string testString = "something {{key}}";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_LongerTokenStartEndTextAtEnd_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{{")
                .SetTokenEnd("}}")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = "substitution something";
            const string testString = "{{key}} something";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_LongerTokenStartEndTextAtStartAndEnd_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{{")
                .SetTokenEnd("}}")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = "something substitution something";
            const string testString = "something {{key}} something";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_MixedTokenStartEnd_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("*|")
                .SetTokenEnd("|*")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = substitution;
            const string testString = "*|key|*";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_MixedTokenStartEndTextAtStart_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("*|")
                .SetTokenEnd("|*")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = "something substitution";
            const string testString = "something *|key|*";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_MixedTokenStartEndTextAtEnd_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("*|")
                .SetTokenEnd("|*")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = "substitution something";
            const string testString = "*|key|* something";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_MixedTokenStartEndTextAtStartAndEnd_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("*|")
                .SetTokenEnd("|*")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = "something substitution something";
            const string testString = "something *|key|* something";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_MixedTokenStartEndMultipleSubstitution_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("*|")
                .SetTokenEnd("|*")
                .AddMapping("key", substitution)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = "substitution something substitution something substitution";
            const string testString = "*|key|* something *|key|* something *|key|*";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_MixedTokenStartEndMultipleKeys_ReturnsProperlySubstitutedText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("*|")
                .SetTokenEnd("|*")
                .AddMapping("key", substitution)
                .AddMapping("key2", "substitution2")
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = "substitution something substitution2 something substitution";
            const string testString = "*|key|* something *|key2|* something *|key|*";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
        
        [Fact]
        public async Task TestTokenReplacementEngine_UnmatchedToken_ReturnsRawText()
        {
            const string substitution = "substitution";
            
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("*|")
                .SetTokenEnd("|*")
                .AddMapping("key", substitution)
                .AddMapping("key2", "substitution2")
                .Build();

            var escapeRouter = new EscapeRouter(config);
            
            const string expected = "substitution something substitution2 something substitution";
            const string testString = "*|key|* something *|key2|* something *|key|*";

            var actual = await escapeRouter.ParseAsync(testString);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
    }
}