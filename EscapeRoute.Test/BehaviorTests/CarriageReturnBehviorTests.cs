using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.BehaviorTests
{
    public class CarriageReturnBehviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineCarriageReturnEscape()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                CarriageReturnBehavior = CarriageReturnBehavior.Escape
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\r Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog.\r Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineCarriageReturnStrip()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                CarriageReturnBehavior = CarriageReturnBehavior.Strip
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\r Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineCarriageReturnIgnore()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                CarriageReturnBehavior = CarriageReturnBehavior.Ignore
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\r Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog.\r Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}