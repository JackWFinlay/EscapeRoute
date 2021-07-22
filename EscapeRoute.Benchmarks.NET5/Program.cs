using System.Reflection;
using BenchmarkDotNet.Running;
using EscapeRoute.Benchmarks.NET5.Benchmarks;

namespace EscapeRoute.Benchmarks.NET5
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run(typeof(TokenReplacementMultipleTokenBenchmarks));
            BenchmarkRunner.Run(typeof(TokenReplacementSingleTokenBenchmarks));
            //BenchmarkRunner.Run(typeof(ReplacementEngineAsciiBenchmarks));
            //BenchmarkRunner.Run(typeof(ReplacementEngineAsciiUnicodeBenchmarks));
            //BenchmarkRunner.Run(typeof(ReplacementEngineUnicodeSurrogatePairBenchmarks));
        }
    }
}