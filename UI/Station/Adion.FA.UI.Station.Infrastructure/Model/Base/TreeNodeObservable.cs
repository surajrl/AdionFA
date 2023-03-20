using Adion.FA.Infrastructure.Common.Base;
using Adion.FA.UI.Station.Infrastructure.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Adion.FA.UI.Station.Infrastructure.Model.Base
{
    public abstract class TreeNodeObservable<Hierarchy, T> : ViewModelBase, ITree<Hierarchy, T> where Hierarchy : ObservableCollection<T>
    {
        #region Expanded
        bool _isExpanded;

        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }
        #endregion

        public string UUID { get; private set; }
        public int Level { get; set; }

        public TreeNodeObservable()
        {
            Nodes = (Hierarchy)new ObservableCollection<T>();
            UUID = Guid.NewGuid().ToString();
        }

        public bool IsLeafNode => Nodes.Any();

        public abstract string Name { get; set; }
        public abstract string Label { get; set; }
        public T Parent { get; set; }
        public abstract Hierarchy Nodes { get; set; }
    }
}
