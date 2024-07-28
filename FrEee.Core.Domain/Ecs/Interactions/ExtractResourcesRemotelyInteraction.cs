using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities;
using FrEee.Ecs.Stats;
using FrEee.Extensions;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Processes.Setup;
using FrEee.Utility;

namespace FrEee.Ecs.Interactions
{
	/// <summary>
	/// Produces resources for empires via remote extraction.
	/// </summary>
    public record ExtractResourcesRemotelyInteraction
	(
		Empire Empire,
		IDictionary<IEntity, (IEntity, ResourceQuantity)> Resources
	) : IInteraction
	{
		public ExtractResourcesRemotelyInteraction(Empire empire) 
			: this(empire, new Dictionary<IEntity, (IEntity, ResourceQuantity)>()) { }

		public void Execute()
		{
			if (Empire is null)
			{
				return;
			}

			foreach (var (mineable, (miner, resources)) in Resources)
			{
				var owner = miner?.GetOwner();
				if (owner == Empire)
				{
					foreach (var (resource, amount) in resources)
					{
						// TODO: compute resource value and factor based on mineable/miner stats
						var resourceValue = 100;
						var factor = 1d;
						var rate = Galaxy.Current.GameSetup.RemoteMiningModel.GetRate(amount, resourceValue, factor);
						owner.StoredResources += rate * resource;
					}
				}
			}
		}
	}
}
