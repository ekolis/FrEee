using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Orders;
using FrEee.Objects.Civilization;

namespace FrEee.Plugins.Default.Commands.Orders;

public class ToggleRepeatOrdersCommand
	: Command<IOrderable>, IToggleRepeatOrdersCommand
{
	public ToggleRepeatOrdersCommand(IOrderable target, bool areRepeatOrdersEnabled)
		: base(target)
	{
		IsToggleEnabled = areRepeatOrdersEnabled;
	}

	/// <summary>
	/// Are repeat orders enabled?
	/// </summary>
	public bool IsToggleEnabled { get; set; }

	public override void Execute()
	{
		Executor.IsOnRepeat = IsToggleEnabled;
	}
}
