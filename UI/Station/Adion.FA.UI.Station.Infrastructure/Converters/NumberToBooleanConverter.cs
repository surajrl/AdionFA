using System;
using System.Windows.Data;

namespace Adion.FA.UI.Station.Infrastructure.Converters
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class NumberToBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool) && targetType != typeof(System.Windows.Visibility))
                throw new InvalidOperationException("The target must be a boolean");
            bool result = (int)value > 0 ? true : false;
            return result; 
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
