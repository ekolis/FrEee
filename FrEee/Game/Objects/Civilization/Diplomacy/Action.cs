using FrEee.Game.Objects.Commands;
using FrEee.Utility;

#nullable enable

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A unilateral diplomatic action.
	/// </summary>
	public abstract class Action : Command<Empire>
	{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		// initialized via property
		protected Action(Empire target)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
			: base(Empire.Current)
		{
			Target = target;
		}

		public abstract string Description { get; }

		/// <summary>
		/// The empire that is the target of this action.
		/// </summary>
		[DoNotSerialize]
		public Empire Target { get => target; set => target = value; }

		private GalaxyReference<Empire> target { get; set; }
	}
}
