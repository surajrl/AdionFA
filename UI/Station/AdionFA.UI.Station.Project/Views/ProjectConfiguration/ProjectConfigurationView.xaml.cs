using AdionFA.UI.Station.Project.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

namespace AdionFA.UI.Station.Project.Views
{
    /// <summary>
    /// Interaction logic for ProjectSettingView.xaml
    /// </summary>
    public partial class ProjectConfigurationView : UserControl
    {
        public ProjectConfigurationView()
        {
            InitializeComponent();
        }

        // Progressiveness

        public static readonly DependencyProperty LabelRequiredProgressivenessProperty =
            DependencyProperty.Register("LabelRequiredProgressiveness", typeof(string), typeof(ProjectConfigurationView), new UIPropertyMetadata(string.Empty));
        public string LabelRequiredProgressiveness
        {
            get => (string)GetValue(LabelRequiredProgressivenessProperty);
            set => SetValue(LabelRequiredProgressivenessProperty, value);
        }

        public static readonly DependencyProperty EnableProgressivenessProperty =
            DependencyProperty.Register("EnableProgressiveness", typeof(bool), typeof(ProjectConfigurationView), new UIPropertyMetadata(false));
        public bool EnableProgressiveness
        {
            get => (bool)GetValue(EnableProgressivenessProperty);
            set => SetValue(EnableProgressivenessProperty, value);
        }

        private void ProgressivenessToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                LabelRequiredProgressiveness =
                    DataContext != null && !((ProjectConfigurationViewModel)DataContext).ProjectConfiguration.IsProgressiveness ? string.Empty : "*";
                EnableProgressiveness = IsEnableProgressiveness();
            }
        }

        private void AdjustConfigToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                EnableProgressiveness = IsEnableProgressiveness();
            }
        }

        private bool IsEnableProgressiveness()
        {
            if (DataContext != null)
            {
                var dcontext = (ProjectConfigurationViewModel)DataContext;
                return dcontext.ProjectConfiguration.IsProgressiveness;
            }

            return false;
        }
    }
}
