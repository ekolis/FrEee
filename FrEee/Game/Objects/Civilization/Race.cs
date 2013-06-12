using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.AI;
using FrEee.Game.Objects.Space;
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
	public class Race : INamed, IAbilityObject, IPictorial, IReferrable
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
		/// The atmosphere which this race breathes.
		/// </summary>
		public string NativeAtmosphere { get; set; }

		/// <summary>
		/// The native planet surface type of this race.
		/// </summary>
		public string NativeSurface { get; set; }

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

		public int ID
		{
			get;
			set;
		}

		/// <summary>
		/// Races have no owner.
		/// </summary>
		public Empire Owner
		{
			get { return null; }
		}

		public void Dispose()
		{
			Galaxy.Current.Unregister(this);
			foreach (var emp in Galaxy.Current.Empires)
				Galaxy.Current.Unregister(this, emp);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
