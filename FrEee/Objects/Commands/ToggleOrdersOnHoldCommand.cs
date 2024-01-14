using FrEee.Interfaces;

namespace FrEee.Objects.Commands;

public class ToggleOrdersOnHoldCommand : Command<IOrderable>
{
	public ToggleOrdersOnHoldCommand(IOrderable target, bool areOrdersOnHold)
		: base(target)
	{
		AreOrdersOnHold = areOrdersOnHold;
	}

	public bool AreOrdersOnHold { get; set; }

	public override void Execute()
	{
		Executor.AreOrdersOnHold = AreOrdersOnHold;
	}
}
