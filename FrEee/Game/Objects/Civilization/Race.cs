using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A race of beings.
	/// </summary>
	public class Race : IAbilityObject
	{
		public Race()
		{
			Traits = new List<ITrait<Race>>();
		}

		/// <summary>
		/// The name of the insignia picture file, relative to Pictures/Insignia.
		/// </summary>
		public string InsigniaName { get; set; }

		/// <summary>
		/// The name of the shipset, relative to Pictures/Shipsets.
		/// </summary>
		public string ShipsetPath { get; set; }

		/// <summary>
		/// The name of the leader's image file, relative to Pictures/Leaders.
		/// </summary>
		public string LeaderPortraitName { get; set; }

		/// <summary>
		/// The name of the population image file, relative to Pictures/Population.
		/// </summary>
		public string PopulationIconName { get; set; }

		/// <summary>
		/// The AI which controls the behavior of empires of this race.
		/// </summary>
		public EmpireAI AI { get; set; }

		public IList<ITrait<Race>> Traits { get; private set; }

		public IEnumerable<Ability> Abilities
		{
			get { return Traits.SelectMany(t => t.Abilities); }
		}
	}
}
