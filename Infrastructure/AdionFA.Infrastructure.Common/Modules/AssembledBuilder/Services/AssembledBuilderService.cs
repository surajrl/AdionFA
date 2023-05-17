using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.AssembledBuilder.Contracts;
using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Contracts;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Common.Weka.Services;
using AdionFA.Infrastructure.Enums;
using AdionFA.TransferObject.Project;
using AdionFA.Infrastructure.Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdionFA.Infrastructure.Common.Managements;

namespace AdionFA.Infrastructure.Common.AssembledBuilder.Services
{
    public class AssembledBuilderService : IAssembledBuilderService
    {
        // Services

        private readonly IProjectDirectoryService ProjectDirectoryService;
        private readonly IExtractorService ExtractorService;
        private readonly IStrategyBuilderService StrategyBuilderService;

        public AssembledBuilderService()
        {
            ProjectDirectoryService = IoC.Get<IProjectDirectoryService>();
            ExtractorService = IoC.Get<IExtractorService>();
            StrategyBuilderService = IoC.Get<IStrategyBuilderService>();
        }

        public AssembledBuilderModel LoadStrategyModel(string projectName)
        {
            try
            {
                var model = new AssembledBuilderModel();

                // UP

                StartNodeAssembledModel upNodeStart = AssembledBuilderModel.Start("UP");
                EndNodeAssembledModel upNodeEnd = AssembledBuilderModel.End("UP");

                upNodeStart.Nodes = LoadNodes(projectName, "up");
                upNodeStart.Nodes.ForEach(n => n.Nodes.Add(upNodeEnd));

                if (upNodeStart.Nodes.Count > 0)
                    model.UPNode = upNodeStart;

                // DOWN

                StartNodeAssembledModel downNodeStart = AssembledBuilderModel.Start("DOWN");
                EndNodeAssembledModel downNodeEnd = AssembledBuilderModel.End("DOWN");

                downNodeStart.Nodes = LoadNodes(projectName, "down");
                downNodeStart.Nodes.ForEach(n => n.Nodes.Add(downNodeEnd));

                if (downNodeStart.Nodes.Count > 0)
                    model.DOWNNode = downNodeStart;

                return model;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<AssembledBuilderService>(ex);
                throw;
            }

            // Load Nodes
            List<NodeAssembledModel> LoadNodes(string projectName, string label)
            {
                try
                {
                    var result = new List<NodeAssembledModel>();

                    string path = label.ToLower() == "up" ? projectName.ProjectStrategyBuilderNodesUPDirectory() :
                        label.ToLower() == "down" ? projectName.ProjectStrategyBuilderNodesDOWNDirectory() : null;

                    if (path != null)
                    {
                        ProjectDirectoryService.GetFilesInPath(path, "*.xml")
                            .ToList().ForEach(fi =>
                            {
                                BacktestModel model = SerializerHelper.XMLDeSerializeObject<BacktestModel>(fi.FullName);
                                result.Add(new BacktestNodeAssembledModel
                                {
                                    Backtest = model,
                                    Type = NodeAssembledTypeEnum.Backtest,
                                });
                            });
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException<AssembledBuilderService>(ex);
                    throw;
                }
            }
        }

        public void ExtractorExecute(
            string projectName, AssembledBuilderModel model, IEnumerable<Candle> candles, ProjectConfigurationDTO config)
        {
            try
            {
                if (model?.UPNode?.Nodes?.Any() ?? false)
                    Extractor("up");

                if (model?.DOWNNode?.Nodes?.Any() ?? false)
                    Extractor("down");

                void Extractor(string label)
                {
                    NodeAssembledModel node = label == "up" ? model.UPNode : model.DOWNNode;

                    string directory = label == "up" ?
                        projectName.ProjectAssembledBuilderExtractorUPDirectory()
                        : projectName.ProjectAssembledBuilderExtractorDOWNDirectory();

                    List<NodeAssembledModel> backtestNodes = node.ConvertTreeToList<NodeAssembledModel, BacktestNodeAssembledModel>();

                    List<BacktestModel> backtests = (from bn in backtestNodes
                                                     let bt = (bn as BacktestNodeAssembledModel).Backtest
                                                     select bt).ToList();

                    List<BacktestOperationModel> operations = backtests.SelectMany(bt => bt.Backtests.OrderBy(o => o.Date)).ToList();

                    if (operations.Any() && ProjectDirectoryService.DeleteAllFiles(directory, option: SearchOption.AllDirectories, isBackup: false))
                    {
                        var first = operations.FirstOrDefault().Date;
                        var last = operations.LastOrDefault().Date;

                        var templates = ProjectDirectoryService.GetFilesInPath(
                            projectName.ProjectExtractorTemplatesDirectory()).ToList();

                        Parallel.ForEach(templates,
                            new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount - 1 },
                            fi =>
                            {
                                var indicators = ExtractorService.BuildIndicatorsFromCSV(fi.FullName);
                                var extractions = ExtractorService.DoExtraction(first, last, indicators, candles.ToList(), config.TimeframeId, config.Variation);

                                foreach (var ex in extractions)
                                {
                                    var intervals = (from il in ex.IntervalLabels.Select((_il, _idx) => new { _idx, _il })
                                                     let ops = operations.Where(op => op.Date == il._il.Interval)
                                                     where ops.Any()
                                                     select new
                                                     {
                                                         idx = il._idx,
                                                         il = new IntervalLabel
                                                         {
                                                             Interval = il._il.Interval,
                                                             Label = ops.Any(op => op.IsWinner) ? "WINNER" : "LOSER"
                                                         },
                                                     }).ToList();

                                    ex.IntervalLabels = intervals.Select(a => a.il).ToArray();

                                    var outputExtraction = new List<double>();
                                    foreach (var idx in intervals.Select(a => a.idx))
                                    {
                                        outputExtraction.Add(ex.Output[idx]);
                                    }

                                    ex.Output = outputExtraction.ToArray();
                                }

                                string timeSignature = DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss");
                                string nameSignature = fi.Name.Replace(".csv", string.Empty);
                                string output = projectName.ProjectAssembledBuilderExtractorWithoutScheduleDirectory(label, $"{nameSignature}.{timeSignature}.csv");

                                ExtractorService.ExtractorWrite(output, extractions, 0, 0);

                                if (!config.WithoutSchedule && config.ProjectScheduleConfigurations.Any())
                                {
                                    // America
                                    config.ProjectScheduleConfigurations.ToList().MarketStartTime(MarketRegionEnum.America,
                                        out DateTime FromTimeInSecondsAmerica, out DateTime ToTimeInSecondsAmerica);

                                    ExtractorService.ExtractorWrite(
                                        projectName.ProjectAssembledBuilderExtractorAmericaDirectory(label, $"{nameSignature}.{timeSignature}.America.csv"),
                                        indicators,
                                        FromTimeInSecondsAmerica.Hour, ToTimeInSecondsAmerica.Hour
                                    );

                                    // Europe
                                    config.ProjectScheduleConfigurations.ToList().MarketStartTime(MarketRegionEnum.Europe,
                                        out DateTime FromTimeInSecondsEurope, out DateTime ToTimeInSecondsEurope);

                                    ExtractorService.ExtractorWrite(
                                        projectName.ProjectAssembledBuilderExtractorEuropeDirectory(label, $"{nameSignature}.{timeSignature}.Europe.csv"),
                                        indicators,
                                        FromTimeInSecondsEurope.Hour, ToTimeInSecondsEurope.Hour
                                    );

                                    // Asia
                                    config.ProjectScheduleConfigurations.ToList().MarketStartTime(MarketRegionEnum.Asia,
                                        out DateTime FromTimeInSecondsAsia, out DateTime ToTimeInSecondsAsia);

                                    ExtractorService.ExtractorWrite(
                                        projectName.ProjectAssembledBuilderExtractorAsiaDirectory(label, $"{nameSignature}.{timeSignature}.Asia.csv"),
                                        indicators,
                                        FromTimeInSecondsAsia.Hour, ToTimeInSecondsAsia.Hour
                                    );
                                }
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<AssembledBuilderService>(ex);
                throw;
            }
        }

        public void Build(string projectName, ProjectConfigurationDTO config, IEnumerable<Candle> candles)
        {
            try
            {
                StartProcessing("UP");
                StartProcessing("DOWN");

                void StartProcessing(string label)
                {
                    string directory = projectName.ProjectAssembledBuilderExtractorWithoutScheduleDirectory(label);
                    IEnumerable<FileInfo> extractions = ProjectDirectoryService.GetFilesInPath(directory);
                    if (extractions.Any())
                    {
                        Parallel.ForEach(extractions,
                            new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount - 1 },
                            ext =>
                            {
                                // Weka

                                var wekaApi = new WekaApiClient();
                                IList<REPTreeOutputModel> responseWeka = wekaApi.GetREPTreeClassifier(
                                    ext.FullName,
                                    config.DepthWeka,
                                    config.TotalDecimalWeka,
                                    config.MinimalSeed, config.MaximumSeed,
                                    config.TotalInstanceWeka,
                                    (double)config.MaxRatioTree,
                                    (double)config.NTotalTree,
                                    true
                                );

                                // Strategy

                                foreach (var instance in responseWeka)
                                {
                                    var winningNodes = instance.NodeOutput.Where(m => m.Winner).ToList();
                                    winningNodes.ForEach(m =>
                                    {
                                        m.Node = new List<string>(m.Node.OrderByDescending(n => n).ToList());
                                    });

                                    foreach (var node in winningNodes)
                                    {
                                        var stb = StrategyBuilderService.BacktestBuild(label, node.Node.ToList(), config, candles.ToList(), new System.Threading.CancellationToken());

                                        // Serialization

                                        if (stb.WinningStrategy)
                                        {
                                            BacktestSerialize(projectName, stb.IS);
                                        }
                                    }
                                }
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException<AssembledBuilderService>(ex);
                throw;
            }
        }

        // Serialization

        public void BacktestSerialize(string projectName, BacktestModel model)
        {
            string directory = model.Label.ToLower() == "up"
                                        ? projectName.ProjectAssembledBuilderNodesUPDirectory()
                                        : projectName.ProjectAssembledBuilderNodesDOWNDirectory();

            SerializerHelper.XMLSerializeObject(model, string.Format(@"{0}\{1}.xml", directory,
                                            RegexHelper.GetValidFileName(model.NodeName(), "_")));
        }

        public BacktestModel BacktestDeserialize(string path)
        {
            return SerializerHelper.XMLDeSerializeObject<BacktestModel>(path);
        }
    }
}