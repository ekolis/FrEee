using FrEee.Extensions;
using FrEee.Gameplay.Commands;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Serialization;

namespace FrEee.Plugins.Default.Commands.Notes;

/// <summary>
/// Clears the private name for an object.
/// </summary>
public class ClearPrivateNameCommand : Command<Empire>
{
	public ClearPrivateNameCommand(Empire empire, INameable target)
		: base(empire)
	{
		Target = target;
	}

	/// <summary>
	/// What are we clearing the name on?
	/// </summary>
	[DoNotSerialize]
	public INameable Target { get; set; }

	private GameReference<INameable> target
	{
		get => Target?.ReferViaGalaxy();
		set => Target = value?.Value;
	}

	public override void Execute()
	{
		Executor.PrivateNames.Remove(target);
	}
}