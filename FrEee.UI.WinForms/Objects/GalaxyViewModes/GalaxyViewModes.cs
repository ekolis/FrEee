using System.Collections.Generic;

namespace FrEee.UI.WinForms.Objects.GalaxyViewModes;

public static class GalaxyViewModes
{
	public static IEnumerable<IGalaxyViewMode> All
	{
		get
		{
			return all;
		}
	}

	private static IGalaxyViewMode[] all = new IGalaxyViewMode[]
			{
		new PresenceMode(),
		new ForcesMode(),
		new ColoniesMode(),
		new ResourcesMode(),
		new ResearchIntelMode(),
		new UtilityMode(),
		new ConstructionMode(),
		new SpaceYardMode(),
		new RepairMode(),
		new WarpPointsMode(),
	};
}