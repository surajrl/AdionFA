using AdionFA.Infrastructure.Enums;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using System.Collections.ObjectModel;

namespace AdionFA.UI.Station.Project.Model.AssembledBuilder
{
    public abstract class NodeAssembledBindableModel : TreeNodeObservable<ObservableCollection<NodeAssembledBindableModel>, NodeAssembledBindableModel>
    {
        #region Ctor
        public NodeAssembledBindableModel()
        {
            Nodes = new ObservableCollection<NodeAssembledBindableModel>();
        }
        #endregion

        public override string Label { get; set; }
        public override string Name { get; set; }

        public string TypeName => Type.GetMetadata()?.Code ?? string.Empty;
        public NodeAssembledTypeEnum Type { get; set; }

        public override ObservableCollection<NodeAssembledBindableModel> Nodes { get; set; }
    }
}
