using AdionFA.UI.Station.Infrastructure.Services;
using System;
using System.Globalization;
using System.Windows.Data;

namespace AdionFA.UI.Station.Infrastructure.Converters
{
    public class CommandParameterFlyoutConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type Target_Type, object Parameter, CultureInfo culture)
        {
            return new FlyoutModel
            {
                Name = (string)values[0],
                ModelOne = values.Length > 1 ? values[1] : null,
                ModelTwo = values.Length > 2 ? values[2] : null
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
