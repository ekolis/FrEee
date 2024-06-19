using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Modding;

namespace FrEee.Ecs.Abilities
{
	public static class AbilityFactory
	{
		public static Ability Build(Type abilityType, IAbilityObject container, AbilityRule rule, Formula<string>? description, params Formula<string>[] values)
		{
			var constructor = abilityType.GetConstructor([
				typeof(IAbilityObject),
				typeof(AbilityRule),
				typeof(Formula<string>),
				typeof(Formula<string>[])
			]);
			var ability = (Ability)constructor.Invoke([
					container,
					rule,
					description,
					values
				]);
			return ability;
		}
	}
}
