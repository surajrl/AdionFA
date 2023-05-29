﻿using AdionFA.UI.Station.Module.Dashboard.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AdionFA.UI.Station.Module.Dashboard
{
    /// <summary>
    /// Interaction logic for ProjectGlobalConfigurationView.xaml
    /// </summary>
    public partial class ProjectGlobalConfigurationView : UserControl
    {
        public ProjectGlobalConfigurationView(ProjectGlobalConfigurationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        // Progressiveness

        public static readonly DependencyProperty LabelRequiredProgressivenessProperty =
            DependencyProperty.Register("LabelRequiredProgressiveness", typeof(string), typeof(ProjectGlobalConfigurationView), new UIPropertyMetadata(string.Empty));

        public string LabelRequiredProgressiveness
        {
            get => (string)GetValue(LabelRequiredProgressivenessProperty);
            set => SetValue(LabelRequiredProgressivenessProperty, value);
        }

        public static readonly DependencyProperty EnableProgressivenessProperty =
            DependencyProperty.Register("EnableProgressiveness", typeof(bool), typeof(ProjectGlobalConfigurationView), new UIPropertyMetadata(false));

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
                    && !((ProjectGlobalConfigurationViewModel)DataContext).ProjectGlobalConfiguration.IsProgressiveness
                    ? string.Empty
                    : "*";

                EnableProgressiveness = IsEnableProgressiveness();
            }
        }

        private bool IsEnableProgressiveness()
        {
            if (DataContext != null)
            {
                var dcontext = (ProjectGlobalConfigurationViewModel)DataContext;
                return dcontext.ProjectGlobalConfiguration.IsProgressiveness;
            }

            return false;
        }

    }

    // Validation Rule

    public class DatetimeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo, BindingExpressionBase owner)
        {
            if (!DateTime.TryParse(value?.ToString(), out DateTime dt))
            {
                return new ValidationResult(false, $"{owner.Target} - Spected Datatime");
            }

            return new ValidationResult(true, null);
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
