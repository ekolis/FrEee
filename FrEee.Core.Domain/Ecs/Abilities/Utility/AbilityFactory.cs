using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Modding;

namespace FrEee.Ecs.Abilities.Utility
{
    public static class AbilityFactory
    {
        public static Ability Build(Type abilityType, IEntity container, AbilityRule rule, Formula<string>? description, params Formula<string>[] values)
        {
            var constructor = abilityType.GetConstructor([
                typeof(IEntity),
                typeof(AbilityRule),
                typeof(Formula<string>[])
            ]);
            var ability = (Ability)constructor.Invoke([
                    container,
                    rule,
                    rule.PrefixValues.Concat(values).ToArray()
                ]);
            return ability;
        }
    }
}
