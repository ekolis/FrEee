using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FrEee.Wpf
{
    /// <summary>
    /// Used to gray-out older log entries. Should be a better way, but will do for now.
    /// </summary>
    [ValueConversion(typeof(int), typeof(bool))]
    public class FunnyLogEntryFreshnessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                if ((int)value - Game.Objects.Space.Galaxy.Current.TurnNumber == 0) return true;
                else return false;
            }

            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
