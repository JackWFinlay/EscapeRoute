using EscapeRoute.Abstractions.Enums;
using System.Threading.Tasks;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class BackslashBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineBackslashEscape()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration
            {
                BackslashBehavior = BackslashBehavior.Escape
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\\ Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog.\\ Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineBackslashStrip()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration
            {
                BackslashBehavior = BackslashBehavior.Strip
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\\ Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineBackslashIgnore()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration
            {
                BackslashBehavior = BackslashBehavior.Ignore
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\\ Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog.\\ Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}