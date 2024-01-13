using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Utility;
using FrEee.Extensions;
using System.Drawing;
using System.Linq;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
	/// <summary>
	/// Displays space yards by build rate in each resource.
	/// </summary>
	public class SpaceYardMode : ArgbMode
	{
		public override string Name
		{
			get { return "Space Yards"; }
		}

		protected override Color GetColor(StarSystem sys)
		{
			var max = Galaxy.Current.StarSystemLocations.MaxOfAllResources(l => GetSY(l.Item));
			foreach (var r in Resource.All)
				if (max[r] == 0)
					max[r] = int.MaxValue; // prevent divide by zero
			var amt = GetSY(sys);
			var blue = 255 * amt[Resource.Minerals] / max[Resource.Minerals];
			var green = 255 * amt[Resource.Organics] / max[Resource.Organics];
			var red = 255 * amt[Resource.Radioactives] / max[Resource.Radioactives];
			return Color.FromArgb(red, green, blue);
		}

		private ResourceQuantity GetSY(StarSystem sys)
		{
			var qs = sys.FindSpaceObjects<IConstructor>().OwnedBy(Empire.Current).Select(x => x.ConstructionQueue).Where(q => q != null && q.IsSpaceYardQueue);
			return qs.Sum(q => q.Rate);
		}
	}
}