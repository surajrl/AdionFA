using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Weka;
using AdionFA.UI.Station.Infrastructure.Services;
using AdionFA.UI.Station.Project.AutoMapper;
using AutoMapper;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels.StrategyBuilder
{
    public class WekaTreeFlyoutViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;

        public WekaTreeFlyoutViewModel(IApplicationCommands applicationCommands)
        {
            applicationCommands.ShowFlyoutCommand.RegisterCommand(FlyoutCommand);
            applicationCommands.SaveNodeCommand.RegisterCommand(SaveNodeCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();
        }

        public ICommand FlyoutCommand => new DelegateCommand<FlyoutModel>(flyoutModel =>
        {
            if ((flyoutModel?.FlyoutName ?? string.Empty).Equals(FlyoutRegions.FlyoutProjectModuleWekaTree))
            {
                var projection = ((ObservableCollection<REPTreeNodeVM>)flyoutModel.Model)
                    .Where(m => m.Winner)
                    .OrderByDescending(n => n.Winner)
                    .ThenByDescending(n => n.WinningStrategy)
                    .ToList();

                projection.ForEach(m =>
                {
                    m.Node = new ObservableCollection<string>(m.Node.OrderByDescending(n => n).ToList());
                });

                Nodes = new ObservableCollection<REPTreeNodeVM>(projection);
            }
        });

        public ICommand SaveNodeCommand => new DelegateCommand<REPTreeNodeVM>(node =>
        {
            var directory = ProcessArgs.ProjectName.ProjectStrategyBuilderNodesDirectory();
            SerializerHelper.XMLSerializeObject(_mapper.Map<REPTreeNodeVM, REPTreeNodeModel>(node), string.Format(@"{0}\{1}.xml", directory, RegexHelper.GetValidFileName(node.NodeName(), "_")));
        });

        // View Bindings

        private ObservableCollection<REPTreeNodeVM> _nodes;

        public ObservableCollection<REPTreeNodeVM> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }
    }
}