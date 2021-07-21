using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.BehaviorTests
{
    public class BackslashBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineBackslashEscape()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                BackslashBehavior = BackslashBehavior.Escape
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\\ Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog.\\ Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineBackslashStrip()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                BackslashBehavior = BackslashBehavior.Strip
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\\ Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineBackslashIgnore()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                BackslashBehavior = BackslashBehavior.Ignore
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\\ Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog.\\ Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}