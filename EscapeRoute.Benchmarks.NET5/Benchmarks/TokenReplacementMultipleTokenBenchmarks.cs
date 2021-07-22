using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using EscapeRoute.Abstractions.Interfaces;
using EscapeRoute.Configuration;
using StringTokenFormatter;

namespace EscapeRoute.Benchmarks.NET5.Benchmarks
{
    [MemoryDiagnoser]
    public class TokenReplacementMultipleTokenBenchmarks
    {
        private class TestClass
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        private static readonly IEscapeRouter _escapeRouter;
        private static readonly string _testString;
        private static readonly TestClass _tokenValues;
        private const int _iterations = 100_000;

        static TokenReplacementMultipleTokenBenchmarks()
        {
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                .SetTokenEnd("}")
                .AddMapping("name", "John")
                .AddMapping("age", "99")
                .Build();

            _escapeRouter = new EscapeRouter(config);

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < _iterations; i++)
            {
                stringBuilder.AppendLine("{name} {age}");
            }

            _testString = stringBuilder.ToString();
            
            _tokenValues = new TestClass() { Name = "John", Age = 99 };
        }

        [Benchmark(Baseline = true)]
        public void StringTokenFormatter()
        {
            _testString.FormatToken(_tokenValues);
        }

        [Benchmark]
        public async Task EscapeRouteTokenReplacementEngine()
        {
            await _escapeRouter.ParseAsync(_testString);
        }
    }
}