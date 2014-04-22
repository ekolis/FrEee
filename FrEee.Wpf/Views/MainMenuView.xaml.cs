using System.ComponentModel.Composition;
using System.Windows;
using FrEee.Wpf.ViewModels;

namespace FrEee.Wpf.Views
{
    [Export(ViewNames.MainMenuView)]
    public partial class MainMenuView : FrEeeViewBase
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        [Import]
        public MainMenuViewModel ViewModel
        {
            get { return DataContext as MainMenuViewModel; }
            set { DataContext = value; }
        }
    }
}
