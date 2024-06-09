using FrEee.Objects.LogMessages;
using FrEee.Extensions;
using System.Linq;
using FrEee.Processes.Combat;

namespace FrEee.Objects.Civilization.Diplomacy.Clauses;

/// <summary>
/// A treaty clause which shares combat logs between empires.
/// </summary>
public class ShareCombatLogsClause : Clause
{
	public ShareCombatLogsClause(Empire giver, Empire receiver)
		: base(giver, receiver)
	{
	}

	public override string BriefDescription
	{
		get { return "Share Combat Logs"; }
	}

	public override string FullDescription
	{
		get
		{
			return Giver.WeOrName() + " will share all combat logs with " + Receiver.UsOrName();
		}
	}

	public override void PerformAction()
	{
		foreach (var msg in Giver.Log.OfType<PictorialLogMessage<IBattle>>())
			Receiver.Log.Add(msg.Copy());
	}
}