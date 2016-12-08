using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FrEee.Wpf
{
    /// <summary>
    /// Used to convert engine turn numbers into user's date, for example 15 -> 2401.5.
    /// </summary>
    [ValueConversion(typeof(int), typeof(string))]
    public class TurnNumberToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return (2400 + ((int)value - 1) / 10.0).ToString("F1");
            }
            else
            {
                return "?wrong turn?";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
