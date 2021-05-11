using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.Benchmarks.ReplacementEngine
{
    [MemoryDiagnoser]
    public class ReplacementEngineUnicodeSurrogatePairBenchmarks
    {
        private static readonly IEscapeRouter BaseEscapeRouter = new EscapeRouter();
        private static readonly SpanEngine.Abstractions.Interfaces.IEscapeRouter SpanEscapeRouter = new EscapeRoute.SpanEngine.EscapeRouter();

        [Benchmark(Baseline = true)]
        public async Task EmojiParseAsync()
        {
            await BaseEscapeRouter.ParseAsync(Constants.BenchmarkStringEmoji);
        }
        
        [Benchmark]
        public async Task EmojiParseAsyncSpanString()
        {
            await SpanEscapeRouter.ParseAsync(Constants.BenchmarkStringEmoji);
        }
        
        // [Benchmark]
        // public async Task EmojiAsyncSpanTextReader()
        // {
        //     var stringReader = new StringReader(Constants.BenchmarkStringEmoji);
        //     await SpanEscapeRouter.ParseAsync(stringReader);
        // }
    }
}