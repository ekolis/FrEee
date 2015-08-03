using System.IO;
using System.Reflection;
using System.Windows;

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
			// TODO - load game
			MessageBox.Show("Sorry, the load game feature is not yet enabled.");
		}
	}
}
