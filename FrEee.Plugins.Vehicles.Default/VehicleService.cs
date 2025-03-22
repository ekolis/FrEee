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
public class VehicleService
	: Plugin<IVehicleService>, IVehicleService
{
	public override string Name { get; } = "VehicleService";

	public override IVehicleService Implementation => this;

	public IVehicle CreateVehicle(VehicleTypes vehicleType)
	{
		return vehicleType switch
		{
			VehicleTypes.Ship => new Ship(),
			VehicleTypes.Base => new Base(),
			VehicleTypes.Fighter => new Fighter(),
			VehicleTypes.Satellite => new Satellite(),
			VehicleTypes.Troop => new Troop(),
			VehicleTypes.WeaponPlatform => new WeaponPlatform(),
			VehicleTypes.Mine => new Mine(),
			VehicleTypes.Drone => new Drone(),
			var x => throw new NotSupportedException($"Can't build a vehicle of type {x}. Only single vehicle types can be built."),
		};
	}
}
