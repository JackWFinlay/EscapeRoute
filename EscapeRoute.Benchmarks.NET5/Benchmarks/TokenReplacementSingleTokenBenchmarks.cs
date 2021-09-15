using System;
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
        
        private const string _testString = Constants.ExampleMergeTemplate;
        private const string _tokenValue = "name";
        private const string _name = "John";

        [Benchmark(Baseline = true)]
        public void StringTokenFormatter()
        {
            _testString.FormatToken(_tokenValue, _name);
        }

        [Benchmark]
        public async Task EscapeRouteTokenReplacementEngine()
        {
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                .SetTokenEnd("}")
                .AddMapping(_tokenValue, _name)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            await escapeRouter.ParseAsync(_testString.AsMemory());
        }
        
        [Benchmark]
        public void NativeStringReplace()
        {
            _testString.Replace("{name}", _name);
        }
    }
}