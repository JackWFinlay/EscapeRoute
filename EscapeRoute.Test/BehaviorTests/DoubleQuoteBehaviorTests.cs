using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class DoubleQuoteBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineDoubleQuoteDouble()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration
            {
                DoubleQuoteBehavior = DoubleQuoteBehavior.Double
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. \"Something else\".";
            const string expected = @"The quick brown fox jumps over the lazy dog. \""Something else\"".";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineDoubleQuoteSingle()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration
            {
                DoubleQuoteBehavior = DoubleQuoteBehavior.Single
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. \"Something else\".";
            const string expected = @"The quick brown fox jumps over the lazy dog. \'Something else\'.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineDoubleQuoteIgnore()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration
            {
                DoubleQuoteBehavior = DoubleQuoteBehavior.Ignore
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. \"Something else\".";
            const string expected = "The quick brown fox jumps over the lazy dog. \"Something else\".";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}