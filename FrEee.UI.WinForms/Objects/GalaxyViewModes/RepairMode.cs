using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Extensions;
using System.Drawing;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Ecs;

namespace FrEee.UI.WinForms.Objects.GalaxyViewModes;

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