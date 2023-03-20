using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Adion.FA.UI.Station
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Splash : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public Splash()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(dt_tick);
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Start();
        }

        private void dt_tick(object sender, EventArgs e)
        {
            Shell s = new Shell(ContainerLocator.Current.Resolve<IRegionManager>());
            s.Show();

            timer.Stop();
            this.Close();
        }
    }
}
