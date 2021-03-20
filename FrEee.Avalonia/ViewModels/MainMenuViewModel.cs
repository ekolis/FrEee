using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using FrEee.Avalonia.Views;

namespace FrEee.Avalonia.ViewModels
{
	public class MainMenuViewModel
		: ViewModelBase
	{
		/// <summary>
		/// Loads a game.
		/// </summary>
		public async Task LoadGameAsync()
		{
			var dlg = new OpenFileDialog
			{
				Filters = new List<FileDialogFilter>
				{
					new FileDialogFilter
					{
						Extensions = new List<string> { "gam" },
						Name = "FrEee Savegames"
					}
				},
				Directory = "Savegame",
				AllowMultiple = false,
			};
			var filenames = await dlg.ShowAsync(GameWindow.Instance);
			if (filenames.Any())
				GameWindow.Instance.Load(ViewLayer.Strategy); // TODO: actually load game
		}

		/// <summary>
		/// Quits the game.
		/// </summary>
		public void Quit()
		{
			while (ViewLayer.Any())
				ViewLayer.Pop();
		}
	}
}
