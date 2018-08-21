using FrEee.Game.Interfaces;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Combat
{
	/// <summary>
	/// A hit by a weapon or other source of damage.
	/// </summary>
	public class Hit : IFormulaHost
	{
		public Hit(Shot shot, IDamageable target, int nominalDamage)
		{
			Shot = shot;
			Target = target;
			NominalDamage = nominalDamage;
		}

		/// <summary>
		/// The nominal damage inflicted by this hit, not accounting for special damage types and target defenses.
		/// </summary>
		public int NominalDamage { get; set; }

		/// <summary>
		/// The shot which inflicted this hit.
		/// </summary>
		public Shot Shot { get; set; }

		/// <summary>
		/// The specific target of this hit.
		/// </summary>
		public IDamageable Target { get { return target == null ? null : target.Value; } set { target = value.ReferViaGalaxy(); } }

		public IDictionary<string, object> Variables
		{
			get
			{
				var sv = Shot.Variables;
				var result = new SafeDictionary<string, object>();
				foreach (var v in sv)
					result.Add(v);
				result["target"] = Target;
				return result;
			}
		}

		private GalaxyReference<IDamageable> target { get; set; }
	}
}