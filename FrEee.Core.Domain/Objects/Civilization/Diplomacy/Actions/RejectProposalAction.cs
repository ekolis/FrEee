﻿using FrEee.Serialization;
using FrEee.Extensions;
using System.Collections.Generic;
using FrEee.Objects.GameState;

namespace FrEee.Objects.Civilization.Diplomacy.Actions;

/// <summary>
/// An action that rejects a proposal.
/// </summary>
/// <typeparam name="T"></typeparam>
public class RejectProposalAction : DiplomaticAction
{
    public RejectProposalAction(Proposal proposal)
        : base(proposal.Executor)
    {
        Proposal = proposal;
    }

    public override string Description
    {
        get { return "Reject " + Proposal; }
    }

    /// <summary>
    /// The proposal in question.
    /// </summary>
    [DoNotSerialize]
    public Proposal Proposal { get { return proposal; } set { proposal = value; } }

    private GameReference<Proposal> proposal { get; set; }

    public override void Execute()
    {
        if (Proposal.IsResolved)
            Executor.Log.Add(Target.CreateLogMessage("The proposal \"" + Proposal + "\" has already been resolved and cannot be rejected now.", LogMessages.LogMessageType.Error));
        else
            Target.Log.Add(Executor.CreateLogMessage("The " + Executor + " has rejected our proposal (" + Proposal + ").", LogMessages.LogMessageType.Diplomacy));
    }

    public override IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
    {
        if (done == null)
            done = new HashSet<IPromotable>();
        base.ReplaceClientIDs(idmap, done);
        if (!done.Contains(this))
        {
            done.Add(this);
            proposal = proposal.ReplaceClientIDs(idmap, done);
        }
		return this;
	}
}