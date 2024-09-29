using FrEee.Objects.Civilization;

namespace FrEee.Gameplay.Commands.Orders;

public class ToggleOrdersOnHoldCommand
	: Command<IOrderable>, IToggleOrdersOnHoldCommand
{
	public ToggleOrdersOnHoldCommand(IOrderable target, bool areOrdersOnHold)
		: base(target)
	{
		IsToggleEnabled = areOrdersOnHold;
	}

	/// <summary>
	/// Are the orders on hold?
	/// </summary>
	public bool IsToggleEnabled { get; set; }

	public override void Execute()
	{
		Executor.AreOrdersOnHold = IsToggleEnabled;
	}
}
