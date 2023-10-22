using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Serialization;
using FrEee.Utility;

namespace FrEee.Objects.Commands
{
	/// <summary>
	/// Sets the private name for an object.
	/// </summary>
	public class SetPrivateNameCommand : Command<Empire>
	{
		public SetPrivateNameCommand(Empire empire, INameable target, string name)
			: base(empire)
		{
			Target = target;
		}

		/// <summary>
		/// The name to set.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// What are we clearing the name on?
		/// </summary>
		[GameReference]
		public INameable Target { get; set; }

		public override void Execute()
		{
			Executor.PrivateNames[Target] = Name;
		}
	}
}
