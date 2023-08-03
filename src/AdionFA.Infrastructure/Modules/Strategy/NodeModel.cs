﻿using AdionFA.Infrastructure.Weka.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdionFA.Infrastructure.Modules.Strategy
{
    public class NodeModel : NodeMetadataModel
    {
        private readonly Guid _guid;

        public NodeModel()
        {
            _guid = Guid.NewGuid();
        }

        // Node

        public REPTreeNodeModel NodeData { get; set; }

        public string Name
        {
            get
            {
                var indicators = new List<string>();

                NodeData.Node.ForEach(n =>
                {
                    var f = n.Replace("|", string.Empty).Replace(" ", string.Empty);
                    string[] divisions = null;

                    // Operator Split

                    if (f.Contains(">="))
                    {
                        divisions = f.Split(">=");
                    }
                    else if (f.Contains("<="))
                    {
                        divisions = f.Split("<=");
                    }
                    else if (f.Contains('>'))
                    {
                        divisions = f.Split('>');
                    }
                    else if (f.Contains('<'))
                    {
                        divisions = f.Split('<');
                    }
                    else if (f.Contains('='))
                    {
                        divisions = f.Split('=');
                    }

                    var name = divisions[0].Split("_")[0].Replace(".", string.Empty);

                    var last = indicators.LastOrDefault();
                    if (last != null)
                    {
                        var lastName = last.Split(".")[0];
                        var lastCount = 1;
                        if (last.Contains('.'))
                        {
                            lastCount = int.Parse(last.Split(".")[1], CultureInfo.InvariantCulture);
                        }
                        if (name == lastName)
                        {
                            indicators[^1] = string.Concat(name, ".", lastCount + 1);
                        }
                        else
                        {
                            indicators.Add(name);
                        }
                    }
                    else
                    {
                        indicators.Add(name);
                    }
                });

                var indicatorsName = string.Join("_", indicators);
                var name = $"{indicatorsName}-{NodeData.Label}-{_guid}";

                return name;
            }
        }
    }
}