using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Practices.ServiceLocation;

namespace FrEee.Wpf.ViewModels
{
    public abstract class FrEeeViewModelBase : INotifyPropertyChanged, INavigationAware
    {
        #region Public Properties
        [Import]
        protected IServiceLocator ServiceLocator { get; private set; }
        [Import]
        protected IEventAggregator EventAggregator { get; private set; }
        #endregion

        #region Virtual Methods
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
