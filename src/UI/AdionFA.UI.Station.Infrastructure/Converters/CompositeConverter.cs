using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace AdionFA.UI.Infrastructure.Converters
{
    [ContentProperty("Converters")]
    public class CompositeConverter : IValueConverter
    {
        public List<IValueConverter> Converters { get; } = new List<IValueConverter>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return value;
            }
            foreach (var converter in Converters)
            {
                value = converter.Convert(value, targetType, parameter, culture);
                if (value == Binding.DoNothing)
                {
                    return Binding.DoNothing;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
