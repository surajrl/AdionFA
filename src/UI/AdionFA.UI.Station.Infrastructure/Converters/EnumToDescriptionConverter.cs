using AdionFA.Domain.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace AdionFA.UI.Infrastructure.Converters
{
    [ValueConversion(typeof(Enum), typeof(string))]
    public class EnumToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum x)
            {
                return x.GetDescription();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
