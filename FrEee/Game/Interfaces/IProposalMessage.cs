using FrEee.Game.Objects.Civilization.Diplomacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	public interface IProposalMessage : IMessage
	{
		Proposal Proposal { get; }
	}

	public interface IProposalMessage<T> : IProposalMessage, IMessage<AcceptRejectMessage<T>>
		where T : Proposal
	{
		T Proposal { get; set; }
	}
}
