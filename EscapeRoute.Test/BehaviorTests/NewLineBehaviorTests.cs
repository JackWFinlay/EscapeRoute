using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.BehaviorTests
{
    public class NewLineBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineNewLineEscape()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                NewLineBehavior = NewLineBehavior.Escape
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\n Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog.\n Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineNewLineSpace()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                NewLineBehavior = NewLineBehavior.Space
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\nSomething else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineNewLineStrip()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                NewLineBehavior = NewLineBehavior.Strip
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\n Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineCarriageReturnIgnore()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                NewLineBehavior = NewLineBehavior.Ignore
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\n Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog.\n Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}