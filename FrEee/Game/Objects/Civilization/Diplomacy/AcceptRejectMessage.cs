using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A message to accept or reject a proposal.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class AcceptRejectMessage<T> : Message<GeneralMessage>
		where T : Proposal
	{
		public AcceptRejectMessage(ProposalMessage<T> proposalMessage)
			: base(proposalMessage.Owner)
		{
			ProposalMessage = proposalMessage;
		}

		/// <summary>
		/// Was the proposal accepted?
		/// </summary>
		public bool IsAccepted { get; set; }

		private Reference<ProposalMessage<T>> proposalMessage { get; set; }

		/// <summary>
		/// The proposal message in question.
		/// </summary>
		[DoNotSerialize]
		public ProposalMessage<T> ProposalMessage { get { return proposalMessage; } set { proposalMessage = value; } }

		public override GeneralMessage CreateReply()
		{
			return new GeneralMessage(Owner);
		}

		public override void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			proposalMessage.ReplaceClientIDs(idmap);
		}
	}
}
