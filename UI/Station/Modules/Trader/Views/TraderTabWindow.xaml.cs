using Dragablz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdionFA.UI.Station.Modules.Trader.Views
{
    /// <summary>
    /// Interaction logic for TraderTabWindow.xaml
    /// </summary>
    public partial class TraderTabWindow : Window
    {
        public TraderTabWindow()
        {
            InitializeComponent();
        }

        public TabablzControl TabablzControl { get => InitialTabablzControl; }
    }
}
