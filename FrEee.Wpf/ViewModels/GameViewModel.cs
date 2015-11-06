using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicViewModel;
using FrEee.Game.Objects.Space;

namespace FrEee.Wpf.ViewModels
{
	public class GameViewModel
	{
		/// <summary>
		/// For XAML design time binding.
		/// </summary>
		public GameViewModel()
			: this(null)
		{

		}

		public GameViewModel(Galaxy galaxy)
		{
			Galaxy = galaxy;
			Map = new GalaxyMapViewModel(Galaxy);
		}

		#region Model
		private Galaxy Galaxy { get; set; }
		#endregion

		#region Properties
		#endregion

		#region Sub View Models
		public GalaxyMapViewModel Map { get; private set; }

		public IEnumerable<EmpireViewModel> Empires
		{
			get
			{
				foreach (var e in Galaxy.Empires)
					yield return new EmpireViewModel(e);
			}
		}

		public EmpireViewModel CurrentEmpire
		{
			get
			{
				return new EmpireViewModel(Galaxy?.CurrentEmpire);
			}
		}
		#endregion
	}
}
