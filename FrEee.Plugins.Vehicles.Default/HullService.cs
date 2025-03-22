using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Plugins.Vehicles.Default.Types;
using FrEee.Processes.Combat;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;

namespace FrEee.Plugins.Vehicles.Default;

[Export(typeof(IPlugin))]
public class HullService
	: Plugin<IHullService>, IHullService
{
	public override string Name { get; } = "HullService";

	public override IHullService Implementation => this;

	public IHull CreateHull(VehicleTypes vehicleType)
	{
		return vehicleType switch
		{
			VehicleTypes.Ship => new Hull<Ship>(),
			VehicleTypes.Base => new Hull<Base>(),
			VehicleTypes.Fighter => new Hull<Fighter>(),
			VehicleTypes.Satellite => new Hull<Satellite>(),
			VehicleTypes.Troop => new Hull<Troop>(),
			VehicleTypes.WeaponPlatform => new Hull<WeaponPlatform>(),
			VehicleTypes.Mine => new Hull<Mine>(),
			VehicleTypes.Drone => new Hull<Drone>(),
			var x => throw new NotSupportedException($"Can't build a hull of type {x}. Only single vehicle types can be built."),
		};
	}
}
