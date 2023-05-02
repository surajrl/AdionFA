using AdionFA.Core.Domain.Aggregates.MarketData;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Core.Data.Persistence;
using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Model.Weka;
using AdionFA.UI.Station.Project.Model.StrategyBuilder;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Xml.Linq;

namespace AdionFA.Benchmark
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }

        public static IEnumerable<(string FullName, string Name)> GetFileNames(string directoryPath, string searchPattern)
        {
            foreach (string file in Directory.EnumerateFiles(directoryPath, searchPattern))
            {
                var fileName = Path.GetFileName(file);
                var fullPath = Path.GetFullPath(file);

                yield return (FullName: fullPath, Name: fileName);
            }
        }

        [Benchmark]
        public void UsingEnumerateFiles()
        {
            const string path = "C:\\Users\\suraj\\Documents\\FA.Workspace\\Projects\\test-release1\\Extractions\\WithoutSchedule";
            const string ext = "*.csv";

            var strategyBuilderProcessList = new ObservableCollection<ProcessModel>();

            foreach ((var fullName, var name) in GetFileNames(path, ext))
            {
                strategyBuilderProcessList.Add(new ProcessModel
                {
                    Path = fullName,
                    TemplateName = name,
                    IsEnabled = false,
                    IsExpanded = false,
                    InstancesList = new ObservableCollection<TreeOutput>()
                });
            }
        }

        [Benchmark]
        public void UsingGetFiles()
        {
            var strategyBuilderProcessList = new ObservableCollection<ProcessModel>();

            const string path = "C:\\Users\\suraj\\Documents\\FA.Workspace\\Projects\\test-release1\\Extractions\\WithoutSchedule";
            const string ext = "*.csv";

            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles(ext);

            for (var i = 0; i < files.Length; i++)
            {
                strategyBuilderProcessList.Add(new ProcessModel
                {
                    Path = files[i].FullName,
                    TemplateName = files[i].Name,
                    IsEnabled = false,
                    IsExpanded = false,
                    InstancesList = new ObservableCollection<TreeOutput>()
                });
            }
        }

        public class ProcessModel
        {
            public string Path { get; set; }

            public string TemplateName { get; set; }
            public bool IsExpanded { get; set; }
            public bool IsEnabled { get; set; }
            public ObservableCollection<TreeOutput> InstancesList { get; set; }
        }

        public class TreeOutput
        {
            public int Seed { get; set; }
            public string Output { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public int TotalWinningStrategy { get; set; }
            public int CounterProgressBar { get; set; }
        }
    }
}
