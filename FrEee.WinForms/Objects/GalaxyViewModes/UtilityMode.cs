using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Extensions;
using System.Drawing;
using System.Linq;

namespace FrEee.WinForms.Objects.GalaxyViewModes;

/// <summary>
/// Displays resupply depots in blue and systems lacking space ports (but also containing non-merchants population) in red.
/// Purple means the system has a resupply depot but lacks a space port, naturally.
/// Gray is for colonized systems that have (or don't need) a spaceport but are colonized.
/// Systems that are not colonized are displayed in black.
/// Blue is lighter for systems with system supply.
/// </summary>
public class UtilityMode : ArgbMode
{
	public override string Name
	{
		get { return "Utilities"; }
	}

	protected override Color GetColor(StarSystem sys)
	{
		var colonized = sys.FindSpaceObjects<Planet>().OwnedBy(Empire.Current).Any();
		var hasSupply = sys.SpaceObjects.OwnedBy(Empire.Current).Any(sobj => sobj.HasAbility("Supply Generation"));
		var hasSystemSupply = sys.HasAbility("Supply Generation - System", Empire.Current) || sys.HasAbility("Supply Generation - System");
		// TODO - only count systems that would have income that needs a spaceport?
		var lacksSpaceport = !sys.HasAbility("Spaceport") && sys.SpaceObjects.OfType<Planet>().OwnedBy(Empire.Current).Where(p => p.Colony.Population.Any(kvp => !kvp.Key.HasAbility("No Spaceports") && kvp.Value > 0)).Any();

		if (hasSystemSupply && lacksSpaceport)
			return Color.Pink;
		else if (hasSupply && lacksSpaceport)
			return Color.Magenta;
		else if (hasSystemSupply)
			return Color.Cyan;
		else if (hasSupply)
			return Color.Blue;
		else if (lacksSpaceport)
			return Color.Red;
		else if (colonized)
			return Color.Gray;
		return Color.Black;
	}
}