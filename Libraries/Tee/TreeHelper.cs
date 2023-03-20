using System.Collections.Generic;

namespace Tee
{
    public static class TreeHelper
    {
        public static void TransitionTo<TBase,TNBase>(this TBase tbase, TNBase node, int level = 0, int branch = 0) 
            where TBase : TreeNode where TNBase : TreeNode
        {
            TransitionTo(tbase, node, level, branch);

            void TransitionTo(TreeNode tbase, TreeNode node, int level = 0, int branch = 0, int innerLevel = 0)
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
            where TBase : TreeNode where TNBase : TreeNode
        {
            InsertTo(tbase, node, level, branch);

            void InsertTo(TreeNode tbase, TreeNode node, int level = 0, int branch = 0, int innerLevel = 0)
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

                        tbase.Nodes = new List<TreeNode>() { node };

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

        public static ObservableNode MapToTreeObservableNode(this TreeNode source)
        {
            ObservableNode node = Recursive(source);
            return node;

            ObservableNode Recursive(TreeNode source, int level = 0)
            {
                ObservableNode node = null;
                if (source != null)
                {
                    if (source is StartNodeAssembledModel)
                    {
                        node = new StartNodeAssembledBindableModel 
                        {
                           Level = level
                        };
                    }

                    if (source is EndNodeAssembledModel)
                    {
                        node = new EndNodeAssembledBindableModel
                        {
                            Level = level
                        };
                    }

                    if (source is BacktestNodeAssembledModel)
                    {
                        node = new BacktestNodeAssembledBindableModel
                        {
                            Level = level
                        };
                    }

                    level++;
                    int branch = 0;
                    while (branch < source.Nodes.Count)
                    {
                        node?.Nodes.Add(Recursive(source.Nodes[branch], level));
                        branch++;
                    }
                }

                return node;
            }
        }
    }
}
