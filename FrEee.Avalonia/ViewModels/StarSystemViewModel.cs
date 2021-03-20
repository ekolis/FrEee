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
	public class StarSystemViewModel
		: ViewModelBase
	{
		public StarSystemViewModel(StarSystem starSystem)
			=> StarSystem = starSystem;

		public StarSystem StarSystem { get; set; }
	}
}
