using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.LogMessages;
using FrEee.Serialization;
using FrEee.Utility;
using System.Collections.Generic;

namespace FrEee.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// An action that rejects a proposal.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RejectProposalAction : Action
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
		[GameReference]
		public Proposal Proposal { get; set; }

		public override void Execute()
		{
			if (Proposal.IsResolved)
				Executor.Log.Add(Target.CreateLogMessage("The proposal \"" + Proposal + "\" has already been resolved and cannot be rejected now.", LogMessageType.Error));
			else
				Target.Log.Add(Executor.CreateLogMessage("The " + Executor + " has rejected our proposal (" + Proposal + ").", LogMessageType.Diplomacy));
		}

		public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			base.ReplaceClientIDs(idmap, done);
			if (!done.Contains(this))
			{
				done.Add(this);
				Proposal.ReplaceClientIDs(idmap, done);
			}
		}
	}
}
