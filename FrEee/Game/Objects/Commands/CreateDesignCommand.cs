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
			: base(Empire.Current)
		 {
			 Design = design;
		 }

		IDesign ICreateDesignCommand.Design { get { return Design; } }

		public IDesign<T> Design { get; set; }

		public override void Execute()
		{
			Design.VehiclesBuilt = 0; // in case it was tested in the simulator
			if (Design.Warnings.Any())
				Issuer.Log.Add(Design.CreateLogMessage("The " + Design.Name + " " + Design.VehicleTypeName + " design cannot be saved because it has warnings."));
			Issuer.KnownDesigns.Add(Design);
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				base.ReplaceClientIDs(idmap, done);
				Design.ReplaceClientIDs(idmap, done);
			}
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
