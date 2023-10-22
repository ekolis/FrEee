using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Serialization;
using FrEee.Utility;

namespace FrEee.Objects.Commands
{
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
		[GameReference]
		public INameable Target { get; set; }

		public override void Execute()
		{
			Executor.PrivateNames.Remove(Target);
		}
	}
}
