using FrEee.Modding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// A planet. Planets can be colonized or mined.
	/// </summary>
	public class Planet : StellarObject, ITemplate<Planet>
	{
		public Planet()
		{
			ResourceValue = new Resources();
		}

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
		/// Description of this planet. (For flavor)
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Planet abilities take into account abilities on the colony if one is present.
		/// </summary>
		public override IEnumerable<Ability> Abilities
		{
			get
			{
				// TODO - take into account colony abilities once we have colonies
				return IntrinsicAbilities;
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
		[JsonIgnore]
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
		public virtual void Redact(Galaxy galaxy, StarSystem starSystem, Visibility visibility)
		{
			base.Redact(galaxy, starSystem, visibility);
			if (visibility == Visibility.Fogged)
				Colony = null;
			// TODO - memory sight
		}
	}
}
