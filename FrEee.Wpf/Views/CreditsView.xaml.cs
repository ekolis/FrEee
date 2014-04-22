using System.ComponentModel.Composition;
using System.Windows;
using FrEee.Wpf.ViewModels;

namespace FrEee.Wpf.Views
{
    [Export(ViewNames.CreditsView)]
    public partial class CreditsView : FrEeeViewBase
    {
        public CreditsView()
        {
            InitializeComponent();
        }
    }
}
