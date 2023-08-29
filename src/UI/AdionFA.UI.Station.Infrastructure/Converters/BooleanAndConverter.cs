﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace AdionFA.UI.Infrastructure.Converters
{
    public class BooleanAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(v => ReferenceEquals(v, DependencyProperty.UnsetValue)))
            {
                return DependencyProperty.UnsetValue;
            }

            return values.All(System.Convert.ToBoolean);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
