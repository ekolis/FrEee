using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility.Extensions;
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
	public class CreateDesignCommand<T> : Command<Empire>, ICreateDesignCommand where T : Vehicle
	{
		public CreateDesignCommand(IDesign<T> design)
			: base(design.Owner, design.Owner)
		 {
			 Design = design;
		 }

		IDesign ICreateDesignCommand.Design { get { return Design; } }

		public IDesign<T> Design { get; set; }

		public override void Execute()
		{
			if (Design.Warnings.Any())
				Issuer.Log.Add(Design.CreateLogMessage("The " + Design.Name + " " + Design.VehicleTypeName + " design cannot be saved because it has warnings."));
			Issuer.KnownDesigns.Add(Design);
		}

		public override void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			base.ReplaceClientIDs(idmap);
			Design.ReplaceClientIDs(idmap);
		}

		public override IEnumerable<IReferrable> NewReferrables
		{
			get
			{
				yield return Design;
			}
		}
	}
}
