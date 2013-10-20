using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A general message with no parameters. Just text.
	/// </summary>
	public class GeneralMessage : Message<GeneralMessage>
	{
		public GeneralMessage(Empire recipient)
			: base(recipient)
		{
		}

		public override GeneralMessage CreateReply()
		{
			return new GeneralMessage(Owner);
		}

		public override void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			// nothing to do
		}
	}
}
