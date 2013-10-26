using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// An action that accepts a proposal.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class AcceptProposalAction : Action
	{
		public AcceptProposalAction(Proposal proposal)
			: base(proposal.Executor)
		{
			Proposal = proposal;
		}

		private Reference<Proposal> proposal { get; set; }

		/// <summary>
		/// The proposal in question.
		/// </summary>
		[DoNotSerialize]
		public Proposal Proposal { get { return proposal; } set { proposal = value; } }

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				proposal.ReplaceClientIDs(idmap, done);
			}
		}

		public override string Description
		{
			get { return "Reject " + Proposal; }
		}

		public override void Execute()
		{
			if (Proposal.IsResolved)
				Executor.Log.Add(Target.CreateLogMessage("The proposal \"" + Proposal + "\" has already been resolved and cannot be accepted now."));
			else
			{
				Target.Log.Add(Executor.CreateLogMessage("The " + Executor + " has accepted our proposal (" + Proposal + ")."));
				Proposal.Execute();
			}
		}
	}
}
