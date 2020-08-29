using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;

#nullable enable

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// An asteroid field. Asteroids can be mined or converted to planets.
	/// </summary>
	[Serializable]
	public class AsteroidField : StellarObject, ITemplate<AsteroidField>, IMineableSpaceObject, IDataObject
	{
		public AsteroidField() => ResourceValue = new ResourceQuantity();

		public override AbilityTargets AbilityTarget => AbilityTargets.AsteroidField;

		/// <summary>
		/// The atmospheric composition (e.g. methane, oxygen, carbon dioxide) of this asteroid field.
		/// </summary>
		public string? Atmosphere { get; set; }

		public override bool CanBeObscured => true;

		/// <summary>
		/// Some sort of combat image? Where are these stored anyway?
		/// </summary>
		public string? CombatTile { get; set; }

		public override SafeDictionary<string, object> Data
		{
			get
			{
				var dict = base.Data;
				dict[nameof(size)] = size;
				dict[nameof(Surface)] = Surface;
				dict[nameof(Atmosphere)] = Atmosphere;
				dict[nameof(CombatTile)] = CombatTile;
				dict[nameof(ResourceValue)] = ResourceValue;
				return dict;
			}
			set
			{
				base.Data = value;
				size = value[nameof(size)].Default<ModReference<StellarObjectSize>>();
				Surface = value[nameof(Surface)].Default<string>();
				Atmosphere = value[nameof(Atmosphere)].Default<string>();
				CombatTile = value[nameof(CombatTile)].Default<string>();
				ModID = value[nameof(ModID)].Default<string>();
				ResourceValue = value[nameof(ResourceValue)].Default(new ResourceQuantity());
			}
		}

		public double MineralsValue => ResourceValue[Resource.Minerals];

		public double OrganicsValue => ResourceValue[Resource.Organics];

		public Empire? Owner => null;

		public double RadioactivesValue => ResourceValue[Resource.Radioactives];

		/// <summary>
		/// The resource value of this asteroid field, in %.
		/// </summary>
		public ResourceQuantity ResourceValue { get; set; }

		/// <summary>
		/// The PlanetSize.txt entry for this asteroid field's size.
		/// </summary>
		[DoNotSerialize]
		public StellarObjectSize Size { get => size; set => size = value; }

		/// <summary>
		/// The surface composition (e.g. rock, ice, gas) of this asteroid field.
		/// </summary>
		public string? Surface { get; set; }

		private ModReference<StellarObjectSize>? size { get; set; }

		/// <summary>
		/// Just copy the asteroid field's data.
		/// </summary>
		/// <returns>A copy of the asteroid field.</returns>
		public AsteroidField Instantiate()
		{
			var result = this.CopyAndAssignNewID();
			result.ModID = null;
			return result;
		}
	}
}
