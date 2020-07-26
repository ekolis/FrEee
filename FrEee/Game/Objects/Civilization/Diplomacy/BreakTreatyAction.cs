using FrEee.Utility.Extensions;

#nullable enable

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// Breaks a treaty with the target empire.
	/// </summary>
	public class BreakTreatyAction : Action
	{
		public BreakTreatyAction(Empire target)
			: base(target)
		{
		}

		public override string Description => "Break Treaty";

		public override void Execute()
		{
			foreach (var clause in Executor.GetTreaty(Target))
				clause.Dispose();

			Executor.Log.Add(Target.CreateLogMessage("We have broken our treaty with the " + Target + ".", LogMessages.LogMessageType.Generic));
			Target.Log.Add(Executor.CreateLogMessage("The " + Target + " has broken its treaty with us.", LogMessages.LogMessageType.Generic));

			Executor.TriggerHappinessChange(hm => hm.TreatyNone);
			Target.TriggerHappinessChange(hm => hm.TreatyNone);
		}
	}
}
