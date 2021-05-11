using System.Threading.Tasks;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class CarriageReturnBehviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineCarriageReturnEscape()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                CarriageReturnBehavior = CarriageReturnBehavior.Escape
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\r Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog.\r Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineCarriageReturnStrip()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                CarriageReturnBehavior = CarriageReturnBehavior.Strip
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\r Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineCarriageReturnIgnore()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                CarriageReturnBehavior = CarriageReturnBehavior.Ignore
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\r Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog.\r Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}