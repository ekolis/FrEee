using System.ComponentModel.Composition;
using System.Windows;
using FrEee.Wpf.ViewModels;

namespace FrEee.Wpf.Views
{
    [Export]
    public partial class ShellView : FrEeeWindowBase
    {
        public ShellView()
        {
            InitializeComponent();
        }

        [Import]
        public ShellViewModel ViewModel
        {
            get { return DataContext as ShellViewModel; }
            set { DataContext = value; }
        }
    }
}
