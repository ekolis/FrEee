using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		}

		/// <summary>
		/// The action in question.
		/// </summary>
		public Action Action { get; set; }

		public override void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			Action.ReplaceClientIDs(idmap);
		}
	}
}
