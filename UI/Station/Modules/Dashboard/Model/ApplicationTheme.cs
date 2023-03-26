using Prism.Mvvm;
using System.Windows.Media;

namespace AdionFA.UI.Station.Module.Dashboard.Model
{
    public class ApplicationTheme : BindableBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetProperty<string>(ref this.name, value); }
        }


        private Brush colorBrush;
        public Brush ColorBrush
        {
            get { return colorBrush; }
            set { this.SetProperty<Brush>(ref this.colorBrush, value); }
        }


        private Brush borderColorBrush;
        public Brush BorderColorBrush
        {
            get { return borderColorBrush; }
            set { this.SetProperty<Brush>(ref this.borderColorBrush, value); }
        }
    }
}
