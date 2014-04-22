using FrEee.Wpf.ViewModels;
using System.ComponentModel.Composition;

namespace FrEee.Wpf.Views
{
    [Export(ViewNames.GameView)]
    public partial class GameView : FrEeeViewBase
    {
        public GameView()
        {
            InitializeComponent();
        }

        [Import]
        public GameViewModel ViewModel
        {
            get { return DataContext as GameViewModel; }
            set { DataContext = value; }
        }
    }
}
