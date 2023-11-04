using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Serialization; using FrEee.Serialization.Attributes;
using FrEee.Utility;
using System.Collections.Generic;

namespace FrEee.Objects.Commands
{
	/// <summary>
	/// A command to enable or disable the obsolete flag on a design.
	/// </summary>
	public class SetObsoleteFlagCommand : Command<IDesign>
	{
		public SetObsoleteFlagCommand(IDesign design, bool isObsolete)
			: base(design)
		{
			if (design.IsNew)
				NewDesign = design;
			else
				Design = design;
			IsObsolete = isObsolete;
		}

		/// <summary>
		/// The design to set the flag on if it's already knwon by the server.
		/// </summary>
		[GameReference]
		public IDesign Design { get; set; }

		/// <summary>
		/// The flag state to set.
		/// </summary>
		public bool IsObsolete { get; set; }

		/// <summary>
		/// The design to set the flag on if it's only in the library and not in the game or it's a brand new design.
		/// </summary>
		public IDesign NewDesign { get; set; }

		public override IEnumerable<IReferrable> NewReferrables
		{
			get
			{
				if (NewDesign != null)
					yield return NewDesign;
			}
		}

		public override void Execute()
		{
			if (NewDesign != null)
			{
				// allows obsoleting designs that are on the library or newly created (not on the server yet)
				Issuer.KnownDesigns.Add(NewDesign);
				Design = NewDesign;
			}
			Design.IsObsolete = IsObsolete;
		}

		public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				base.ReplaceClientIDs(idmap, done);
				if (Design != null)
					Design.ReplaceClientIDs(idmap, done);
				if (NewDesign != null)
					NewDesign.ReplaceClientIDs(idmap, done);
			}
		}
	}
}
