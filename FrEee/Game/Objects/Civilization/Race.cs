﻿using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.AI;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A race of beings.
	/// </summary>
	[Serializable]
	public class Race : INamed, IAbilityObject, IPictorial, IReferrable
	{
		public Race()
		{
			TraitNames = new List<string>();
			Aptitudes = new SafeDictionary<string, int>();
		}

		/// <summary>
		/// The name of this race. Also used for picture names.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The population icon name for this race.
		/// </summary>
		public string PopulationIconName { get; set; }

		/// <summary>
		/// The atmosphere which this race breathes.
		/// </summary>
		public string NativeAtmosphere { get; set; }

		/// <summary>
		/// The native planet surface type of this race.
		/// </summary>
		public string NativeSurface { get; set; }

		/// <summary>
		/// The race's happiness model.
		/// </summary>
		[DoNotSerialize]
		public HappinessModel HappinessModel { get { return happinessModel; } set { happinessModel = value; } }

		private ModReference<HappinessModel> happinessModel { get; set; }

		/// <summary>
		/// The names of the race's traits.
		/// </summary>
		public IList<string> TraitNames { get; private set; }

		/// <summary>
		/// The traits of the race.
		/// </summary>
		public IEnumerable<Trait> Traits { get { return Mod.Current.Traits.Join(TraitNames, t => t.Name, n => n, (t, n) => t); } }

		public IEnumerable<Ability> Abilities
		{
			get { return Traits.SelectMany(t => t.Abilities); }
		}

		public IEnumerable<Ability> UnstackedAbilities
		{
			get { return Abilities; }
		}

		/// <summary>
		/// The population icon.
		/// </summary>
		public Image Icon
		{
			get { return Pictures.GetIcon(this); }
		}

		/// <summary>
		/// The population portrait.
		/// </summary>
		public Image Portrait
		{
			get { return Pictures.GetPortrait(this); }
		}

		/// <summary>
		/// Aptitudes of this race.
		/// </summary>
		// TODO - convert to NamedDictionary
		public IDictionary<string, int> Aptitudes { get; private set; }

		/// <summary>
		/// Races have no owner.
		/// </summary>
		public Empire Owner
		{
			get { return null; }
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			Galaxy.Current.UnassignID(this);
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
				if (HappinessModel == null)
					yield return "You must specify a happiness model for your race.";
				foreach (var kvp in Aptitudes)
				{
					var apt = Aptitude.All.FindByName(kvp.Key);
					if (apt == null)
						yield return "\"" + kvp.Key + "\" is not a valid racial aptitude.";
					else if (kvp.Value > apt.MaxPercent)
						yield return "Aptitude value for " + kvp.Key + " is too high.";
					else if (kvp.Value < apt.MinPercent)
						yield return "Aptitude value for " + kvp.Key + " is too low.";
				}
			}
		}

		public static Race Load(string filename)
		{
			var fs = new FileStream(filename, FileMode.Open);
			var race = Serializer.Deserialize<Race>(fs);
			fs.Close(); fs.Dispose();
			return race;
		}

		public void Save(string filename)
		{
			var fs = new FileStream(filename, FileMode.Create);
			Serializer.Serialize(this, fs);
			fs.Close(); fs.Dispose();
		}

		/// <summary>
		/// Races are known to everyone, though they really should be hidden until first contact...
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			// TODO - hide races until first contact
			return Visibility.Scanned;
		}

		public long ID { get; set; }

		public AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Race; }
		}

		public IEnumerable<Ability> IntrinsicAbilities
		{
			get { yield break; }
		}

		public IEnumerable<IAbilityObject> Children
		{
			get { return Traits; }
		}

		public IEnumerable<IAbilityObject> Parents
		{
			get
			{
				// TODO - return empire?
				yield break;
			}
		}

		public bool IsDisposed { get; set; }

		/// <summary>
		/// Resource income percentages based on racial aptitudes.
		/// </summary>
		public ResourceQuantity IncomePercentages
		{
			get
			{
				var result = new ResourceQuantity();
				foreach (var r in Resource.All)
				{
					var factor = 1d;
					if (r.Aptitude != null)
						factor *= Aptitudes[r.Aptitude.Name] / 100d;
					result += (int)(100 * factor) * r;
				}
				return result;
			}
		}
	}
}
