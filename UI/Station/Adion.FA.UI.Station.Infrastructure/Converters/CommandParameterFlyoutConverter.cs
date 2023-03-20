﻿using Adion.FA.UI.Station.Infrastructure.Services;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Adion.FA.UI.Station.Infrastructure.Converters
{
    public class CommandParameterFlyoutConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type Target_Type, object Parameter, CultureInfo culture)
        {
            var fmodel = new FlyoutModel();
            fmodel.FlyoutName = (string)values[0];
            fmodel.Model = values.Length > 1 ? values[1] : null;
            return fmodel;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
