using System.Threading.Tasks;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class NewLineBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineNewLineEscape()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                NewLineBehavior = NewLineBehavior.Escape
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\n Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog.\n Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineNewLineSpace()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                NewLineBehavior = NewLineBehavior.Space
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\nSomething else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineNewLineStrip()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                NewLineBehavior = NewLineBehavior.Strip
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\n Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineCarriageReturnIgnore()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                NewLineBehavior = NewLineBehavior.Ignore
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\n Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog.\n Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}