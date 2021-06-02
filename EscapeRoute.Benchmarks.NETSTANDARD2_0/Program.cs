using System.Reflection;
using BenchmarkDotNet.Running;

namespace EscapeRoute.Benchmarks.NETSTANDARD2_0
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run(Assembly.GetExecutingAssembly());
        }
    }
}