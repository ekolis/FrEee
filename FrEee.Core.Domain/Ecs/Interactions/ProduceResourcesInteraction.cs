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
	/// Produces resources for an empire.
	/// </summary>
    public record ProduceResourcesInteraction
	(
		IDictionary<IEntity, ResourceQuantity> ColonyResources,
		IDictionary<IEntity, (IEntity, ResourceQuantity)> RemoteResources
	) : IInteraction
	{
		public ProduceResourcesInteraction() : this(new Dictionary<IEntity, ResourceQuantity>(), new Dictionary<IEntity, (IEntity, ResourceQuantity)>()) { }

		// TODO: raw resource income
		public ResourceQuantity TotalResources =>
			ColonyResources.Sum(q => q.Value)
			+ RemoteResources.Sum(q => q.Value.Item2);

		public void Execute()
		{
			foreach (var (colony, resources) in ColonyResources)
			{
				// TODO: resources need to be associated with ownable objects
				// such as facilities or colonies, not facility templates which are unowned
				// or else add an empire to the dictionary...
				var owner = colony?.GetOwner();
				if (owner is not null)
				{
					foreach (var (resource, amount) in resources)
					{
						// TODO: compute resource value and factor based on colony stats
						var resourceValue = 100;
						var factor = 1d;
						var rate = Galaxy.Current.GameSetup.StandardMiningModel.GetRate(amount, resourceValue, factor);
						owner.StoredResources += rate * resource;
					}
				}
			}
			foreach (var (mineable, (miner, resources)) in RemoteResources)
			{
				var owner = miner?.GetOwner();
				if (owner is not null)
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
