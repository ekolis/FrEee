using FrEee.Objects.Civilization;
using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.Objects.GameState;

namespace FrEee.Objects.Commands;

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
	public INameable Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

	private GalaxyReference<INameable> target { get; set; }

	public override void Execute()
	{
		Executor.PrivateNames.Remove(target);
	}
}