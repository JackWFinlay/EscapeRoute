using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.BehaviorTests
{
    public class BackspaceBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineBackspaceEscape()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                BackspaceBehavior = BackspaceBehavior.Escape
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\b Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog.\b Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineBackspaceStrip()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                BackspaceBehavior = BackspaceBehavior.Strip
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\b Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineBackspaceIgnore()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                BackspaceBehavior = BackspaceBehavior.Ignore
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\b Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog.\b Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}