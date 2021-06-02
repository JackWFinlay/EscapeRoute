using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.Benchmarks.NETSTANDARD2_0.Benchmarks
{
    [MemoryDiagnoser]
    public class ReplacementEngineUnicodeSurrogatePairBenchmarks
    {
        private static readonly IEscapeRouter _escapeRouter = new EscapeRouter();

        [Benchmark(Baseline = true)]
        public async Task EmojiParseAsync()
        {
            await _escapeRouter.ParseAsync(Constants.BenchmarkStringEmoji);
        }
    }
}