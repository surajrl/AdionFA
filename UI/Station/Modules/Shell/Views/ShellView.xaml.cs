﻿using AdionFA.UI.Station.Module.Shell.ViewModels;
using System.Windows.Controls;

namespace AdionFA.UI.Station.Module.Shell
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : UserControl
    {
        public ShellView(ShellViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
