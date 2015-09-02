using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Wpf.Views.GalaxyMapViewRenderers
{
	public static class GalaxyMapViewRenderers
	{
		// TODO - implement the rest of the renderers
		private static IGalaxyMapViewRenderer[] all = new IGalaxyMapViewRenderer[]
		{
			new PresenceRenderer(),
			//new ForcesRenderer(),
			//new ColoniesRenderer(),
			//new ResourcesRenderer(),
			//new ResearchIntelRenderer(),
			//new UtilityRenderer(),
			//new ConstructionRenderer(),
			//new SpaceYardRenderer(),
			//new RepairRenderer(),
			//new WarpPointsRenderer(),
		};

		// TODO - moddable renderers?
		public static IEnumerable<IGalaxyMapViewRenderer> All
		{
			get
			{
				return all;
			}
		}
	}
}
