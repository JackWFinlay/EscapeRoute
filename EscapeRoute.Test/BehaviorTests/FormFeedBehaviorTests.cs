using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.BehaviorTests
{
    public class FormFeedBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineFormFeedEscape()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                FormFeedBehavior = FormFeedBehavior.Escape
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\f Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog.\f Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineFormFeedStrip()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                FormFeedBehavior = FormFeedBehavior.Strip
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\f Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineFormFeedIgnore()
        {
            var config = new Configuration.CharReplacementConfiguration
            {
                FormFeedBehavior = FormFeedBehavior.Ignore
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\f Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog.\f Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}