using FrEee.Game.Interfaces;
using System.Collections.Generic;

#nullable enable

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A general message with no parameters. Just text.
	/// </summary>
	public class GeneralMessage : Message
	{
		public GeneralMessage(Empire recipient)
			: base(recipient)
		{
		}

		public override IEnumerable<string> IconPaths => Owner.IconPaths;

		public override IEnumerable<string> PortraitPaths => Owner.PortraitPaths;

		public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done)
		{
			// nothing to do
		}
	}
}
