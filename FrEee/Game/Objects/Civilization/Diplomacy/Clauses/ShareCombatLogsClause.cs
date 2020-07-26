using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.LogMessages;
using FrEee.Utility.Extensions;
using System.Linq;

#nullable enable

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

		public override string BriefDescription => "Share Combat Logs";

		public override string FullDescription => $"{Giver.WeOrName()} will share all combat logs with {Receiver.UsOrName()}";

		public override void PerformAction()
		{
			foreach (var msg in Giver.Log.OfType<PictorialLogMessage<IBattle>>())
				Receiver.Log.Add(msg.Copy());
		}
	}
}
