using System.Threading.Tasks;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class DoubleQuoteBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineDoubleQuoteDouble()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                DoubleQuoteBehavior = DoubleQuoteBehavior.Double
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. \"Something else\".";
            const string expected = @"The quick brown fox jumps over the lazy dog. \""Something else\"".";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineDoubleQuoteSingle()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                DoubleQuoteBehavior = DoubleQuoteBehavior.Single
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. \"Something else\".";
            const string expected = @"The quick brown fox jumps over the lazy dog. \'Something else\'.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineDoubleQuoteIgnore()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                DoubleQuoteBehavior = DoubleQuoteBehavior.Ignore
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. \"Something else\".";
            const string expected = "The quick brown fox jumps over the lazy dog. \"Something else\".";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}