using Prism.Mvvm;
using System.Windows.Media;

namespace AdionFA.UI.Module.Dashboard.Model
{
    public class ApplicationTheme : BindableBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }


        private Brush _colorBrush;
        public Brush ColorBrush
        {
            get => _colorBrush;
            set => SetProperty(ref _colorBrush, value);
        }


        private Brush _borderColorBrush;
        public Brush BorderColorBrush
        {
            get => _borderColorBrush;
            set => SetProperty(ref _borderColorBrush, value);
        }
    }
}
