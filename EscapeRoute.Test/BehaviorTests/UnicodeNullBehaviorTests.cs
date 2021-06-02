using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class UnicodeNullBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeNullStrip()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration()
            {
                UnicodeNullBehavior = UnicodeNullBehavior.Strip
            };
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°)\0";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeNullEscape()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration()
            {
                UnicodeNullBehavior = UnicodeNullBehavior.Escape
            };
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°)\0";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)\0";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeNullEscapeHex()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration()
            {
                UnicodeNullBehavior = UnicodeNullBehavior.EscapeHex
            };
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°)\0";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)\u0000";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeNullIgnore()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration()
            {
                UnicodeNullBehavior = UnicodeNullBehavior.Ignore
            };
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°)\0";
            const string expected = "The quick brown fox jumps over the lazy dog. ( \\u0361\\u00b0 \\u035c\\u0296 \\u0361\\u00b0)\0";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}