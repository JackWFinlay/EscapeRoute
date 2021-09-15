using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace EscapeRoute.Benchmarks.Benchmarks
{
    [Config(typeof(Config))]
    public class EscapeRouteBenchmark
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                var baseJob = Job.Default;

                AddJob(baseJob.WithNuGet("EscapeRoute", "0.0.3"));
                AddJob(baseJob.WithNuGet("EscapeRoute", "0.0.4"));
            }
        }
    }
}