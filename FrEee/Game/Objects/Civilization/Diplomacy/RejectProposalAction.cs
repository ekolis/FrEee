using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Collections.Generic;

#nullable enable

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// An action that rejects a proposal.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RejectProposalAction : Action
	{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		// initialized via property
		public RejectProposalAction(Proposal proposal)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
			: base(proposal.Executor)
		{
			Proposal = proposal;
		}

		public override string Description => $"Reject {Proposal}";

		/// <summary>
		/// The proposal in question.
		/// </summary>
		[DoNotSerialize]
		public Proposal Proposal { get => proposal; set => proposal = value; }

		private GalaxyReference<Proposal> proposal { get; set; }

		public override void Execute()
		{
			if (Proposal.IsResolved)
				Executor.Log.Add(Target.CreateLogMessage("The proposal \"" + Proposal + "\" has already been resolved and cannot be rejected now.", LogMessages.LogMessageType.Error));
			else
				Target.Log.Add(Executor.CreateLogMessage("The " + Executor + " has rejected our proposal (" + Proposal + ").", LogMessages.LogMessageType.Diplomacy));
		}

		public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable>? done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			base.ReplaceClientIDs(idmap, done);
			if (!done.Contains(this))
			{
				done.Add(this);
				proposal.ReplaceClientIDs(idmap, done);
			}
		}
	}
}
