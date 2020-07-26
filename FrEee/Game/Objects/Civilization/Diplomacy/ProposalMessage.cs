using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;

#nullable enable

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A message with a proposal that can be accepted or rejected.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ProposalMessage : Message
	{
		public ProposalMessage(Empire recipient)
			: base(recipient)
		{
			if (Recipient == Owner)
				throw new Exception("You can't send a diplomatic proposal to yourself!");
			Proposal = new Proposal(recipient);
		}

		public override IEnumerable<string> IconPaths => Proposal.Owner.IconPaths;

		public override IEnumerable<string> PortraitPaths => Proposal.Owner.PortraitPaths;

		/// <summary>
		/// The proposal in question.
		/// </summary>
		public Proposal Proposal { get; set; }

		public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable>? done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				Proposal.ReplaceClientIDs(idmap, done);
			}
		}
	}
}
