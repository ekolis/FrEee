using FrEee.Objects.Civilization;

namespace FrEee.Gameplay.Commands.Notes;

/// <summary>
/// Sets the name of an object.
/// </summary>
public class SetPublicNameCommand : Command<INameable>
{
	public SetPublicNameCommand(INameable target, string name)
		: base(target)
	{
		Name = name;
	}

	/// <summary>
	/// The name to set.
	/// </summary>
	public string Name { get; set; }

	public override void Execute()
	{
		Executor.Name = Name;
	}
}