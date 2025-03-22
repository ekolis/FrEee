using System.Collections.Generic;
using FrEee.Extensions;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Objects.GameState;
using FrEee.Serialization;
using FrEee.Vehicles;

namespace FrEee.Plugins.Commands.Default.Commands.Designs;

/// <summary>
/// A command to enable or disable the obsolete flag on a design.
/// </summary>
public class SetObsoleteFlagCommand : Command<IDesign>, ISetObsoleteFlagCommand
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

	[DoNotSerialize]
	public IDesign Design { get { return design?.Value; } set { design = value.ReferViaGalaxy(); } }

	public bool IsObsolete { get; set; }

	public IDesign NewDesign { get; set; }

	public override IEnumerable<IReferrable> NewReferrables
	{
		get
		{
			if (NewDesign != null)
				yield return NewDesign;
		}
	}

	private GameReference<IDesign> design { get; set; }

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
}