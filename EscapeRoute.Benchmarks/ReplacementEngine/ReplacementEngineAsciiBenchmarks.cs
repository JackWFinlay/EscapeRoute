using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.Benchmarks.ReplacementEngine
{
    [MemoryDiagnoser]
    public class ReplacementEngineAsciiBenchmarks
    {
        private static readonly IEscapeRouter BaseEscapeRouter = new EscapeRouter();
        private static readonly SpanEngine.Abstractions.Interfaces.IEscapeRouter SpanEscapeRouter = new EscapeRoute.SpanEngine.EscapeRouter();

        [Benchmark(Baseline = true)]
        public async Task AsciiParseAsync()
        {
            await BaseEscapeRouter.ParseAsync(Constants.BenchmarkStringAscii);
        }
        
        [Benchmark]
        public async Task AsciiParseAsyncSpanString()
        {
            await SpanEscapeRouter.ParseAsync(Constants.BenchmarkStringAscii);
        }
        
        // [Benchmark]
        // public async Task AsciiParseAsyncSpanTextReader()
        // {
        //     var stringReader = new StringReader(Constants.BenchmarkStringAscii);
        //     await SpanEscapeRouter.ParseAsync(stringReader);
        // }
    }
}