using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FrEee.Modding;
using FrEee.Modding.Loaders;
using FrEee.Utility.Extensions;

namespace FrEee.Wpf.Services
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FilesystemService
    {
        private const string ModsDirectory = "Mods/";

        public IEnumerable<ModInfo> GetModInfos()
        {
            var stock = new Mod();
            var loader = new ModInfoLoader(null);
            loader.Load(stock);
            var mods = new Collection<ModInfo>
            {
                stock.Info
            };

            if (Directory.Exists(ModsDirectory))
            {
                foreach (var folder in Directory.GetDirectories(ModsDirectory))
                {
                    loader.ModPath = Path.GetFileName(folder);
                    var mod = new Mod();
                    loader.Load(mod);
                    mods.Add(mod.Info);
                }
            }

            return mods.OrderBy(mod => mod.Folder == null).ThenBy(mod => mod.Name).ToList();
        }

        public async Task<IEnumerable<ModInfo>> GetModInfosAsync()
        {
            return await Task.Run(() => GetModInfos());
        }
    }
}
