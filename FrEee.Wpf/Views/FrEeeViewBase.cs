using System.Windows;
using System.Windows.Controls;

namespace FrEee.Wpf.Views
{
    public abstract class FrEeeViewBase : ContentControl
    {
        static FrEeeViewBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FrEeeViewBase), new FrameworkPropertyMetadata(typeof(FrEeeViewBase)));
        }
    }
}
