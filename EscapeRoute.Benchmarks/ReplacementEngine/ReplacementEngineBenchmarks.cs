using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using EscapeRoute.ReplacementEngines;

namespace EscapeRoute.Benchmarks.ReplacementEngine
{
    [MemoryDiagnoser]
    public class ReplacementEngineBenchmarks
    {
        private static readonly EscapeRouter _escapeRouter = new EscapeRouter();
        private static readonly EscapeRouter _spanEscapeRouter = new EscapeRouter(new EscapeRouteConfiguration()
        {
            ReplacementEngine = new SpanReplacementEngine()
        });

        // [Benchmark(Baseline = true)]
        // public void ParseString()
        // {
        //     _escapeRouter.ParseString(Constants.BenchmarkString);
        // }
        //
        //
        // public async Task ParseStringAsync()
        // {
        //     await _escapeRouter.ParseStringAsync(Constants.BenchmarkString);
        // }
        
        [Benchmark]
        public async Task ParseAsync()
        {
            await _escapeRouter.ParseAsync(Constants.BenchmarkString);
        }
        
        [Benchmark(Baseline = true)]
        public async Task ParseAsyncSpan()
        {
            await _spanEscapeRouter.ParseAsync(Constants.BenchmarkString);
        }
    }
}