using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Wpf.Utility;

namespace FrEee.Wpf.Views.GalaxyMapViewRenderers
{
	/// <summary>
	/// Displays the presence of empires in star systems using pies with equal slices of each present empire's color, similar to SE3.
	/// </summary>
	public class PresenceRenderer : PieRenderer
	{
		public override string Name
		{
			get
			{
				return "Presence";
			}
		}

		protected override byte GetAlpha(StarSystem sys)
		{
			// draw all systems at full opacity
			return 255;
		}

		protected override IEnumerable<Tuple<Color, double>> GetAmounts(StarSystem sys)
		{
			// count each empire equally
			var owners = FindOwners(sys);
			foreach (var owner in owners)
				yield return Tuple.Create(owner.Color.ToWpfColor(), 1d);
		}

		private IEnumerable<Empire> FindOwners(StarSystem sys)
		{
			return sys.FindSpaceObjects<ISpaceObject>().Select(x => x.Owner).Distinct().Where(o => o != null);
		}
	}
}
