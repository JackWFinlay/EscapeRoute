using System.Threading.Tasks;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class UnicodeBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeEscape()
        {
            var config = new SpanEngine.EscapeRouteConfiguration()
            {
                UnicodeBehavior = UnicodeBehavior.Escape
            };
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°). Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0). Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeStrip()
        {
            var config = new SpanEngine.EscapeRouteConfiguration()
            {
                UnicodeBehavior = UnicodeBehavior.Strip
            };
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°). Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. (   ). Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeIgnore()
        {
            var config = new SpanEngine.EscapeRouteConfiguration()
            {
                UnicodeBehavior = UnicodeBehavior.Ignore
            };
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°). Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°). Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}