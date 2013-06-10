using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.AI;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A race of beings.
	/// </summary>
	public class Race : INamed, IAbilityObject, IPictorial
	{
		public Race()
		{
			Traits = new List<ITrait<Race>>();
		}

		/// <summary>
		/// The name of this race. Also used for picture names.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The color used to represent empires of this race.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// The name of the shipset, relative to Pictures/Shipsets.
		/// </summary>
		public string ShipsetPath { get; set; }

		/// <summary>
		/// The AI which controls the behavior of empires of this race.
		/// </summary>
		public EmpireAI AI { get; set; }

		public IList<ITrait<Race>> Traits { get; private set; }

		public IEnumerable<Ability> Abilities
		{
			get { return Traits.SelectMany(t => t.Abilities); }
		}

		/// <summary>
		/// The population icon.
		/// </summary>
		public Image Icon
		{
			get { return Pictures.GetIcon(this); }
		}

		/// <summary>
		/// The leader portrait.
		/// </summary>
		public Image Portrait
		{
			get { return Pictures.GetPortrait(this); }
		}
	}
}
