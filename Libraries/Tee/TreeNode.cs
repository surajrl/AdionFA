using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tee
{
    public class TreeNode : TreeNodeBase<List<TreeNode>, TreeNode>
    {
        public override string Name { get; set; }
        public override string Label { get; set; }
        public override TreeNode Parent { get; set; }
        public override List<TreeNode> Nodes { get; set; }
    }

    public class StartNodeAssembledModel : TreeNode
    {
    }

    public class EndNodeAssembledModel : TreeNode
    {
    }

    public class BacktestNodeAssembledModel : TreeNode
    {
    }

    public class ObservableNode : TreeNodeObservable<ObservableCollection<ObservableNode>, ObservableNode>
    {
        public override string Name { get; set; }
        public override string Label { get; set; }
        public override ObservableNode Parent { get; set; }
        public override ObservableCollection<ObservableNode> Nodes { get; set; }
    }

    public class StartNodeAssembledBindableModel : ObservableNode
    {
    }

    public class EndNodeAssembledBindableModel : ObservableNode
    {
    }

    public class BacktestNodeAssembledBindableModel : ObservableNode
    {
    }
}
