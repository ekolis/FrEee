using System.Collections.Generic;
using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Objects.GameState;
using FrEee.Serialization;
using FrEee.Utility;
using Newtonsoft.Json.Linq;

namespace FrEee.Processes.Combat;

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
        NominalDamage = nominalDamage ?? shot.DamageLeft;
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
    public Shot Shot { get; set; }

    /// <summary>
    /// The specific target of this hit.
    /// </summary>
    [DoNotSerialize]
    public IDamageable Target { get; set; }

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

	private GameReference<IDamageableReferrable> target
	{
		get
        {
            if (Target is IDamageableReferrable dr)
                return dr.ReferViaGalaxy();
            else
                return null;
		}
        set
        {
            Target = value?.Value;
            if (Target is IDamageableReferrable dr)
                _target = null;
            else
                _target = Target;
		}
	}

	private IDamageable _target { get; set; }
}