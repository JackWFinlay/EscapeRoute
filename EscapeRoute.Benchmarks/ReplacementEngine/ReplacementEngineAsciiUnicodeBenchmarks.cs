using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.Benchmarks.ReplacementEngine
{
    [MemoryDiagnoser]
    public class ReplacementEngineAsciiUnicodeBenchmarks
    {
        private static readonly IEscapeRouter BaseEscapeRouter = new EscapeRouter();
        private static readonly SpanEngine.Abstractions.Interfaces.IEscapeRouter SpanEscapeRouter = new EscapeRoute.SpanEngine.EscapeRouter();

        [Benchmark(Baseline = true)]
        public async Task AsciiUnicodeParseAsync()
        {
            await BaseEscapeRouter.ParseAsync(Constants.BenchmarkStringUnicode);
        }
        
        [Benchmark]
        public async Task AsciiUnicodeParseAsyncSpanString()
        {
            await SpanEscapeRouter.ParseAsync(Constants.BenchmarkStringUnicode);
        }
        
        [Benchmark]
        public async Task AsciiUnicodeParseAsyncSpanTextReader()
        {
            var stringReader = new StringReader(Constants.BenchmarkStringUnicode);
            await SpanEscapeRouter.ParseAsync(stringReader);
        }
    }
}