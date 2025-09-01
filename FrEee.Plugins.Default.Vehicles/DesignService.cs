using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.Technology;
using FrEee.Plugins.Default.Vehicles.Types;
using FrEee.Processes.Combat;
using FrEee.Utility;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;

namespace FrEee.Plugins.Default.Vehicles;

[Export(typeof(IPlugin))]
public class DesignService
	: Plugin<IDesignService>, IDesignService
{
	public override string Name { get; } = "DesignService";

	public override IDesignService Implementation => this;

	private IDesign<IUnit>? militiaDesign = null;

	public IDesign<IUnit> MilitiaDesign
	{
		get
		{
			if (militiaDesign is null)
			{
				militiaDesign = BuildMilitia();
			}
			return militiaDesign;
		}
	}

	private static IDesign<IUnit> BuildMilitia()
	{
		var militiaDesign = new Design<Troop>();
		militiaDesign.BaseName = "Militia";
		var militiaWeapon = new ComponentTemplate();
		militiaWeapon.Durability = Mod.Current.Settings.MilitiaHitpoints;
		militiaWeapon.Name = "Small Arms";
		militiaWeapon.WeaponInfo = new DirectFireWeaponInfo
		{
			// TODO: rebuild militia design when mod changes
			Damage = Mod.Current.Settings.MilitiaFirepower,
			MinRange = 0,
			MaxRange = 1,
		};
		militiaDesign.Components.Add(new MountedComponentTemplate(militiaDesign, militiaWeapon));
		return militiaDesign;
	}

	public IDesign CreateDesign(VehicleTypes vehicleType)
	{
		return vehicleType switch
		{
			VehicleTypes.Ship => new Design<Ship>(),
			VehicleTypes.Base => new Design<Base>(),
			VehicleTypes.Fighter => new Design<Fighter>(),
			VehicleTypes.Satellite => new Design<Satellite>(),
			VehicleTypes.Troop => new Design<Troop>(),
			VehicleTypes.WeaponPlatform => new Design<WeaponPlatform>(),
			VehicleTypes.Mine => new Design<Mine>(),
			VehicleTypes.Drone => new Design<Drone>(),
			var x => throw new NotSupportedException($"Can't build a design of type {x}. Only single vehicle types can be built."),
		};
	}

	public IDesign CreateDesign(IHull hull)
	{
		var design = CreateDesign(hull.VehicleType);
		design.Hull = hull;
		return design;
	}

	/// <summary>
	/// Imports designs from the library into the game that aren't already in the game.
	/// Requires a current empire. Should only be called client side.
	/// </summary>
	/// <returns>Copied designs imported.</returns>
	public IEnumerable<IDesign> ImportDesignsFromLibrary()
	{
		if (Empire.Current == null)
			throw new InvalidOperationException("Can't import designs without a current empire.");

		var designs = Services.DesignLibrary.Get(d =>
			d.IsValidInMod && !Empire.Current.KnownDesigns.Any(d2 => d2.Equals(d))
			).ToArray();

		designs.SafeForeach(d =>
		{
			d.IsNew = true;
			d.Owner = Empire.Current;
			d.TurnNumber = Game.Current.TurnNumber;
			d.Iteration = Empire.Current.KnownDesigns.OwnedBy(Empire.Current).Where(x => x.BaseName == d.BaseName && x.IsUnlocked()).MaxOrDefault(x => x.Iteration) + 1; // auto assign nex available iteration
			d.IsObsolete = d.IsObsolescent;
			Game.Current.Designs.Add(d);
			Empire.Current.KnownDesigns.Add(d); // only client side, don't need to worry about other players spying :)
		});

		return designs;
	}
}
