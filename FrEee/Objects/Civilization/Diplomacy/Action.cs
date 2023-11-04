using FrEee.Objects.Commands;
using FrEee.Serialization; using FrEee.Serialization.Attributes;

namespace FrEee.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A unilateral diplomatic action.
	/// </summary>
	public abstract class Action : Command<Empire>
	{
		protected Action(Empire target)
			: base(Empire.Current)
		{
			Target = target;
		}

		public abstract string Description { get; }

		/// <summary>
		/// The empire that is the target of this action.
		/// </summary>
		[GameReference]
		public Empire Target { get; set; }
	}
}
