﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System.Drawing;
using System.Linq;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
	/// <summary>
	/// Displays repair rates in each resource.
	/// </summary>
	public class RepairMode : ArgbMode
	{
		public override string Name
		{
			get { return "Repair"; }
		}

		protected override Color GetColor(StarSystem sys)
		{
			var max = Galaxy.Current.StarSystemLocations.Max(l => GetRepair(l.Item));
			if (max == 0)
				return Color.Black;
			var sat = Weight(GetRepair(sys), max);
			return Color.FromArgb(sat, sat, sat);
		}

		private int GetRepair(StarSystem sys)
		{
			return sys.FindSpaceObjects<ISpaceObject>().OwnedBy(Empire.Current).Sum(x => x.GetAbilityValue("Component Repair").ToInt());
		}
	}
}