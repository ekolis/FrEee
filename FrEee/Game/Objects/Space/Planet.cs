using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A planet. Planets can be colonized or mined.
	/// </summary>
	 [Serializable] public class Planet : StellarObject, ITemplate<Planet>
	{
		public Planet()
		{
			ResourceValue = new Resources();
		}

		/// <summary>
		/// Used for naming.
		/// </summary>
		public Planet MoonOf { get; set; }

		/// <summary>
		/// The size of this planet.
		/// </summary>
		public Size Size { get; set; }

		/// <summary>
		/// The surface composition (e.g. rock, ice, gas) of this planet.
		/// </summary>
		public string Surface { get; set; }

		/// <summary>
		/// The atmospheric composition (e.g. methane, oxygen, carbon dioxide) of this planet.
		/// </summary>
		public string Atmosphere { get; set; }

		/// <summary>
		/// Planet abilities take into account abilities on the colony if one is present.
		/// </summary>
				public override IEnumerable<Ability> Abilities
		{
			get
			{
				return IntrinsicAbilities.Concat(Colony == null ? Enumerable.Empty<Ability>() : Colony.Abilities);
			}
		}

		/// <summary>
		/// The resource value of this planet, in %.
		/// </summary>
		public Resources ResourceValue { get; set; }

		/// <summary>
		/// Just copy the planet's data.
		/// </summary>
		/// <returns>A copy of the planet.</returns>
		public new Planet Instantiate()
		{
			return this.Clone();
		}

		/// <summary>
		/// The empire which has a colony on this planet, if any.
		/// </summary>
				public override Empire Owner
		{
			get
			{
				return Colony == null ? null : Colony.Owner;
			}
		}

		/// <summary>
		/// The colony on this planet, if any.
		/// </summary>
		public Colony Colony { get; set; }

		/// <summary>
		/// Planets need to have their colony redacted if the empire can't see them anymore.
		/// </summary>
		/// <param name="galaxy"></param>
		/// <param name="starSystem"></param>
		/// <param name="visibility"></param>
		public override void Redact(Galaxy galaxy, StarSystem starSystem, Visibility visibility)
		{
			base.Redact(galaxy, starSystem, visibility);
			if (visibility == Visibility.Fogged)
				Colony = null;
			// TODO - memory sight
		}

		/// <summary>
		/// The resource income from this planet.
		/// </summary>
		public Resources Income
		{
			get
			{
				var income = new Resources();
				var prefix = "Resource Generation - ";
				foreach (var abil in Abilities.ToArray().Where(abil => abil.Name.StartsWith(prefix)))
				{
					var resource = abil.Name.Substring(prefix.Length);
					int amount;
					int.TryParse(abil.Values[0], out amount);

					// do modifiers to income
					amount = (int)(amount * ResourceValue[resource] / 100d);
					// TODO - other modifiers (population, happiness, robotoid factories, etc.)

					income.Add(resource, amount);
				}
				return income;
			}
		}
	}
}
