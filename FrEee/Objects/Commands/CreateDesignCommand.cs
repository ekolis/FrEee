using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Vehicles;
using FrEee.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Objects.Commands
{
	/// <summary>
	/// A command to create a new vehicle design.
	/// </summary>
	[Serializable]
	public class CreateDesignCommand<T> : Command<Empire>, ICreateDesignCommand where T : IVehicle
	{
		public CreateDesignCommand(IDesign design)
			: base(Empire.Current)
		{
			Design = design;
		}

		public IDesign Design { get; set; }

		public override IEnumerable<IReferrable> NewReferrables
		{
			get
			{
				yield return Design;
			}
		}

		public override void Execute()
		{
			// don't try to re-add a design that was already added (e.g. during deserialization)
			if (!Issuer.KnownDesigns.Contains(Design))
			{
				if (Design.Warnings.Any())
				{
					Issuer.Log.Add(Design.CreateLogMessage("The " + Design.Name + " " + Design.VehicleTypeName + " design cannot be saved because it has warnings.", LogMessages.LogMessageType.Warning));
				}
				else
				{
					Design.VehiclesBuilt = 0; // in case it was tested in the simulator
					Issuer.KnownDesigns.Add(Design);
				}
			}
		}

		/// <summary>
		/// This command is executed immediately after deserialization so that the design will be available for subsequent commands.
		/// </summary>
		public void AfterDeserialize(ObjectGraphContext context)
		{
			// XXX: this isn't going to work, we need an intermediate state for the game
			// where the game references' IDs are loaded but the actual objects aren't!
			// removing the references from the properties won't work unless we have an
			// intermediate state where deserialized data can just be data and not be immediately
			// dereferenced against data that might not even exist yet!
			// perhaps consider rewriting the serializer to be based on the SimpleData protocol
			// or some other sort of protocol that doesn't immediately instantiate live objects?
			// could this be the root cause of all those previous incompatibilities with various
			// serializers like the JSON serializer? that we need the intermediate state?
			// or can we time the loading of different things to make it work as is?
			context.AfterDeserializeActions.Add(Execute);
		}
	}
}
