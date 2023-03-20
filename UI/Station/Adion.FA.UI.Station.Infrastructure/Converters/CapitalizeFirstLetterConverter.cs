using System;
using System.Globalization;
using System.Windows.Data;

namespace Adion.FA.UI.Station.Infrastructure.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class CapitalizeFirstLetterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                var castValue = (string)value;
                return char.ToUpper(castValue[0]) + castValue.Substring(1);
            }
            else
            {
                return value;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
