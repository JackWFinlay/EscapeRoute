using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using EscapeRoute.Abstractions.Interfaces;
using EscapeRoute.Configuration;
using StringTokenFormatter;

namespace EscapeRoute.Benchmarks.NET5.Benchmarks
{
    [MemoryDiagnoser]
    public class TokenReplacementSingleTokenBenchmarks
    {
        private class TestClass
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        private static readonly IEscapeRouter _escapeRouter;
        private static readonly string _testString;
        private const int _iterations = 100_000;
        private const string _tokenValue = "name";
        private const string _name = "John";

        static TokenReplacementSingleTokenBenchmarks()
        {
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                .SetTokenEnd("}")
                .AddMapping(_tokenValue, _name)
                .Build();

            _escapeRouter = new EscapeRouter(config);

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < _iterations; i++)
            {
                stringBuilder.AppendLine("{name}");
            }

            _testString = stringBuilder.ToString();
        }

        [Benchmark(Baseline = true)]
        public void StringTokenFormatter()
        {
            _testString.FormatToken(_tokenValue, _name);
        }

        [Benchmark]
        public async Task EscapeRouteTokenReplacementEngine()
        {
            await _escapeRouter.ParseAsync(_testString);
        }
    }
}