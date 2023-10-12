using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using System;
using System.Collections.Generic;

namespace FrEee.Objects.Civilization.Diplomacy
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

		public override IEnumerable<string> IconPaths
		{
			get
			{
				return Proposal.Owner.IconPaths;
			}
		}

		public override IEnumerable<string> PortraitPaths
		{
			get
			{
				return Proposal.Owner.PortraitPaths;
			}
		}

		/// <summary>
		/// The proposal in question.
		/// </summary>
		public Proposal Proposal { get; set; }

		public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
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