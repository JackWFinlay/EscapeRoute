using System.Threading.Tasks;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class UnicodeNullBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeNullStrip()
        {
            var config = new SpanEngine.EscapeRouteConfiguration()
            {
                UnicodeNullBehavior = UnicodeNullBehavior.Strip
            };
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°)\0";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeNullEscape()
        {
            var config = new SpanEngine.EscapeRouteConfiguration()
            {
                UnicodeNullBehavior = UnicodeNullBehavior.Escape
            };
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°)\0";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)\0";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeNullEscapeHex()
        {
            var config = new SpanEngine.EscapeRouteConfiguration()
            {
                UnicodeNullBehavior = UnicodeNullBehavior.EscapeHex
            };
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°)\0";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)\u0000";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeNullIgnore()
        {
            var config = new SpanEngine.EscapeRouteConfiguration()
            {
                UnicodeNullBehavior = UnicodeNullBehavior.Ignore
            };
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°)\0";
            const string expected = "The quick brown fox jumps over the lazy dog. ( \\u0361\\u00b0 \\u035c\\u0296 \\u0361\\u00b0)\0";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}