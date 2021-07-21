using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.BehaviorTests
{
    public class SingleQuoteBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineSingleQuoteDouble()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                SingleQuoteBehavior = SingleQuoteBehavior.Double
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. \'Something else\'.";
            const string expected = @"The quick brown fox jumps over the lazy dog. \""Something else\"".";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineSingleQuoteSingle()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                SingleQuoteBehavior = SingleQuoteBehavior.Single
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. \'Something else\'.";
            const string expected = @"The quick brown fox jumps over the lazy dog. \'Something else\'.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineSingleQuoteIgnore()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                SingleQuoteBehavior = SingleQuoteBehavior.Ignore
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. \'Something else\'.";
            const string expected = "The quick brown fox jumps over the lazy dog. \'Something else\'.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}