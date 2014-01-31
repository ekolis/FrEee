using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A race with a quantity of population.
	/// </summary>
	public class RacePopulation
	{
		/// <summary>
		/// For serialization.
		/// </summary>
		public RacePopulation()
		{

		}

		public RacePopulation(Race race, long pop)
		{
			Race = race;
			Population = pop;
		}

		public Race Race { get; set; }

		public long Population { get; set; }
	}
}
