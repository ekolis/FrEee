using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Objects.Civilization;
using FrEee.Objects.Technology;
using FrEee.Utility;
using FrEee.Vehicles.Types;

namespace FrEee.Vehicles;
public class DesignFactory
	: IDesignFactory
{
	public IDesign<IUnit> Militia { get; } = BuildMilitia();

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

	public IDesign Build(VehicleTypes vehicleType)
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

	public IDesign Build(IHull hull)
	{
		var design = Build(hull.VehicleType);
		design.Hull = hull;
		return design;
	}
}
