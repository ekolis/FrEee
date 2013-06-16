using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.AI;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
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
			Color = Color.White;
		}

		/// <summary>
		/// The name of this race. Also used for picture names.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The default empire name for this race.
		/// </summary>
		public string EmpireName { get; set; }

		/// <summary>
		/// The default leader name for empires of this race.
		/// </summary>
		public string LeaderName { get; set; }

		/// <summary>
		/// The default leader portrait name for empires of this race.
		/// </summary>
		public string LeaderPortraitName { get; set; }

		/// <summary>
		/// The population icon name for this race.
		/// </summary>
		public string PopulationIconName { get; set; }

		/// <summary>
		/// The default insignia name for empires of this race.
		/// </summary>
		public string InsigniaName { get; set; }

		/// <summary>
		/// The default color used to represent empires of this race.
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
		/// The race's preferred shipset path.
		/// </summary>
		public string ShipsetPath { get; set; }

		/// <summary>
		/// The AI which controls the behavior of empires of this race.
		/// </summary>
		public string AIName { get; set; }

		public string HappinessModelName { get; set; }

		/// <summary>
		/// The race's cultural happiness model.
		/// </summary>
		[DoNotSerialize]
		public HappinessModel HappinessModel {
			get { return Mod.Current.HappinessModels.SingleOrDefault(h => h.Name == HappinessModelName); }
			set { HappinessModelName = value.Name; }
		}

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

		public IEnumerable<string> Warnings
		{
			get
			{
				if (string.IsNullOrWhiteSpace(Name))
					yield return "You must specify a name for your race.";
				if (string.IsNullOrWhiteSpace(PopulationIconName))
					yield return "You must specify a population icon for your race.";
				if (string.IsNullOrWhiteSpace(NativeAtmosphere))
					yield return "You must specify a native atmosphere for your race.";
				if (string.IsNullOrWhiteSpace(NativeSurface))
					yield return "You must specify a native planet surface for your race.";
				if (!Mod.Current.StellarObjectTemplates.OfType<Planet>().Any(p => p.Atmosphere == NativeAtmosphere && p.Surface == NativeSurface && !p.Size.IsConstructed))
					yield return NativeSurface + " / " + NativeAtmosphere + " is not a valid surface / atmosphere combination for the current mod.";
			}
		}
	}
}
