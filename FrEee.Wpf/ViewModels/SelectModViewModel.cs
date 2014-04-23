using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using FrEee.Modding;
using FrEee.Modding.Loaders;
using FrEee.Utility;
using Microsoft.Practices.Prism.Commands;
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
            var mods = new Collection<ModInfo>
            {
                stock.Info
            };

            // TODO abstract directories away
            if (Directory.Exists("Mods"))
            {
                foreach (var folder in Directory.GetDirectories("Mods"))
                {
                    loader.ModPath = Path.GetFileName(folder);
                    var mod = new Mod();
                    loader.Load(mod);
                    mods.Add(mod.Info);
                }
            }

            // Maintain referential integrity for the sake of figuring out which mod is active.
            LoadedModInfo = Mod.Current != null ? mods.Single(mod => mod.Folder == Mod.Current.Info.Folder) : null;
            ModInfos = new ObservableCollection<ModInfo>(mods.OrderBy(m => LoadedModInfo != null && m.Folder == LoadedModInfo.Folder).ThenBy(m => m.Name));
        }

        private ObservableCollection<ModInfo> _modInfos;
        public ObservableCollection<ModInfo> ModInfos
        {
            get { return _modInfos; }
            set
            {
                _modInfos = value;
                OnPropertyChanged();
            }
        }

        private ModInfo _loadedModInfo;
        public ModInfo LoadedModInfo
        {
            get { return _loadedModInfo; }
            set
            {
                _loadedModInfo = value;
                OnPropertyChanged();
                LoadModCommand.RaiseCanExecuteChanged();
            }
        }

        private DelegateCommand<ModInfo> _loadModCommand;
        public DelegateCommand<ModInfo> LoadModCommand
        {
            get
            {
                return _loadModCommand ?? (_loadModCommand = new DelegateCommand<ModInfo>(mod =>
                {
                    LoadedModInfo = mod;
                }, mod => mod != null));
            }
        }
    }
}
