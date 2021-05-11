using EscapeRoute.SpanEngine.Abstractions.Enums;
using System.Threading.Tasks;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class BackslashBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineBackslashEscape()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                BackslashBehavior = BackslashBehavior.Escape
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\\ Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog.\\ Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineBackslashStrip()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                BackslashBehavior = BackslashBehavior.Strip
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\\ Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineBackslashIgnore()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                BackslashBehavior = BackslashBehavior.Ignore
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\\ Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog.\\ Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}