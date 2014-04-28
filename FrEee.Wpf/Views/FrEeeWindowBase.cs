using System.Windows;
using System.Windows.Controls;

namespace FrEee.Wpf.Views
{
    public abstract class FrEeeWindowBase : Window
    {
        static FrEeeWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FrEeeWindowBase), new FrameworkPropertyMetadata(typeof(FrEeeWindowBase)));
        }
    }
}
