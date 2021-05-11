using System.Threading.Tasks;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class FormFeedBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineFormFeedEscape()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                FormFeedBehavior = FormFeedBehavior.Escape
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\f Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog.\f Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineFormFeedStrip()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                FormFeedBehavior = FormFeedBehavior.Strip
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\f Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineFormFeedIgnore()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                FormFeedBehavior = FormFeedBehavior.Ignore
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\f Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog.\f Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}