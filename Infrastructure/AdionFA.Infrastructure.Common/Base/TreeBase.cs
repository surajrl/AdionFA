using System;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Base
{
    public interface ITree<Hierarchy, Node> where Hierarchy : ICollection<Node>
    {
        public int Level { get; set; }

        public abstract string Name { get; set; }
        public abstract string Label { get; set; }

        public abstract Node Parent { get; set; }
        public abstract Hierarchy Nodes { get; set; }
    }

    public abstract class TreeNodeBase<Hierarchy, T> : ITree<Hierarchy, T> where Hierarchy : List<T>
    {
        public string UUID { get; private set; }
        public int Level { get; set; }

        public TreeNodeBase()
        {
            Nodes = (Hierarchy)new List<T>();
            UUID = Guid.NewGuid().ToString();
        }

        public bool IsLeafNode => Nodes.Any();

        public abstract string Name { get; set; }
        public abstract string Label { get; set; }
        public T Parent { get; set; }
        public abstract Hierarchy Nodes { get; set; }
    }
}
