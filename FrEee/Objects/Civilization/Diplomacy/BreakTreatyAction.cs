using FrEee.Extensions;
using FrEee.Objects.Civilization;
using FrEee.Objects.LogMessages;

namespace FrEee.Objects.Civilization.Diplomacy
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

		public override string Description
		{
			get { return "Break Treaty"; }
		}

		public override void Execute()
		{
			foreach (var clause in Executor.GetTreaty(Target))
				clause.Dispose();

			Executor.Log.Add(Target.CreateLogMessage("We have broken our treaty with the " + Target + ".", LogMessageType.Generic));
			Target.Log.Add(Executor.CreateLogMessage("The " + Target + " has broken its treaty with us.", LogMessageType.Generic));

			Executor.TriggerHappinessChange(hm => hm.TreatyNone);
			Target.TriggerHappinessChange(hm => hm.TreatyNone);
		}
	}
}