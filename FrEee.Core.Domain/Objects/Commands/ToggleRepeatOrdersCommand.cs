using FrEee.Objects.Civilization;

namespace FrEee.Objects.Commands;

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
		Executor.AreRepeatOrdersEnabled = AreRepeatOrdersEnabled;
	}
}
