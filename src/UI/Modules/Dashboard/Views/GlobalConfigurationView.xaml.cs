using AdionFA.UI.Module.Dashboard.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

namespace AdionFA.UI.Module.Dashboard
{
    /// <summary>
    /// Interaction logic for GlobalConfigurationView.xaml
    /// </summary>
    public partial class GlobalConfigurationView : UserControl
    {
        public GlobalConfigurationView(GlobalConfigurationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        // Progressiveness

        public static readonly DependencyProperty LabelRequiredProgressivenessProperty =
            DependencyProperty.Register("LabelRequiredProgressiveness", typeof(string), typeof(GlobalConfigurationView), new UIPropertyMetadata(string.Empty));

        public string LabelRequiredProgressiveness
        {
            get => (string)GetValue(LabelRequiredProgressivenessProperty);
            set => SetValue(LabelRequiredProgressivenessProperty, value);
        }

        public static readonly DependencyProperty EnableProgressivenessProperty =
            DependencyProperty.Register("EnableProgressiveness", typeof(bool), typeof(GlobalConfigurationView), new UIPropertyMetadata(false));

        public bool EnableProgressiveness
        {
            get => (bool)GetValue(EnableProgressivenessProperty);
            set => SetValue(EnableProgressivenessProperty, value);
        }

        private void ProgressivenessToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch)
            {
                LabelRequiredProgressiveness =
                    DataContext != null
                    && !((GlobalConfigurationViewModel)DataContext).GlobalConfiguration.IsProgressiveness
                    ? string.Empty
                    : "*";

                EnableProgressiveness = IsEnableProgressiveness();
            }
        }

        private bool IsEnableProgressiveness()
        {
            if (DataContext != null)
            {
                var dcontext = (GlobalConfigurationViewModel)DataContext;
                return dcontext.GlobalConfiguration.IsProgressiveness;
            }

            return false;
        }
    }
}
