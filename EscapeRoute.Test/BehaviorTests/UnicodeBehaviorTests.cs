using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.BehaviorTests
{
    public class UnicodeBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeEscape()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Escape
            };
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°). Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0). Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeStrip()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Strip
            };
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°). Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. (   ). Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeIgnore()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Ignore
            };
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°). Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°). Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}