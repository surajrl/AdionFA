using AdionFA.UI.Station.Infrastructure.Base;
using MahApps.Metro.Controls;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class MenuItemViewModel : ViewModelBase, IHamburgerMenuItemBase
    {
        private object _icon;
        private object _label;
        private object _name;
        private object _toolTip;
        private bool _isVisible = true;
        private bool _isEnable = false;

        public MenuItemViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel { get; }

        public object Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public object Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        public object Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public object ToolTip
        {
            get => _toolTip;
            set => SetProperty(ref _toolTip, value);
        }

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        public bool IsEnable
        {
            get => _isEnable;
            set => SetProperty(ref _isEnable, value);
        }
    }
}
