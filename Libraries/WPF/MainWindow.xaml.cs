using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            CommandBindings.Add(((MainWindowViewModel)DataContext).ValidateCommandBinding);

            //var myBindingExpression = txtAge.GetBindingExpression(TextBox.TextProperty);
            //var myBinding = myBindingExpression.ParentBinding;
            //myBinding.UpdateSourceExceptionFilter = ReturnExceptionHandler;
            //myBindingExpression.UpdateSource();
        }

        private void UseCustomHandler(object sender, RoutedEventArgs e)
        {
            //var myBindingExpression = txtAge.GetBindingExpression(TextBox.TextProperty);
            //var myBinding = myBindingExpression.ParentBinding;
            //myBinding.UpdateSourceExceptionFilter = ReturnExceptionHandler;
            //myBindingExpression.UpdateSource();
        }

        private void DisableCustomHandler(object sender, RoutedEventArgs e)
        {
            //// textBox3 is an instance of a TextBox
            //// the TextProperty is the data-bound dependency property
            //var myBinding = BindingOperations.GetBinding(txtAge, TextBox.TextProperty);
            //myBinding.UpdateSourceExceptionFilter -= ReturnExceptionHandler;
            //BindingOperations.GetBindingExpression(txtAge, TextBox.TextProperty).UpdateSource();
        }

        private object ReturnExceptionHandler(object bindingExpression, Exception exception) => "This is from the UpdateSourceExceptionFilterCallBack.";
    }

    

    public class AgeRule : ValidationRule
    {
        public int Min { get; set; } = 10;
        public int Max { get; set; } = 20;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var age = 0;

            try
            {
                if (((string)value).Length > 0)
                    age = int.Parse((string)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((age < Min) || (age > Max))
            {
                return new ValidationResult(false,
                    "Please enter an age in the range: " + Min + " - " + Max + ".");
            }
            return new ValidationResult(true, null);
        }
    }
}
