using FrEee.Game.Objects.Space;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A command to disband a fleet.
	/// </summary>
	public class DisbandFleetCommand : Command<Fleet>
	{
		public DisbandFleetCommand(Fleet fleet)
			: base(fleet)
		{
		}

		public override void Execute()
		{
			Executor.Dispose();
		}
	}
}