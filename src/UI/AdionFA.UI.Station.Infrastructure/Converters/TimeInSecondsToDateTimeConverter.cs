using System;
using System.Globalization;
using System.Windows.Data;

namespace AdionFA.UI.Infrastructure.Converters
{
    [ValueConversion(typeof(int), typeof(DateTime))]
    public class TimeInSecondsToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int timeInSeconds)
            {
                return DateTime.MinValue.AddSeconds(timeInSeconds);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
