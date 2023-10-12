using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
	/// <summary>
	/// Displays the relative concentrations of friendly, allied, neutral, and enemy forces (by tonnage) using pie charts.
	/// </summary>
	public class ForcesMode : PieMode
	{
		public override string Name
		{
			get { return "Forces"; }
		}

		protected override int GetAlpha(StarSystem sys)
		{
			var forces = The.Galaxy.StarSystemLocations.Select(l => new { System = l.Item, Vehicles = l.Item.FindSpaceObjects<SpaceVehicle>() });
			var maxTonnage = forces.Max(f => f.Vehicles.Sum(v => v.Design.Hull.Size));
			var vehicles = sys.FindSpaceObjects<SpaceVehicle>().ToArray();
			var tonnageHere = vehicles.Sum(v => v.Design.Hull.Size);
			return 255 * tonnageHere / maxTonnage;
		}

		protected override IEnumerable<Tuple<Color, float>> GetAmounts(StarSystem sys)
		{
			// find relative tonnage of friendly, allied, neutral, and and enemy forces
			var vehicles = sys.FindSpaceObjects<SpaceVehicle>().ToArray();
			var byOwner = vehicles.GroupBy(v => v.Owner);
			var friendlyTonnage = byOwner.Where(f => f.Key == Empire.Current).SelectMany(f => f).Sum(v => v.Design.Hull.Size);
			var allyTonnage = byOwner.Where(f => f.Key.IsAllyOf(Empire.Current, sys)).SelectMany(f => f).Sum(v => v.Design.Hull.Size);
			var neutralTonnage = byOwner.Where(f => f.Key.IsNeutralTo(Empire.Current, sys)).SelectMany(f => f).Sum(v => v.Design.Hull.Size);
			var enemyTonnage = byOwner.Where(f => f.Key.IsEnemyOf(Empire.Current, sys)).SelectMany(f => f).Sum(v => v.Design.Hull.Size);

			yield return Tuple.Create(Color.Blue, (float)friendlyTonnage);
			yield return Tuple.Create(Color.Green, (float)allyTonnage);
			yield return Tuple.Create(Color.Yellow, (float)neutralTonnage);
			yield return Tuple.Create(Color.Red, (float)enemyTonnage);
		}
	}
}