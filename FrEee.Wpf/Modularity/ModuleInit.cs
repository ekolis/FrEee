using FrEee.Wpf.Controllers;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace FrEee.Wpf.Modularity
{
    [Module(ModuleName = "FrEee.Wpf", OnDemand = false)]
    [ModuleExport("FrEee.Wpf", typeof(ModuleInfo))]
    public class ModuleInfo : IModule
    {
        public void Initialize()
        {
            ServiceLocator.Current.GetInstance<FrEeeViewController>();
        }
    }
}
