using FrEee.Game.Interfaces;

#nullable enable

namespace FrEee.Game.Objects.Commands
{
	public class ToggleRepeatOrdersCommand : Command<IOrderable>
	{
		public ToggleRepeatOrdersCommand(IOrderable target, bool areRepeatOrdersEnabled)
			: base(target)
		{
			AreRepeatOrdersEnabled = areRepeatOrdersEnabled;
		}

		public bool AreRepeatOrdersEnabled { get; set; }

		public override void Execute()
		{
			if (Executor is null)
				return;
			Executor.AreRepeatOrdersEnabled = AreRepeatOrdersEnabled;
		}
	}
}
