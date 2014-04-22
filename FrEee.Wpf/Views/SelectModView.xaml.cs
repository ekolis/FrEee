using System.ComponentModel.Composition;
using System.Windows;
using FrEee.Wpf.ViewModels;

namespace FrEee.Wpf.Views
{
    [Export(ViewNames.SelectModView)]
    public partial class SelectModView : FrEeeViewBase
    {
        public SelectModView()
        {
            InitializeComponent();
        }

        [Import]
        public SelectModViewModel ViewModel
        {
            get { return DataContext as SelectModViewModel; }
            set { DataContext = value; }
        }
    }
}
