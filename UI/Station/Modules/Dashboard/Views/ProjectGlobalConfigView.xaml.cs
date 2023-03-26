using AdionFA.UI.Station.Module.Dashboard.ViewModels;
using MahApps.Metro.Controls;
using System.Globalization;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AdionFA.UI.Station.Module.Dashboard
{
    /// <summary>
    /// Interaction logic for SettingView.xaml
    /// </summary>
    public partial class ProjectGlobalConfigView : UserControl
    {
        public ProjectGlobalConfigView(ProjectGlobalConfigViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        #region LabelRequiredAdjustConfig

        public static readonly DependencyProperty LabelRequiredAdjustConfigProperty =
            DependencyProperty.Register("LabelRequiredAdjustConfig", typeof(string), typeof(ProjectGlobalConfigView), new UIPropertyMetadata(string.Empty));
        public string LabelRequiredAdjustConfig
        {
            get => (string)GetValue(LabelRequiredAdjustConfigProperty);
            set => SetValue(LabelRequiredAdjustConfigProperty, value);
        }

        #endregion

        #region Progressiveness

        public static readonly DependencyProperty LabelRequiredProgressivenessProperty =
            DependencyProperty.Register("LabelRequiredProgressiveness", typeof(string), typeof(ProjectGlobalConfigView), new UIPropertyMetadata(string.Empty));
        public string LabelRequiredProgressiveness
        {
            get => (string)GetValue(LabelRequiredProgressivenessProperty);
            set => SetValue(LabelRequiredProgressivenessProperty, value);
        }

        public static readonly DependencyProperty LabelRequiredAdjustProgressivenessProperty =
            DependencyProperty.Register("LabelRequiredAdjustProgressiveness", typeof(string), typeof(ProjectGlobalConfigView), new UIPropertyMetadata(string.Empty));
        public string LabelRequiredAdjustProgressiveness
        {
            get => (string)GetValue(LabelRequiredAdjustProgressivenessProperty);
            set => SetValue(LabelRequiredAdjustProgressivenessProperty, value);
        }

        public static readonly DependencyProperty EnableProgressivenessProperty =
            DependencyProperty.Register("EnableProgressiveness", typeof(bool), typeof(ProjectGlobalConfigView), new UIPropertyMetadata(false));
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
                    DataContext != null && !((ProjectGlobalConfigViewModel)DataContext).ProjectGlobalConfiguration.AutoAdjustConfig ? string.Empty : "*";

                LabelRequiredProgressiveness =
                    DataContext != null && !((ProjectGlobalConfigViewModel)DataContext).ProjectGlobalConfiguration.IsProgressiveness ? string.Empty : "*";
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
                    DataContext != null && !((ProjectGlobalConfigViewModel)DataContext).ProjectGlobalConfiguration.AutoAdjustConfig ? string.Empty : "*";

                EnableProgressiveness = IsEnableProgressiveness();
                LabelRequiredAdjustProgressiveness = !EnableProgressiveness ? string.Empty : "*";
            }
        }

        private bool IsEnableProgressiveness()
        {
            if (DataContext != null)
            {
                var dcontext = (ProjectGlobalConfigViewModel)DataContext;
                return dcontext.ProjectGlobalConfiguration.AutoAdjustConfig && dcontext.ProjectGlobalConfiguration.IsProgressiveness;
            }

            return false;
        }

    }

    #region ValidationRule

    public class DatetimeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo, BindingExpressionBase owner)
        {
            if(!DateTime.TryParse(value?.ToString(), out DateTime dt))
                return new ValidationResult(false, $"{owner.Target} - Spected Datatime");
        
            return new ValidationResult(true, null);
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
