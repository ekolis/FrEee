using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;

#nullable enable

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A message specifying some unilateral action.
	/// </summary>
	public class ActionMessage : Message
	{
		public ActionMessage(Empire recipient)
			: base(recipient)
		{
			if (Recipient == Owner)
				throw new Exception("You can't perform a diplomatic action on yourself!");
		}

		/// <summary>
		/// The action in question.
		/// </summary>
		public Action? Action { get; set; }

		public override IEnumerable<string> IconPaths => Owner.IconPaths;

		public override IEnumerable<string> PortraitPaths => Owner.PortraitPaths;

		public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			Action?.ReplaceClientIDs(idmap);
		}
	}
}
