using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
			Proposal = new Proposal(recipient);
		}

		/// <summary>
		/// The proposal in question.
		/// </summary>
		public Proposal Proposal { get; set; }

		public override void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			Proposal.ReplaceClientIDs(idmap);
		}
	}
}
