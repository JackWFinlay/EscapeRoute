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
            public string Location { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public int Age { get; set; }
            public int Rank { get; set; }
            
        }

        private static readonly IEscapeRouter _escapeRouter;
        private static readonly string _testString;
        private static readonly TestClass _tokenValues;
        private const int _iterations = 1;
        private const int _age = 99;
        private const int _rank = 1;
        private const string _name = "John";
        private const string _location = "Melbourne";
        private const string _state = "Victoria";
        private const string _country = "Australia";


        static TokenReplacementMultipleTokenBenchmarks()
        {
            // var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
            //     .SetTokenEnd("}")
            //     .AddMapping("name", _name)
            //     .AddMapping("age", _age.ToString())
            //     .AddMapping("rank", _rank.ToString())
            //     .AddMapping("location", _location)
            //     .AddMapping("state", _state)
            //     .AddMapping("country", _country)
            //     .Build();
            //
            // _escapeRouter = new EscapeRouter(config);

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < _iterations; i++)
            {
                stringBuilder.AppendLine("{name} {age} {rank} {location} {state} {country}");
            }

            _testString = stringBuilder.ToString();
            
            // _tokenValues = new TestClass() { 
            //     Name = _name,
            //     Location = _location,
            //     State = _state,
            //     Country = _country,
            //     Age = _age,
            //     Rank = _rank
            // };
        }

        [Benchmark(Baseline = true)]
        public void StringTokenFormatter()
        {
            var tokenValues = new TestClass() { 
                Name = _name,
                Location = _location,
                State = _state,
                Country = _country,
                Age = _age,
                Rank = _rank
            };
            
            _testString.FormatToken(tokenValues);
        }

        [Benchmark]
        public async Task EscapeRouteTokenReplacementEngine()
        {
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                .SetTokenEnd("}")
                .AddMapping("name", _name)
                .AddMapping("age", _age.ToString())
                .AddMapping("rank", _rank.ToString())
                .AddMapping("location", _location)
                .AddMapping("state", _state)
                .AddMapping("country", _country)
                .Build();

            var escapeRouter = new EscapeRouter(config);
            await escapeRouter.ParseAsync(_testString);
        }

        [Benchmark]
        public void NativeStringReplace()
        {
            _testString.Replace("{name}", _name)
                .Replace("{age}", _age.ToString())
                .Replace("{rank}", _rank.ToString())
                .Replace("{location}", _location)
                .Replace("{state}", _state)
                .Replace("{country}", _country);

        }
    }
}