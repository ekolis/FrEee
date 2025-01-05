using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Modding.Abilities;
using FrEee.Objects.Civilization;
using FrEee.Processes.Combat.Grid;
using FrEee.Utility;
using FrEee.Vehicles;

namespace FrEee.Processes.Construction;

/// <summary>
/// Stock implementation of <see cref="IConstructionQueueService"/>.
/// </summary>
public class ConstructionQueueService
	: IConstructionQueueService
{
	public IConstructionQueue CreateConstructionQueue(IConstructor constructor)
	{
		return new ConstructionQueue(constructor);
	}

	public ResourceQuantity ComputeRate(IConstructionQueue queue)
	{
		var rate = ComputeSYAbilityRate(queue);
		if (queue.Colony != null)
		{
			if (rate is null)
			{
				rate = Mod.Current.Settings.DefaultColonyConstructionRate;
			}

			// apply population modifier
			var pop = queue.Colony.Population.Sum(p => p.Value);
			if (pop == 0)
			{
				// colonies without population can't construct
				return new ResourceQuantity();
			}
			rate *= Mod.Current.Settings.GetPopulationConstructionFactor(pop);

			// apply mood modifier
			// TODO - load mood modifier from mod
			var moodModifier = queue.Colony.Mood == Mood.Rioting ? 0 : 100;
			rate *= moodModifier / 100d;

			var ratios = queue.Colony.Population.Select(p => new { Race = p.Key, Ratio = p.Value / (double)pop });

			// apply racial trait planetary SY modifier
			// TODO - should Planetary SY Rate apply only to planets that have space yards, or to all planetary construction queues?
			double traitmod = 1d;
			foreach (var ratio in ratios)
			{
				traitmod += ratio.Race.GetAbilityValue("Planetary SY Rate").ToDouble() / 100d * ratio.Ratio;
			}
			rate *= traitmod;

			// apply aptitude modifier
			if (queue.IsSpaceYardQueue)
			{
				double aptmod = 0d;
				foreach (var ratio in ratios)
				{
					aptmod += ratio.Race.Aptitudes[Aptitude.Construction.Name] / 100d * ratio.Ratio;
				}
				rate *= aptmod;

				// apply culture modifier
				rate *= (100d + (queue.Owner?.Culture?.Construction ?? 0)) / 100d;
			}
		}
		rate ??= [];
		if (queue.Container is IVehicle && queue.Owner is not null)
		{
			// apply aptitude modifier for empire's primary race
			rate *= queue.Owner.PrimaryRace.Aptitudes[Aptitude.Construction.Name] / 100d;
		}

		return rate;
	}

	private ResourceQuantity? ComputeSYAbilityRate(IConstructionQueue queue)
	{
		if (queue.Container.HasAbility("Space Yard"))
		{
			var rate = new ResourceQuantity();
			// TODO - moddable resources?
			for (int i = 1; i <= 3; i++)
			{
				var amount = queue.Container.GetAbilityValue("Space Yard", 2, true, true, a => a.Value1 == i.ToString()).ToInt();
				Resource? res = i switch
				{
					1 => Resource.Minerals,
					2 => Resource.Organics,
					3 => Resource.Radioactives,
					_ => null,
				};
				if (res is not null)
				{
					rate[res] = amount;
				}
			}
			return rate;
		}
		else
		{
			return null;
		}
	}
}
