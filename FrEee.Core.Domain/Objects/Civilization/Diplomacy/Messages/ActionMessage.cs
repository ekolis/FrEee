using FrEee.Objects.Civilization.Diplomacy.Actions;
using FrEee.Objects.GameState;
using System;
using System.Collections.Generic;

namespace FrEee.Objects.Civilization.Diplomacy.Messages;

/// <summary>
/// A message specifying some unilateral action.
/// </summary>
public class ActionMessage : Message
{
    public ActionMessage(Empire recipient)
        : base(recipient)
    {
        if (Recipient == Owner)
            throw new Exception("You can't perform a diplomatic action on yourself!");
    }

    /// <summary>
    /// The action in question.
    /// </summary>
    public DiplomaticAction Action { get; set; }

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

    public override IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
    {
        Action.ReplaceClientIDs(idmap);
		return this;
	}
}