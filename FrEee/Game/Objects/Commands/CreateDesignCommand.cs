using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A command to create a new vehicle design.
	/// </summary>
	[Serializable]
	public class CreateDesignCommand<T> : Command<Empire> where T : Vehicle
	{
		public CreateDesignCommand(Design<T> design)
			: base(design.Owner, design.Owner)
		 {
			 Design = design;
		 }

		public IDesign Design { get; set; }

		public override void Execute()
		{
			if (Design.Warnings.Any())
				Issuer.Log.Add(new PictorialLogMessage<IDesign>("The " + Design.Name + " " + Design.VehicleTypeName + " design cannot be saved because it has warnings.", Galaxy.Current.TurnNumber, Design));
			Issuer.KnownDesigns.Add(Design);
			Galaxy.Current.Register(Design);
		}
	}
}
