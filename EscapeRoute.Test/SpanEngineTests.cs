using System.IO;
using System.Threading.Tasks;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using Xunit;

namespace EscapeRoute.Test
{
    public class SpanEngineTests
    {
                [Fact]
                public async Task TestSpanReplacementEngine()
                {
                    SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter();
                    const string inputString = "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog. ( ͡° ͜ʖ ͡°)";
                    const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
                    string result = await escapeRouter.ParseAsync(inputString);
                    Assert.Equal(expected, result);
                }
                
                [Fact]
                public async Task TestSpanReplacementEngineTextReader()
                {
                    SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter();
                    const string inputString = "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog. ( ͡° ͜ʖ ͡°)";
                    const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
                    string result = await escapeRouter.ParseAsync(new StringReader(inputString));
                    Assert.Equal(expected, result);
                }
                
                [Fact]
                public async Task TestSpanReplacementEngineUnicodeNullEscape()
                {
                    var config = new SpanEngine.EscapeRouteConfiguration()
                    {
                        UnicodeNullBehavior = UnicodeNullBehavior.Escape
                    };
                    SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
                    const string inputString = "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog. ( ͡° ͜ʖ ͡°)\0";
                    const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)\0";
                    string result = await escapeRouter.ParseAsync(inputString);
                    Assert.Equal(expected, result);
                }
                
                [Fact]
                public async Task TestSpanReplacementEngineUnicodeNullEscapeHex()
                {
                    var config = new SpanEngine.EscapeRouteConfiguration()
                    {
                        UnicodeNullBehavior = UnicodeNullBehavior.EscapeHex
                    };
                    SpanEngine.Abstractions.Interfaces.IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter(config);
                    const string inputString = "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog. ( ͡° ͜ʖ ͡°)\0";
                    const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)\u0000";
                    string result = await escapeRouter.ParseAsync(inputString);
                    Assert.Equal(expected, result);
                }
    }
}