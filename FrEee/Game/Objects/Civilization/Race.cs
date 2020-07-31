using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

#nullable enable

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

		public IEnumerable<Ability> Abilities => Traits.SelectMany(t => t.Abilities);

		public AbilityTargets AbilityTarget => AbilityTargets.Race;

		/// <summary>
		/// Aptitudes of this race.
		/// </summary>
		// TODO - convert to NamedDictionary
		public IDictionary<string, int> Aptitudes { get; private set; }

		public IEnumerable<IAbilityObject> Children => Traits;

		/// <summary>
		/// The race's happiness model.
		/// </summary>
		[DoNotSerialize]
		public HappinessModel HappinessModel { get { return happinessModel; } set { happinessModel = value; } }

		/// <summary>
		/// The population icon.
		/// </summary>
		public Image Icon => Pictures.GetIcon(this);

		public Image Icon32 => Icon.Resize(32);

		public IEnumerable<string> IconPaths
		{
			get
			{
				foreach (var x in GetImagePaths(PopulationIconName, "Pop_Portrait"))
					yield return x;

				// fall back on leader portrait if icon not found
				foreach (var x in GetImagePaths(PopulationIconName, "Race_Portrait"))
					yield return x;
			}
		}

		public long ID { get; set; }

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
					if (r.Aptitude?.Name != null)
						factor *= Aptitudes[r.Aptitude.Name] / 100d;
					result += (int)(100 * factor) * r;
				}
				return result;
			}
		}

		public IEnumerable<Ability> IntrinsicAbilities
		{
			get { yield break; }
		}

		public bool IsDisposed { get; set; }

		/// <summary>
		/// The name of this race. Also used for picture names.
		/// </summary>
		public string? Name { get; set; }

		/// <summary>
		/// The atmosphere which this race breathes.
		/// </summary>
		public string? NativeAtmosphere { get; set; }

		/// <summary>
		/// The native planet surface type of this race.
		/// </summary>
		public string? NativeSurface { get; set; }

		/// <summary>
		/// Races have no owner.
		/// </summary>
		public Empire? Owner => null;

		public IEnumerable<IAbilityObject> Parents
		{
			get
			{
				// TODO - return empire?
				yield break;
			}
		}

		/// <summary>
		/// The population icon name for this race.
		/// </summary>
		public string? PopulationIconName { get; set; }

		/// <summary>
		/// The population portrait.
		/// </summary>
		public Image Portrait => Pictures.GetPortrait(this);

		public IEnumerable<string> PortraitPaths => IconPaths;

		/// <summary>
		/// The names of the race's traits.
		/// </summary>
		public IList<string> TraitNames { get; private set; }

		/// <summary>
		/// The traits of the race.
		/// </summary>
		public IEnumerable<Trait> Traits => Mod.Current.Traits.Join(TraitNames, t => t.Name, n => n, (t, n) => t);

		public IEnumerable<Ability> UnstackedAbilities => Abilities;

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

		private ModReference<HappinessModel>? happinessModel { get; set; }

		public static Race Load(string filename)
		{
			var fs = new FileStream(filename, FileMode.Open);
			var race = Serializer.Deserialize<Race>(fs);
			fs.Close(); fs.Dispose();
			return race;
		}

		/// <summary>
		/// Races are known to everyone, though they really should be hidden until first contact...
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		// TODO - hide races until first contact
		public Visibility CheckVisibility(Empire emp) => Visibility.Scanned;

		public void Dispose()
		{
			if (IsDisposed)
				return;
			Galaxy.Current.UnassignID(this);
		}

		public void Save(string filename)
		{
			var fs = new FileStream(filename, FileMode.Create);
			Serializer.Serialize(this, fs);
			fs.Close(); fs.Dispose();
		}

		public override string ToString() => Name ?? string.Empty;

		private IEnumerable<string> GetImagePaths(string? imagename, string imagetype)
		{
			if (imagename is null)
			{
				throw new ArgumentNullException(nameof(imagename));
			}

			if (Mod.Current?.RootPath != null)
			{
				yield return Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", imagename, imagetype);
				yield return Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", imagename, Name + "_" + imagetype);
			}
			yield return Path.Combine("Pictures", "Races", imagename, imagetype);
			yield return Path.Combine("Pictures", "Races", imagename, Name + "_" + imagetype);
		}
	}
}
