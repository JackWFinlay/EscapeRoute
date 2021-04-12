using System;
using BenchmarkDotNet.Running;
using EscapeRoute.Benchmarks.ReplacementEngine;

namespace EscapeRoute.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ReplacementEngineBenchmarks>();
        }
    }
}