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
	public class ProposalMessage<T> : Message<AcceptRejectMessage<T>>, IProposalMessage
		where T : Proposal
	{
		public ProposalMessage(Empire recipient)
			: base(recipient)
		{
		}

		/// <summary>
		/// The proposal in question.
		/// </summary>
		public T Proposal { get; set; }

		public override AcceptRejectMessage<T> CreateReply()
		{
			return new AcceptRejectMessage<T>(this);
		}

		public override void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			Proposal.ReplaceClientIDs(idmap);
		}

		Proposal IProposalMessage.Proposal
		{
			get { return Proposal; }
		}
	}
}
