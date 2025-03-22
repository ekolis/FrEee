using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Orders;
using FrEee.Objects.Civilization;

namespace FrEee.Plugins.Commands.Default.Commands.Orders;

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
		Executor.IsOnHold = IsToggleEnabled;
	}
}
