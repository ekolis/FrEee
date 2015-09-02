using System.IO;
using System.Threading;
using System.Windows.Data;
using FrEee.Modding;
using FrEee.Modding.Loaders;
using FrEee.Utility;
using FrEee.Wpf.ViewModels;

namespace FrEee.Wpf.Views
{
	/// <summary>
	/// Interaction logic for ModPickerView.xaml
	/// </summary>
	public partial class ModPickerView
	{
		public ModPickerView()
		{
			InitializeComponent();

			// load modinfos
			ModInfos = new ModPickerViewModel();
			var stock = new Mod();
			var loader = new ModInfoLoader(null);
			loader.Load(stock);
			ModInfos.Add(stock.Info);
			ModInfos.SelectedItem = stock.Info;
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

		public ModPickerViewModel ModInfos
		{
			get
			{
				return (ModPickerViewModel)DataContext;
			}
			set
			{
				DataContext = value;
			}
		}

		private void btnLoad_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			var status = new Status();
			ModInfo mi = ModInfos.SelectedItem;
			var thread = new Thread(new ThreadStart(() =>
			{
				Mod.Load(mi.Folder, true, status);
				Dispatcher.Invoke(() =>
				{
					Close();
				});
			}));
			new StatusView(thread, status).ShowDialog();
		}
	}
}
