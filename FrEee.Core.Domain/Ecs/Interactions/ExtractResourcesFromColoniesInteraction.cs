using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Stats;
using FrEee.Extensions;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Processes.Setup;
using FrEee.Utility;

namespace FrEee.Ecs.Interactions
{
	/// <summary>
	/// Produces resources for an empire at its colonies.
	/// </summary>
	public record ExtractResourcesFromColoniesInteraction
	(
		Empire empire,
		IDictionary<IEntity, ResourceQuantity> Resources
	) : IInteraction
	{
		public ExtractResourcesFromColoniesInteraction(Empire empire)
			: this(empire, new Dictionary<IEntity, ResourceQuantity>()) { }

		public void Execute()
		{
			if (empire is null)
			{
				return;
			}

			foreach (var (extractor, resources) in Resources)
			{
				// TODO: check owner of extractor (can't now because it's a FacilityTemplate)

				foreach (var (resource, amount) in resources)
				{
					// TODO: compute resource value and factor based on colony stats
					var resourceValue = 100;
					var factor = 1d;
					var rate = Galaxy.Current.GameSetup.StandardMiningModel.GetRate(amount, resourceValue, factor);
					empire.StoredResources += rate * resource;
				}
			}
		}
	}
}
