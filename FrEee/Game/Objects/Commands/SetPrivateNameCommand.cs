using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

#nullable enable

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// Sets the private name for an object.
	/// </summary>
	public class SetPrivateNameCommand : Command<Empire>
	{
		// TODO: why isn't "name" used here?
		public SetPrivateNameCommand(Empire empire, INameable target, string name)
			: base(empire)
		{
			Target = target;
		}

		/// <summary>
		/// The name to set.
		/// </summary>
		public string? Name { get; set; }

		/// <summary>
		/// What are we clearing the name on?
		/// </summary>
		[DoNotSerialize]
		public INameable? Target { get => target?.Value; set => target = value.ReferViaGalaxy(); }

		private GalaxyReference<INameable?>? target { get; set; }

		public override void Execute()
		{
			if (Executor is null || target is null)
				return;
			Executor.PrivateNames[target] = Name;
		}
	}
}
