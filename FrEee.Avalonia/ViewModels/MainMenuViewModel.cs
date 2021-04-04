using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using FrEee.Avalonia.Views;
using FrEee.Game.Objects.Space;

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
			// TODO: warnings if there's more than one file selected
			if (filenames.Any())
			{
				Galaxy.Load(filenames.First());
				GameWindow.Instance.Load(ViewLayer.Strategy);
			}
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
