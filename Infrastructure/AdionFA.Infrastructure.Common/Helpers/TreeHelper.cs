using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Helpers
{
    public static class TreeHelper
    {
        public static void TransitionTo<TBase, TNBase>(this TBase tbase, TNBase node, int level = 0, int branch = 0)
            where TBase : NodeAssembledModel where TNBase : NodeAssembledModel
        {
            TransitionTo(tbase, node, level, branch);

            void TransitionTo(NodeAssembledModel tbase, NodeAssembledModel node, int level = 0, int branch = 0, int innerLevel = 0)
            {
                if (innerLevel <= level)
                {
                    branch = tbase.Nodes.Count <= branch ? tbase.Nodes.Count - 1 : branch;

                    if (innerLevel == level)
                    {
                        node.Parent = tbase;
                        tbase.Nodes.Add(node);
                        return;
                    }

                    innerLevel++;

                    int i = 0;
                    while (i <= branch)
                    {
                        if (i == branch)
                        {
                            TransitionTo(tbase.Nodes[i], node, level, innerLevel: innerLevel);
                            break;
                        }
                        i++;
                    }
                }
            }
        }

        public static void InsertTo<TBase, TNBase>(this TBase tbase, TNBase node, int level = 0, int branch = 0)
            where TBase : NodeAssembledModel where TNBase : NodeAssembledModel
        {
            InsertTo(tbase, node, level, branch);

            void InsertTo(NodeAssembledModel tbase, NodeAssembledModel node, int level = 0, int branch = 0, int innerLevel = 0)
            {
                if (innerLevel <= level)
                {
                    branch = tbase.Nodes.Count < branch ? tbase.Nodes.Count - 1 : branch;

                    if (innerLevel == level)
                    {
                        node.Parent = tbase;
                        tbase.Nodes.ForEach(n =>
                        {
                            n.Parent = node;
                            node.Nodes.Add(n);
                        });

                        tbase.Nodes = new List<NodeAssembledModel>() { node };

                        return;
                    }

                    innerLevel++;

                    int i = 0;
                    while (i <= branch)
                    {
                        if (i == branch)
                        {
                            InsertTo(tbase.Nodes[i], node, level, innerLevel: innerLevel);
                            break;
                        }
                        i++;
                    }
                }
            }
        }

        public static List<TBase> ConvertTreeToList<TBase, TResult>(this TBase tbase)
            where TBase : NodeAssembledModel where TResult : NodeAssembledModel
        {
            var res = new List<TBase>();
            if (tbase != null && tbase is TResult)
            {
                res.Add(tbase);
            }
            else if (tbase.Nodes.Count == 0)
            {
                return null;
            }

            foreach (var n in tbase.Nodes)
            {
                res.AddRange(((TBase)n).ConvertTreeToList<TBase, TResult>() ?? Array.Empty<TBase>().ToList());
            }

            return res;
        }
    }
}
