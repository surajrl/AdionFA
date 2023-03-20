using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Tee
{
    public interface ITree<Hierarchy, Node> where Hierarchy : ICollection<Node> 
    {
        public int Branch { get; set; }
        public int Level { get; set; }
        
        public abstract string Name { get; set; }
        public abstract string Label { get; set; }

        public abstract Node Parent { get; set; }
        public abstract Hierarchy Nodes { get; set; }
    }

    public abstract class TreeNodeBase<Hierarchy, T> : ITree<Hierarchy, T> where Hierarchy : List<T>
    {
        public string UUID { get; private set; }
        public int Branch { get; set; }
        public int Level { get; set; }

        public TreeNodeBase()
        {
            Nodes = (Hierarchy)new List<T>();
            UUID = Guid.NewGuid().ToString();
        }

        public bool IsLeafNode => Nodes.Any();

        public abstract string Name { get; set; }
        public abstract string Label { get; set; }
        public abstract T Parent { get; set; }
        public abstract Hierarchy Nodes { get; set; }
    }

    public abstract class TreeNodeObservable<Hierarchy, T> : ITree<Hierarchy, T> where Hierarchy : ObservableCollection<T>
    {
        public string UUID { get; private set; }
        public int Branch { get; set; }
        public int Level { get; set; }

        public TreeNodeObservable()
        {
            Nodes = (Hierarchy)new ObservableCollection<T>();
            UUID = Guid.NewGuid().ToString();
        }

        public bool IsLeafNode => Nodes.Any();

        public abstract string Name { get; set; }
        public abstract string Label { get; set; }
        public abstract T Parent { get; set; }
        public abstract Hierarchy Nodes { get; set; }
    }
}
