using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A proposal to form or modify a treaty.
	/// </summary>
	public class TreatyProposal : Proposal
	{
		public TreatyProposal(Empire target)
			: base(target)
		{ }


		// TODO - implement TreatyProposal once we settle on treaty mechanics

		public override string Description
		{
			get { return "Treaty Proposal"; }
		}

		public override void Execute()
		{
			
		}
	}
}
