using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Avalonia.Views;

namespace FrEee.Avalonia.ViewModels
{
	public class MainMenuViewModel
		: ViewModelBase
	{
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
