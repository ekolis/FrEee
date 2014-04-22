using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace FrEee.Wpf.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FrEeeViewController : IPartImportsSatisfiedNotification
    {
        [Import]
        protected IServiceLocator ServiceLocator { get; private set; }
        [Import]
        protected IEventAggregator EventAggregator { get; private set; }
        [Import]
        protected IRegionManager RegionManager { get; private set; }
        [Import]
        protected ILoggerFacade Logger { get; private set; }

        public virtual void OnImportsSatisfied()
        {
            RegionManager.Regions[RegionNames.MainRegion].RequestNavigate(ViewNames.MainMenuView);
        }

        protected void NavigateTo(string viewName, string regionName = RegionNames.MainRegion, Action<NavigationResult> navigationCallback = null)
        {
            var region = RegionManager.Regions.SingleOrDefault(_ => _.Name == regionName);
            if (region != null)
            {
                region.RequestNavigate(new Uri(viewName, UriKind.Relative), navigationCallback ?? (_ => { }));
            }
        }
    }
}
