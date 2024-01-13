using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;
using System.Drawing;
using System.Linq;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
	/// <summary>
	/// Displays min/org/rad income.
	/// </summary>
	public class ResourcesMode : ArgbMode
	{
		public override string Name
		{
			get { return "Resources"; }
		}

		protected override Color GetColor(StarSystem sys)
		{
			var max = Galaxy.Current.StarSystemLocations.MaxOfAllResources(l => GetResources(l.Item));
			foreach (var r in Resource.All)
				if (max[r] == 0)
					max[r] = int.MaxValue; // prevent divide by zero
			var amt = GetResources(sys);
			var blue = 255 * amt[Resource.Minerals] / max[Resource.Minerals];
			var green = 255 * amt[Resource.Organics] / max[Resource.Organics];
			var red = 255 * amt[Resource.Radioactives] / max[Resource.Radioactives];
			return Color.FromArgb(red, green, blue);
		}

		private ResourceQuantity GetResources(StarSystem sys)
		{
			return sys.SpaceObjects.OwnedBy(Empire.Current).OfType<IIncomeProducer>().Sum(sobj => sobj.GrossIncome());
		}
	}
}