using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using FrEee.Modding;
using FrEee.Modding.Loaders;
using FrEee.Utility;
using Microsoft.Practices.Prism.Regions;

namespace FrEee.Wpf.ViewModels
{
    [Export]
    public class SelectModViewModel : FrEeeViewModelBase
    {
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            // load modinfos
            var stock = new Mod();
            var loader = new ModInfoLoader(null);
            loader.Load(stock);
            ModInfos = new ObservableCollection<ModInfo>();
            ModInfos.Add(stock.Info);

            // TODO abstract directories away
            if (Directory.Exists("Mods"))
            {
                foreach (var folder in Directory.GetDirectories("Mods"))
                {
                    loader.ModPath = Path.GetFileName(folder);
                    var mod = new Mod();
                    loader.Load(mod);
                    ModInfos.Add(mod.Info);
                }
            }
        }

        private ObservableCollection<ModInfo> _modInfos;
        public ObservableCollection<ModInfo> ModInfos
        {
            get { return _modInfos; }
            set { _modInfos = value; OnPropertyChanged(); }
        }

        private ModInfo _selectedModInfo;
        public ModInfo SelectedModInfo
        {
            get { return _selectedModInfo; }
            set { _selectedModInfo = value; OnPropertyChanged(); }
        }
    }
}
