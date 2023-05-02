using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Services;
using AdionFA.UI.Station.Infrastructure.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using MahApps.Metro.IconPacks;
using System.Collections.ObjectModel;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties

        private ObservableCollection<MenuItemViewModel> _menuItems;
        private ObservableCollection<MenuItemViewModel> _menuOptionItems;

        private string projectName;
        private string projectStepName;

        private bool istransactionActive;

        #endregion Properties

        protected readonly IProjectServiceAgent _projectService;

        public MainViewModel(
            IProjectServiceAgent appProjectService)
        {
            _projectService = appProjectService;

            CreateMenuItems();
            PopulateViewModel();
        }

        public async void PopulateViewModel()
        {
            IsTransactionActive = false;
            ProjectName = "Loading...";
            ProjectVM project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true);
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

                new AssembledBuilderViewModel(this)
                {
                    Icon = new PackIconMaterialDesign()
                    {
                        Kind = PackIconMaterialDesignKind.Transform,
                        Width = 20,
                        Height = 20
                    },
                    Label = HamburgerMenuItems.AssembledBuilder,
                    Name = HamburgerMenuItems.AssembledBuilder.Replace(" ", string.Empty),
                    ToolTip = $"Open {HamburgerMenuItems.AssembledBuilder}"
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

                new ProjectSettingsViewModel(this)
                {
                    Icon = new PackIconMaterial()
                    {
                        Kind = PackIconMaterialKind.Cog,
                        Width = 20,
                        Height = 20
                    },
                    Label = HamburgerMenuItems.Settings,
                    Name = HamburgerMenuItems.Settings.Replace(" ", string.Empty),
                    ToolTip = $"Open {HamburgerMenuItems.Settings}",
                },
            };
        }

        #region Bindable Model

        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public ObservableCollection<MenuItemViewModel> MenuOptionItems
        {
            get => _menuOptionItems;
            set => SetProperty(ref _menuOptionItems, value);
        }

        public string ProjectName
        {
            get => projectName;
            set => SetProperty(ref projectName, value);
        }

        public string ProjectNameStep
        {
            get => projectStepName;
            set => SetProperty(ref projectStepName, value);
        }

        public bool IsTransactionActive
        {
            get { return istransactionActive; }
            set { SetProperty(ref istransactionActive, value); }
        }

        #endregion Bindable Model
    }
}