using FrEee.Objects.GameState;
using System.Collections.Generic;

namespace FrEee.Objects.Civilization.Diplomacy.Messages;

/// <summary>
/// A general message with no parameters. Just text.
/// </summary>
public class GeneralMessage : Message
{
    public GeneralMessage(Empire recipient)
        : base(recipient)
    {
    }

    public override IEnumerable<string> IconPaths
    {
        get
        {
            return Owner.IconPaths;
        }
    }

    public override IEnumerable<string> PortraitPaths
    {
        get
        {
            return Owner.PortraitPaths;
        }
    }

    public override IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done)
    {
		// nothing to do
		return this;
	}
}