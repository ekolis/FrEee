using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using FrEee.Game.Objects.Space;
using Microsoft.Win32;

namespace FrEee.Wpf.Views
{
	/// <summary>
	/// Interaction logic for MainMenuView.xaml
	/// </summary>
	public partial class MainMenuView
	{
		public MainMenuView()
		{
			InitializeComponent();
			CausesShutdown = true;
		}

		private void btnMods_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			new ModPickerView().ShowDialog();
		}

		private void btnScenarios_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// TODO - scenarios, however those will work...
			MessageBox.Show("Sorry, the scenario feature is not yet enabled.");
		}

		private void btnOptions_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// TODO - options
			MessageBox.Show("Sorry, the options feature is not yet enabled.");
		}

		private void btnCredits_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// TODO - proper credits view with scrolling and blackjack and hookers
			string credits;
			try
			{
				credits = File.ReadAllText("licenses/credits.txt");
			}
			catch
			{
				credits = "Could not open licenses/credits.txt.";
			}
			MessageBox.Show(credits, "FrEee v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3));
		}

		private void btnNew_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// TODO - start a new game with full fledged game setup and such
			MessageBox.Show("Sorry, the new game feature is not yet enabled.");
		}

		private void btnQuickstart_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// TODO - quickstart
			MessageBox.Show("Sorry, the quickstart feature is not yet enabled.");
		}

		private void btnResume_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// TODO - resume game
			MessageBox.Show("Sorry, the resume game feature is not yet enabled.");
		}

		private void btnLoad_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			var dlg = new OpenFileDialog();
			// TODO - let player resume turn from previously saved PLR
			dlg.Filter = "Savegames (*.gam)|*.gam";
			dlg.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Savegame");
			var result = dlg.ShowDialog();
			if (result ?? false)
				LoadGalaxyFromFile(dlg.FileName);
		}

		private void LoadGalaxyFromFile(string filename, bool? loadPlr = null)
		{
			Cursor = Cursors.Wait;
			var plrfile = Path.GetFileNameWithoutExtension(filename) + ".plr";
			Galaxy.Load(filename);
			if (Galaxy.Current.CurrentEmpire == null)
			{
				// host view, load host console
				Cursor = Cursors.Wait;			
				SwitchTo(new HostConsoleView());
			}
			else
			{
				// player view, load up the game
				if (File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Savegame", plrfile)))
				{
					if (loadPlr == null)
						loadPlr = MessageBox.Show("Player commands file exists for this turn. Resume turn from where you left off?", "Resume Turn", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
					if (loadPlr.Value)
						Galaxy.Current.LoadCommands();
				}
				Cursor = Cursors.Arrow;
				SwitchTo(new GameView());
			}
		}
	}
}
