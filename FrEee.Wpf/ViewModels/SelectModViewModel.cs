using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FrEee.Modding;
using FrEee.Modding.Loaders;
using FrEee.Utility;
using FrEee.Wpf.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;

namespace FrEee.Wpf.ViewModels
{
    [Export]
    public class SelectModViewModel : FrEeeViewModelBase
    {
        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            ModInfos = null;
            ModInfos = new ObservableCollection<ModInfo>(await ServiceLocator.GetInstance<FilesystemService>().GetModInfosAsync());
            LoadedModInfo = Mod.Current != null ? ModInfos.FirstOrDefault(mod => mod.Folder == Mod.Current.Info.Folder) : null;
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
                return _loadModCommand ?? (_loadModCommand = new DelegateCommand<ModInfo>(async (mod) =>
                {
                    await Task.Run(() => Mod.Load(mod.Folder));
                    LoadedModInfo = mod;
                }, mod => mod != null));
            }
        }
    }
}
