using System.Reflection;
using BenchmarkDotNet.Running;
using EscapeRoute.Benchmarks.ReplacementEngine;

namespace EscapeRoute.Benchmarks
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run(Assembly.GetExecutingAssembly());
        }
    }
}