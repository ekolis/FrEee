using SimpleMvvmToolkit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrEee.Wpf
{
    /// <summary>
    /// Interaction logic for GameLogView.xaml
    /// </summary>
    public partial class GameLogView : UserControl
    {
        public GameLogView()
        {
            InitializeComponent();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBus.Default.Notify("LogMessageDoubleClicked", this, new NotificationEventArgs());
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class TurnNumberToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return (2400 + (int)value).ToString("F1");
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
