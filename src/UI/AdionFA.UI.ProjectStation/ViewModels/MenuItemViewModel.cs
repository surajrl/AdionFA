using AdionFA.UI.Infrastructure.Base;
using MahApps.Metro.Controls;

namespace AdionFA.UI.ProjectStation.ViewModels
{
    public class MenuItemViewModel : ViewModelBase, IHamburgerMenuItemBase
    {
        public MenuItemViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel { get; }

        private object _icon;
        public object Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private object _label;
        public object Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private object _name;
        public object Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private object _toolTip;
        public object ToolTip
        {
            get => _toolTip;
            set => SetProperty(ref _toolTip, value);
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        private bool _isEnable;
        public bool IsEnable
        {
            get => _isEnable;
            set => SetProperty(ref _isEnable, value);
        }
    }
}
