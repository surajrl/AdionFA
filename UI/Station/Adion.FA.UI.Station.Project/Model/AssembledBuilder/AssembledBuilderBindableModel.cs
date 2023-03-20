using Adion.FA.UI.Station.Infrastructure.Base;
using System.Collections.ObjectModel;
using System.Linq;

namespace Adion.FA.UI.Station.Project.Model.AssembledBuilder
{
    public class AssembledBuilderBindableModel : ViewModelBase
    {
        public AssembledBuilderBindableModel()
        {
            UPNodes = new ObservableCollection<NodeAssembledBindableModel>();
            DOWNNodes = new ObservableCollection<NodeAssembledBindableModel>();
        }

        public bool ChangeExpandedAll(ObservableCollection<NodeAssembledBindableModel> nodes)
        {
            bool isExpanded = false;
            if (nodes.Any())
            {
                string label = nodes.FirstOrDefault()?.Label?.ToLower();
                bool comparison = label == "up" ? IsExpandedUPNodes : IsExpandedDownNodes;
                foreach (var n in nodes)
                {
                    comparison = comparison != recursiveExpanded(n);
                    if (comparison)
                        break;
                }
                isExpanded = comparison;

                if (label == "up")
                {
                    IsExpandedUPNodes = !isExpanded;
                }
                else
                {
                    IsExpandedDownNodes = !isExpanded;
                }
            }

            foreach (var n in nodes)
            {
                recursiveExpanded(n, !isExpanded);
            }

            return isExpanded;
        
            bool recursiveExpanded(NodeAssembledBindableModel node, bool? isExpanded = null)
            {
                foreach (var n in node.Nodes)
                {
                    if (isExpanded == null)
                    {
                        bool comparison = node.IsExpanded != recursiveExpanded(n);
                        if (comparison)
                        {
                            return !comparison;
                        }
                    }
                }

                if (isExpanded != null)
                {
                    node.IsExpanded = isExpanded.Value;
                }
             
                return node.IsExpanded;
            }
        }

        #region Is Expanded
        bool _isExpandedUPNodes;
        public bool IsExpandedUPNodes
        {
            get => _isExpandedUPNodes;
            set => SetProperty(ref _isExpandedUPNodes, value);
        }


        bool _isExpandedDownNodes;
        public bool IsExpandedDownNodes
        {
            get => _isExpandedDownNodes;
            set => SetProperty(ref _isExpandedDownNodes, value);
        }
        #endregion

        #region UP Nodes
        ObservableCollection<NodeAssembledBindableModel> uPNodes;
        public ObservableCollection<NodeAssembledBindableModel> UPNodes 
        { 
            get => uPNodes;
            set => SetProperty(ref uPNodes, value); 
        }
        #endregion

        #region DOWN Nodes
        ObservableCollection<NodeAssembledBindableModel> dOWNNodes;
        public ObservableCollection<NodeAssembledBindableModel> DOWNNodes 
        {
            get => dOWNNodes;
            set => SetProperty(ref dOWNNodes, value);
        }
        #endregion
    }
}
