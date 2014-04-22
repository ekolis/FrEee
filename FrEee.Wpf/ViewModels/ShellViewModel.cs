using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;

namespace FrEee.Wpf.ViewModels
{
    [Export]
    public class ShellViewModel : FrEeeViewModelBase
    {
        #region Properties
        private string _title = "FrEee";
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
