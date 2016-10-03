using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Space;

namespace FrEee.Game.Objects.Commands
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

		private GalaxyReference<IDesign> design { get; set; }

		/// <summary>
		/// The design to set the flag on if it's already knwon by the server.
		/// </summary>
		[DoNotSerialize]
		public IDesign Design { get { return design.Value; } set { design = value.ReferViaGalaxy(); } }

		/// <summary>
		/// The design to set the flag on if it's only in the library and not in the game or it's a brand new design.
		/// </summary>
		public IDesign NewDesign { get; set; }

		/// <summary>
		/// The flag state to set.
		/// </summary>
		public bool IsObsolete { get; set; }

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
				if (design != null)
					design.ReplaceClientIDs(idmap, done);
				if (NewDesign != null)
					NewDesign.ReplaceClientIDs(idmap, done);
			}
		}

		public override IEnumerable<IReferrable> NewReferrables
		{
			get
			{
				if (NewDesign != null)
					yield return NewDesign;
			}
		}
	}
}
