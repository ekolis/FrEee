namespace FrEee.Game.Objects.Research
{
	/// <summary>
	/// Requirement for a technology to be researched to a particular level.
	/// </summary>
	public class TechnologyRequirement
	{
		public TechnologyRequirement(Technology tech, int level)
		{
			Technology = tech;
			Level = level;
		}

		/// <summary>
		/// The technology to be researched.
		/// </summary>
		public Technology Technology { get; set; }

		/// <summary>
		/// The level required.
		/// </summary>
		public int Level { get; set; }
	}
}
