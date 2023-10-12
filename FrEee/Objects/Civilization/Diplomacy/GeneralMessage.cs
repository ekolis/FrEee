using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using System.Collections.Generic;

namespace FrEee.Objects.Civilization.Diplomacy
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

		public override IEnumerable<string> IconPaths
		{
			get
			{
				return Owner.IconPaths;
			}
		}

		public override IEnumerable<string> PortraitPaths
		{
			get
			{
				return Owner.PortraitPaths;
			}
		}

		public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done)
		{
			// nothing to do
		}
	}
}