using System.ComponentModel.Composition;
using System.Windows;
using FrEee.Wpf.ViewModels;

namespace FrEee.Wpf.Views
{
    [Export(ViewNames.GameSetupView)]
    public partial class GameSetupView : FrEeeViewBase
    {
        public GameSetupView()
        {
            InitializeComponent();
        }

        [Import]
        public GameSetupViewModel ViewModel
        {
            get { return DataContext as GameSetupViewModel; }
            set { DataContext = value; }
        }
    }
}
