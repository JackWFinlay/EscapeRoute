using System.Threading.Tasks;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class SingleQuoteBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineSingleQuoteDouble()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                SingleQuoteBehavior = SingleQuoteBehavior.Double
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. \'Something else\'.";
            const string expected = @"The quick brown fox jumps over the lazy dog. \""Something else\"".";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineSingleQuoteSingle()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                SingleQuoteBehavior = SingleQuoteBehavior.Single
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. \'Something else\'.";
            const string expected = @"The quick brown fox jumps over the lazy dog. \'Something else\'.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineSingleQuoteIgnore()
        {
            var config = new SpanEngine.EscapeRouteConfiguration
            {
                SingleQuoteBehavior = SingleQuoteBehavior.Ignore
            };
            
            SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. \'Something else\'.";
            const string expected = "The quick brown fox jumps over the lazy dog. \'Something else\'.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}