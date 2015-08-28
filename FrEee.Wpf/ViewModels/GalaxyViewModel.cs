using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicViewModel;
using FrEee.Game.Objects.Space;

namespace FrEee.Wpf.ViewModels
{
	public class GalaxyViewModel : DynamicViewModel<Galaxy>
	{
		public GalaxyViewModel()
			: base(null, Galaxy.Current)
		{
		}

		public IEnumerable<EmpireViewModel> Empires
		{
			get
			{
				foreach (var e in Model.Empires)
					yield return new EmpireViewModel(e);
			}
		}

		public EmpireViewModel CurrentEmpire
		{
			get
			{
				return new EmpireViewModel(Model.CurrentEmpire);
            }
		}
	}
}
