using System.Reflection;
using BenchmarkDotNet.Running;

namespace EscapeRoute.Benchmarks.NET5
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run(Assembly.GetExecutingAssembly());
        }
    }
}