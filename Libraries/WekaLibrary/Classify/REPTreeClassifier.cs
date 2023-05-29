using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using WekaLibrary.Model;

using weka.core;
using weka.core.converters;
using weka.classifiers.trees;
using java.io;

namespace WekaLibrary
{
    public class REPTreeClassifier
    {
        private List<REPTree> Trees = new List<REPTree>();
        public List<REPTreeOutputModel> Output { get; private set; }

        public const string WINNER = "WINNER";
        public const string LOSER = "LOSER";

        public REPTreeClassifier(REPTreeOptionsModel model, int maxInstances = 1, Random r = null)
        {
            r = r ?? new Random();
            while (Trees.Count < maxInstances)
            {
                if (model.Seed == 0 || maxInstances > 1)
                {
                    model.Seed = r.Next(100, 1000000);
                }
                REPTree tree = new REPTree();
                tree.setBatchSize(model.BatchSize);
                tree.setDebug(model.Debug);
                tree.setDoNotCheckCapabilities(model.DoNotCheckCapabilities);
                tree.setInitialCount(model.InitialCount);
                tree.setMaxDepth(model.MaxDepth);
                tree.setMinNum(model.MinNum);
                tree.setMinVarianceProp(model.MinVarianceProp);
                tree.setNoPruning(model.NoPruning);
                tree.setNumDecimalPlaces(model.NumDecimalPlaces);
                tree.setNumFolds(model.NumFolds);
                tree.setSeed(model.Seed);
                tree.setSpreadInitialCount(model.SpreadInitialCount);

                Trees.Add(tree);
            }
        }

        public async Task<List<REPTreeOutputModel>> BuildClassifier(string sourcePath)
        {
            try
            {
                Output = new List<REPTreeOutputModel>();

                CSVLoader loader = new CSVLoader();
                loader.setSource(new File(sourcePath));

                Instances data = loader.getDataSet();
                data.setClassIndex(data.numAttributes() - 1);//Label column

                /*Remove remove = new Remove();
                remove.setAttributeIndicesArray(new int[] { 0, 1 });
                remove.setInputFormat(data);

                data = Filter.useFilter(data, remove);*/

                Action<object> action = (object e) =>
                {
                    try
                    {
                        REPTree tree = (REPTree)e;
                        tree.buildClassifier(data);
                        string treeOutput = tree.toString();
                        REPTreeOutputModel output = new REPTreeOutputModel
                        {
                            Seed = tree.getSeed(),
                            TreeOutput = treeOutput,
                            IsSuccess = !string.IsNullOrWhiteSpace(treeOutput) ? true : false,
                            NodeOutput = new List<REPTreeNodeModel>()
                        };

                        using var sr = new System.IO.StringReader(output.TreeOutput);

                        int count = 0;
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            count++;
                            var node = BuildNode(line);
                            if (node != null)
                            {
                                output.NodeOutput.Add(node);
                            }
                        }

                        Output.Add(output);
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(ex.Message);
                        throw;
                    }
                };

                Task[] pool = Trees.Select(e => new Task(action, e)).ToArray();
                foreach (var t in pool)
                {
                    t.Start();
                }
                await Task.WhenAll(pool).ConfigureAwait(true);

                return Output;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private REPTreeNodeModel BuildNode(string node)
        {
            try
            {
                REPTreeNodeModel result = null;
                bool isDown = node.Contains(LOSER);
                bool isUp = node.Contains(WINNER);
                if (isDown || isUp)
                {
                    string pattern = isDown ? LOSER : isUp ? WINNER : string.Empty;
                    if (pattern != string.Empty)
                    {
                        string data = node.Substring(node.IndexOf(pattern)).Replace(" ", string.Empty);
                        string[] numbers = Regex.Split(data, @"\D+").Where(n => !string.IsNullOrEmpty(n)).ToArray();
                        if (numbers.Length == 4)
                        {
                            double totalError = double.Parse(numbers[1]) + double.Parse(numbers[3]);
                            double totalSuccess = double.Parse(numbers[0]) + double.Parse(numbers[2]) - totalError;
                            result = new REPTreeNodeModel
                            {
                                //Node = node,
                                TotalUP = isUp ? totalSuccess : totalError,
                                TotalDOWN = isDown ? totalSuccess : totalError,
                            };

                            result.RatioUP = result.TotalUP / result.TotalDOWN;
                            result.RatioDOWN = result.TotalDOWN / result.TotalUP;
                            result.RatioMax = result.RatioUP > result.RatioDOWN ? result.RatioUP : result.RatioDOWN;
                            result.Total = result.TotalUP + result.TotalDOWN;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
