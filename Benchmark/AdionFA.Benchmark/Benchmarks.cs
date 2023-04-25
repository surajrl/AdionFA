using AdionFA.Core.Domain.Aggregates.MarketData;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Core.Data.Persistence;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Data;

namespace AdionFA.Benchmark
{
    public class Benchmarks
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }

        [Benchmark]
        public FileInfo[] GetFilesInPath()
        {
            const string path = "C:\\Users\\suraj\\Documents\\FA.Workspace\\Projects\\test-release1\\Extractions\\WithoutSchedule";
            const string ext = "*.csv";

            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles(ext);

            return files;
        }
    }
}
