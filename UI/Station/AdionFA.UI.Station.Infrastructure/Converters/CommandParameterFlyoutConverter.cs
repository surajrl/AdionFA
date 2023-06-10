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
            var fmodel = new FlyoutModel();

            switch (values.Length)
            {
                case 1:
                    fmodel.FlyoutName = (string)values[0];
                    break;

                case 2:
                    fmodel.FlyoutName = (string)values[0];
                    fmodel.ModelOne = values[1];
                    break;

                case 3:
                    fmodel.FlyoutName = (string)values[0];
                    fmodel.ModelOne = values[1];
                    fmodel.ModelTwo = values[2];
                    break;
            }

            return fmodel;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
