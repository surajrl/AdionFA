using AdionFA.Application.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.UI.Infrastructure.Base;
using AdionFA.UI.ProjectStation.Features;
using MahApps.Metro.IconPacks;
using Ninject;
using System.Collections.ObjectModel;

namespace AdionFA.UI.ProjectStation.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IProjectAppService _projectService;

        public MainViewModel()
        {
            _projectService = IoC.Kernel.Get<IProjectAppService>();

            CreateMenuItems();
            PopulateViewModel();
        }

        public void PopulateViewModel()
        {
            IsTransactionActive = false;

            ProjectName = "Loading...";

            var project = _projectService.GetProject(ProcessArgs.ProjectId, true);
            if (project != null)
            {
                ProjectName = project.ProjectName;
                IsTransactionActive = true;
            }
        }

        private void CreateMenuItems()
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new ExtractorViewModel(this)
                {
                    Icon = new PackIconModern()
                    {
                        Kind = PackIconModernKind.New,
                        Width = 20,
                        Height = 20
                    },
                    Label = HamburgerMenuItems.Extractor,
                    Name = HamburgerMenuItems.Extractor.Replace(" ", string.Empty),
                    ToolTip = $"Open {HamburgerMenuItems.Extractor}"
                },

                new StrategyBuilderViewModel(this)
                {
                    Icon = new PackIconMaterial()
                    {
                        Kind = PackIconMaterialKind.CogTransferOutline,
                        Width = 20,
                        Height = 20
                    },
                    Label = HamburgerMenuItems.StrategyBuilder,
                    Name = HamburgerMenuItems.StrategyBuilder.Replace(" ", string.Empty),
                    ToolTip = $"Open {HamburgerMenuItems.StrategyBuilder}"
                },

                new AssemblyBuilderViewModel(this)
                {
                    Icon = new PackIconMaterialDesign()
                    {
                        Kind = PackIconMaterialDesignKind.Transform,
                        Width = 20,
                        Height = 20
                    },
                    Label = HamburgerMenuItems.AssemblyBuilder,
                    Name = HamburgerMenuItems.AssemblyBuilder.Replace(" ", string.Empty),
                    ToolTip = $"Open {HamburgerMenuItems.AssemblyBuilder}"
                },

                new CrossingBuilderViewModel(this)
                {
                    Icon = new PackIconMaterial()
                    {
                        Kind = PackIconMaterialKind.Cross,
                        Width = 20,
                        Height = 20
                    },
                    Label = HamburgerMenuItems.CrossingBuilder,
                    Name = HamburgerMenuItems.CrossingBuilder.Replace(" ", string.Empty),
                    ToolTip = $"Open {HamburgerMenuItems.CrossingBuilder}"
                }
            };

            MenuOptionItems = new ObservableCollection<MenuItemViewModel>
            {
                new MetaTraderViewModel(this)
                {
                    Icon = new PackIconMaterial()
                    {
                        Kind = PackIconMaterialKind.ChartBar,
                        Width = 20,
                        Height = 20
                    },
                    Label = HamburgerMenuItems.MetaTrader,
                    Name = HamburgerMenuItems.MetaTrader.Replace(" ", string.Empty),
                    ToolTip = $"Open {HamburgerMenuItems.MetaTrader}",
                },

                new ProjectConfigurationViewModel(this)
                {
                    Icon = new PackIconMaterial()
                    {
                        Kind = PackIconMaterialKind.Cog,
                        Width = 20,
                        Height = 20
                    },
                    Label = HamburgerMenuItems.ProjectConfiguration,
                    Name = HamburgerMenuItems.ProjectConfiguration.Replace(" ", string.Empty),
                    ToolTip = $"Open {HamburgerMenuItems.ProjectConfiguration}",
                },
            };
        }

        // View Bindings

        private ObservableCollection<MenuItemViewModel> _menuItems;
        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        private ObservableCollection<MenuItemViewModel> _menuOptionItems;
        public ObservableCollection<MenuItemViewModel> MenuOptionItems
        {
            get => _menuOptionItems;
            set => SetProperty(ref _menuOptionItems, value);
        }

        private string _projectName;
        public string ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }
    }
}