using System.ComponentModel.Composition.Hosting;
using System.Windows;
using FrEee.Wpf.Views;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace FrEee.Wpf.Modularity
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<ShellView>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (Bootstrapper).Assembly));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
        }
    }
}
