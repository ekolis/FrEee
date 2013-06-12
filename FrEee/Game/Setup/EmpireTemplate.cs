using FrEee.Game.Interfaces;
using FrEee.Game.Objects.AI;
using FrEee.Game.Objects.Civilization;
using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Setup
{
	/// <summary>
	/// A template for configuring an empire.
	/// </summary>
	public class EmpireTemplate : ITemplate<Empire>
	{
		public EmpireTemplate()
		{
			Traits = new List<ITrait<Empire>>();
		}

		/// <summary>
		/// The name of the empire.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The name of the leader of this empire.
		/// </summary>
		public string LeaderName { get; set; }

		/// <summary>
		/// Traits of this empire.
		/// </summary>
		public IList<ITrait<Empire>> Traits { get; set; }

		/// <summary>
		/// The native race of this empire.
		/// </summary>
		public Race PrimaryRace { get; set; }

		/// <summary>
		/// The homeworld surface type of this empire.
		/// </summary>
		public string HomeworldSurfaceOverride { get; set; }

		/// <summary>
		/// Set this to override the color specified by the race.
		/// </summary>
		public Color? ColorOverride { get; set; }

		/// <summary>
		/// Set this to override the insignia name specified by the race.
		/// </summary>
		public string InsigniaNameOverride { get; set; }

		/// <summary>
		/// Set this to override the shipset path specified by the race.
		/// </summary>
		public string ShipsetPathOverride { get; set; }

		/// <summary>
		/// Set this to override the leader portrait specified by the race.
		/// </summary>
		public string LeaderPortraitNameOverride {get; set;}

		/// <summary>
		/// Set this to override the AI specified by the race.
		/// </summary>
		public EmpireAI AIOverride { get; set; }

		public Empire Instantiate()
		{
			var emp = new Empire();
			emp.Name = Name;
			emp.LeaderName = LeaderName;
			emp.Color = ColorOverride ?? PrimaryRace.Color;
			emp.PrimaryRace = PrimaryRace;
			emp.HomeworldSurface = HomeworldSurfaceOverride ?? PrimaryRace.NativeSurface;
			foreach (var t in Traits)
				emp.Traits.Add(t);
			emp.InsigniaName = InsigniaNameOverride ?? PrimaryRace.Name;
			emp.ShipsetPath = ShipsetPathOverride ?? PrimaryRace.Name;
			emp.LeaderPortraitName = LeaderPortraitNameOverride ?? PrimaryRace.Name;
			emp.AI = AIOverride ?? PrimaryRace.AI;

			return emp;
		}
	}
}
