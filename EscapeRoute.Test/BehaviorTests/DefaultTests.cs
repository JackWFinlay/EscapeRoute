using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace EscapeRoute.Test.BehaviorTests
{
    public class DefaultTests
    {
        [Fact]
        public async Task TestSpanReplacementEngineEmptyString()
        {
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter();
            const string inputString = "";
            const string expected = "";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineNoReplacement()
        {
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter();
            const string inputString = "The quick brown fox jumps over the lazy dog.";
            const string expected = @"The quick brown fox jumps over the lazy dog.";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineOnlyReplacementDefaults()
        {
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter();
            const string inputString = "\r\n\f\t\b😍'\"";
            const string expected = @"\ud83d\ude0d\'\""";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineBasic()
        {
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter();
            const string inputString = "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog. ( ͡° ͜ʖ ͡°)";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            var result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestSpanReplacementEngineTextReaderBasic()
        {
            EscapeRoute.Abstractions.Interfaces.IEscapeRouter escapeRouter = new EscapeRoute.EscapeRouter();
            const string inputString = "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog. ( ͡° ͜ʖ ͡°)";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            var result = await escapeRouter.ParseAsync(new StringReader(inputString));
            Assert.Equal(expected, result);
        }
    }
}