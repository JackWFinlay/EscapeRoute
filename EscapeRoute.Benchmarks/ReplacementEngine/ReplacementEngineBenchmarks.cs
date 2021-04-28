using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.Benchmarks.ReplacementEngine
{
    [MemoryDiagnoser]
    public class ReplacementEngineBenchmarks
    {
        private static readonly IEscapeRouter BaseEscapeRouter = new EscapeRouter();
        private static readonly IEscapeRouter SpanEscapeRouter = new EscapeRoute.SpanEngine.EscapeRouter();

        [Benchmark(Baseline = true)]
        public async Task ParseAsync()
        {
            await BaseEscapeRouter.ParseAsync(Constants.BenchmarkString);
        }
        
        [Benchmark]
        public async Task ParseAsyncSpanString()
        {
            await SpanEscapeRouter.ParseAsync(Constants.BenchmarkString);
        }
        
        [Benchmark]
        public async Task ParseAsyncSpanTextReader()
        {
            var stringReader = new StringReader(Constants.BenchmarkString);
            await SpanEscapeRouter.ParseAsync(stringReader);
        }
    }
}