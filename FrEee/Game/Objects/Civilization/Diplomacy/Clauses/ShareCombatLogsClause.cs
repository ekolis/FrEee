using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Combat;

namespace FrEee.Game.Objects.Civilization.Diplomacy.Clauses
{
	/// <summary>
	/// A treaty clause which shares combat logs between empires.
	/// </summary>
	public class ShareCombatLogsClause : Clause
	{
		public ShareCombatLogsClause(Empire giver, Empire receiver)
			: base(giver, receiver)
		{
		}

		public override void PerformAction()
		{
			foreach (var msg in Giver.Log.OfType<PictorialLogMessage<Battle>>())
				Receiver.Log.Add(msg.Copy());
		}

		public override string FullDescription
		{
			get
			{
				return Giver.WeOrName() + " will share all combat logs with " + Receiver.UsOrName();
			}
		}

		public override string BriefDescription
		{
			get { return "Share Combat Logs"; }
		}
	}
}
