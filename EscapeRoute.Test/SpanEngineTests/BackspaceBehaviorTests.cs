using System.Threading.Tasks;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class BackspaceBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineBackspaceEscape()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                BackspaceBehavior = BackspaceBehavior.Escape
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\b Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog.\b Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineBackspaceStrip()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                BackspaceBehavior = BackspaceBehavior.Strip
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\b Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineBackspaceIgnore()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                BackspaceBehavior = BackspaceBehavior.Ignore
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\b Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog.\b Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}