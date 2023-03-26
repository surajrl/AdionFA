using System;
using System.Globalization;
using System.Windows.Data;

namespace AdionFA.UI.Station.Infrastructure.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class EqualityToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
