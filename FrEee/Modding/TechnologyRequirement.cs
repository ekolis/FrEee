using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Technology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A requirement that an empire has researched some technology to some level.
	/// </summary>
	public class TechnologyRequirement : Requirement<Empire>
	{
		public TechnologyRequirement(Technology tech, int level)
			: base("Requires level " + level + " " + tech + ".")
		{
			Technology = tech;
			Level = level;
		}

		public Technology Technology { get; set; }

		public int Level { get; set; }

		public override bool IsMetBy(Empire obj)
		{
			return obj.ResearchedTechnologies[Technology] >= Level;
		}

		public override bool IsStrict
		{
			get { return true; }
		}
	}
}
