using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Infrastructure.Model.Weka;

using Prism.Commands;

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class CorrelationFlyoutViewModel : ViewModelBase
    {
        public CorrelationFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);

            CorrelationNodesUP = new();
            CorrelationNodesDOWN = new();
        }

        private ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleCorrelation))
            {
                var correlationNodes = flyoutModel.Model != null ? (ObservableCollection<REPTreeNodeVM>)flyoutModel.Model : new ObservableCollection<REPTreeNodeVM>();

                CorrelationNodesUP.Clear();
                CorrelationNodesDOWN.Clear();

                foreach (var node in correlationNodes)
                {
                    if (node.CorrelationPass)
                    {
                        switch (node.Label.ToLower())
                        {
                            case "up":
                                CorrelationNodesUP.Add(node);
                                break;

                            case "down":
                                CorrelationNodesDOWN.Add(node);
                                break;
                        }
                    }
                }
            }
        });

        // View Bindings

        private ObservableCollection<REPTreeNodeVM> _correlationNodesUP;

        public ObservableCollection<REPTreeNodeVM> CorrelationNodesUP
        {
            get => _correlationNodesUP;
            set => SetProperty(ref _correlationNodesUP, value);
        }

        private ObservableCollection<REPTreeNodeVM> _correlationNodesDOWN;

        public ObservableCollection<REPTreeNodeVM> CorrelationNodesDOWN
        {
            get => _correlationNodesDOWN;
            set => SetProperty(ref _correlationNodesDOWN, value);
        }
    }
}