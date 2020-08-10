using FrEee.Game.Interfaces;

#nullable enable

namespace FrEee.Game.Objects.Commands
{
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
			if (Executor is null)
				return;
			Executor.Name = Name;
		}
	}
}
