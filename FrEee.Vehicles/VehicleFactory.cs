﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Vehicles.Types;

namespace FrEee.Vehicles;
public class VehicleFactory
	: IVehicleFactory
{
	public IVehicle Build(VehicleTypes vehicleType)
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