using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    public class ReplacementEngineAsciiUnicodeBenchmarks : EscapeRouteBenchmark
    {
        private static readonly IEscapeRouter _escapeRouter = new EscapeRouter();

        [Benchmark(Baseline = true)]
        public async Task AsciiUnicodeParseAsync()
        {
            await _escapeRouter.ParseAsync(Constants.BenchmarkStringUnicode);
        }
    }
}