using System.Collections.Generic;
using FrEee.Game.Interfaces;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;

#nullable enable

namespace FrEee.Game.Objects.Combat
{
	/// <summary>
	/// A hit by a weapon or other source of damage.
	/// </summary>
	public class Hit : IFormulaHost
	{
		public Hit(Shot shot, IDamageable target, int? nominalDamage = null)
		{
			Shot = shot;
			DamageType = shot?.DamageType ?? DamageType.Normal;
			Target = target;
			NominalDamage = nominalDamage ?? shot?.DamageLeft ?? 0;
		}

		public Hit(DamageType dt, int damage, IDamageable target)
		{
			DamageType = dt;
			Target = target;
			NominalDamage = damage;
		}

		public DamageType DamageType { get; set; }

		/// <summary>
		/// The nominal damage inflicted by this hit, not accounting for special damage types and target defenses.
		/// </summary>
		public int NominalDamage { get; set; }

		/// <summary>
		/// The shot which inflicted this hit.
		/// </summary>
		public Shot? Shot { get; set; }

		/// <summary>
		/// The specific target of this hit.
		/// </summary>
		[DoNotSerialize]
		public IDamageable? Target
		{
			get => target?.Value ?? _target;
			set
			{
				if (value is IDamageableReferrable dr)
					target = dr.ReferViaGalaxy();
				else
					_target = value;
			}
		}

		public IDictionary<string, object?> Variables
		{
			get
			{
				var sv = Shot?.Variables;
				var result = new SafeDictionary<string, object?>();
				foreach (var v in sv ?? new Dictionary<string, object?>())
					result.Add(v);
				result["target"] = Target;
				return result;
			}
		}

		private GalaxyReference<IDamageableReferrable>? target { get; set; }

		private IDamageable? _target { get; set; }
	}
}
