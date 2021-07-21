using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.BehaviorTests
{
    public class UnicodeSurrogateBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeSurrogateEscape()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                UnicodeSurrogateBehavior = UnicodeSurrogateBehavior.Escape
            };
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter();
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°)😍. Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)\ud83d\ude0d. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeSurrogateStrip()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                UnicodeSurrogateBehavior = UnicodeSurrogateBehavior.Strip
            };
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°)😍. Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0). Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineUnicodeSurrogateIgnore()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                UnicodeSurrogateBehavior = UnicodeSurrogateBehavior.Ignore
            };
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog. ( ͡° ͜ʖ ͡°)😍. Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)😍. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}