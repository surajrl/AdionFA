using Adion.FA.UI.Station.Project.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

namespace Adion.FA.UI.Station.Project.Views
{
    /// <summary>
    /// Interaction logic for ProjectSettingView.xaml
    /// </summary>
    public partial class ProjectSettingView : UserControl
    {
        public ProjectSettingView()
        {
            InitializeComponent();
        }

        #region LabelRequiredAdjustConfig

        public static readonly DependencyProperty LabelRequiredAdjustConfigProperty =
            DependencyProperty.Register("LabelRequiredAdjustConfig", typeof(string), typeof(ProjectSettingView), new UIPropertyMetadata(string.Empty));
        public string LabelRequiredAdjustConfig
        {
            get => (string)GetValue(LabelRequiredAdjustConfigProperty);
            set => SetValue(LabelRequiredAdjustConfigProperty, value);
        }

        #endregion

        #region Progressiveness
        
        public static readonly DependencyProperty LabelRequiredProgressivenessProperty =
            DependencyProperty.Register("LabelRequiredProgressiveness", typeof(string), typeof(ProjectSettingView), new UIPropertyMetadata(string.Empty));
        public string LabelRequiredProgressiveness
        {
            get => (string)GetValue(LabelRequiredProgressivenessProperty);
            set => SetValue(LabelRequiredProgressivenessProperty, value);
        }

        public static readonly DependencyProperty LabelRequiredAdjustProgressivenessProperty =
            DependencyProperty.Register("LabelRequiredAdjustProgressiveness", typeof(string), typeof(ProjectSettingView), new UIPropertyMetadata(string.Empty));
        public string LabelRequiredAdjustProgressiveness
        {
            get => (string)GetValue(LabelRequiredAdjustProgressivenessProperty);
            set => SetValue(LabelRequiredAdjustProgressivenessProperty, value);
        }

        public static readonly DependencyProperty EnableProgressivenessProperty =
            DependencyProperty.Register("EnableProgressiveness", typeof(bool), typeof(ProjectSettingView), new UIPropertyMetadata(false));
        public bool EnableProgressiveness
        {
            get => (bool)GetValue(EnableProgressivenessProperty);
            set => SetValue(EnableProgressivenessProperty, value);
        }
        
        #endregion

        private void ProgressivenessToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                LabelRequiredAdjustConfig =
                    DataContext != null && !((ProjectSettingsViewModel)DataContext).ProjectConfiguration.AutoAdjustConfig ? string.Empty : "*";

                LabelRequiredProgressiveness =
                    DataContext != null && !((ProjectSettingsViewModel)DataContext).ProjectConfiguration.IsProgressiveness ? string.Empty : "*";
                EnableProgressiveness = IsEnableProgressiveness();
                LabelRequiredAdjustProgressiveness = !EnableProgressiveness ? string.Empty : "*";
            }
        }

        private void AdjustConfigToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                LabelRequiredAdjustConfig = 
                    DataContext != null && !((ProjectSettingsViewModel)DataContext).ProjectConfiguration.AutoAdjustConfig ? string.Empty : "*";

                EnableProgressiveness = IsEnableProgressiveness();
                LabelRequiredAdjustProgressiveness = !EnableProgressiveness ? string.Empty : "*";
            }
        }

        private bool IsEnableProgressiveness()
        {
            if (DataContext != null)
            {
                var dcontext = (ProjectSettingsViewModel)DataContext;
                return dcontext.ProjectConfiguration.AutoAdjustConfig && dcontext.ProjectConfiguration.IsProgressiveness;
            }

            return false;
        }
    }
}
