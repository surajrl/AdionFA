using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF
{
    /// <summary>
    /// Interaction logic for BindValidation.xaml
    /// </summary>
    public partial class BindValidation : Window, IView
    {
        public BindValidation()
        {
            InitializeComponent();
            DataContext = new MyDataSource();
            (DataContext as MyDataSource).View = new Lazy<IView>(this);
        }


        private void UseCustomHandler(object sender, RoutedEventArgs e)
        {
            var myBindingExpression = textBox3.GetBindingExpression(TextBox.TextProperty);
            var myBinding = myBindingExpression.ParentBinding;
            myBinding.UpdateSourceExceptionFilter = ReturnExceptionHandler;
            myBindingExpression.UpdateSource();
        }

        private void DisableCustomHandler(object sender, RoutedEventArgs e)
        {
            // textBox3 is an instance of a TextBox
            // the TextProperty is the data-bound dependency property
            var myBinding = BindingOperations.GetBinding(textBox3, TextBox.TextProperty);
            myBinding.UpdateSourceExceptionFilter -= ReturnExceptionHandler;
            BindingOperations.GetBindingExpression(textBox3, TextBox.TextProperty).UpdateSource();
        }

        private object ReturnExceptionHandler(object bindingExpression, Exception exception) => "This is from the UpdateSourceExceptionFilterCallBack.";

        public Control GetControl(string name)
        {
            return null;
        }
    }

    public interface IView
    {
        Control GetControl(string name);
    }

    public class MyDataSource
    {
        public Lazy<IView> View { get; set; }

        public MyDataSource()
        {
            Age = 0;
            Age2 = 0;
        }

        public int Age { get; set; }
        public int Age2 { get; set; }

        private int age3;
        public int Age3
        { 
            get => age3;
            set
            {
                age3 = value;
            }
        }
    }

    public class AgeRangeRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

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
