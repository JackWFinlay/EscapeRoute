using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test.SpanEngineTests
{
    public class TabBehaviorTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineTabEscape()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\t Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog.\t Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineTabStrip()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Strip
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\t Something else.";
            const string expected = @"The quick brown fox jumps over the lazy dog. Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineTabIgnore()
        {
            var config = new EscapeRoute.EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Ignore
            };
            
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter(config);
            const string inputString = "The quick brown fox jumps over the lazy dog.\t Something else.";
            const string expected = "The quick brown fox jumps over the lazy dog.\t Something else.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}