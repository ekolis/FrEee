﻿using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Extensions;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.LogMessages;
using FrEee.Vehicles;

namespace FrEee.Plugins.Default.Commands.Designs;

/// <summary>
/// A command to create a new vehicle design.
/// </summary>
[Serializable]
public class CreateDesignCommand<T> : Command<Empire>, ICreateDesignCommand where T : IVehicle
{
	public CreateDesignCommand(IDesign<T> design)
		: base(Empire.Current)
	{
		Design = design;
	}

	IDesign ICreateDesignCommand.Design { get { return Design; } }

	public IDesign<T> Design { get; set; }

	public override IEnumerable<IReferrable> NewReferrables
	{
		get
		{
			yield return Design;
		}
	}

	public override void Execute()
	{
		Design.VehiclesBuilt = 0; // in case it was tested in the simulator
		if (Design.Warnings.Any())
			Issuer.Log.Add(Design.CreateLogMessage("The " + Design.Name + " " + Design.VehicleTypeName + " design cannot be saved because it has warnings.", LogMessageType.Warning));
		Issuer.KnownDesigns.Add(Design);
	}
}