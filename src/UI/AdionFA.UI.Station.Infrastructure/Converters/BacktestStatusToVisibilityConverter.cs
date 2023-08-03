using AdionFA.Domain.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AdionFA.UI.Infrastructure.Converters
{
    [ValueConversion(typeof(Enum), typeof(Visibility))]
    public class BacktestStatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BacktestStatus x)
            {
                return x == BacktestStatus.Completed ? Visibility.Visible : Visibility.Hidden;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
