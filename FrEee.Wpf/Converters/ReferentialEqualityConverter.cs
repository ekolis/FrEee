using System;
using System.Windows.Data;

namespace FrEee.Wpf.Converters
{
    /// <summary>
    /// Return a boolean value representing whether or not two objects are the same instance.
    /// </summary>
    public class ReferentialEqualityConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = values[0] == values[1];
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
