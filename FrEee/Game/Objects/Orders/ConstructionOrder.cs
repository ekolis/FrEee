using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Utility; using FrEee.Utility.Serialization;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order for a construction queue to build something.
	/// </summary>
	[Serializable]
	public class ConstructionOrder<T, TTemplate> : IConstructionOrder
		where T : IConstructable
		where TTemplate : ITemplate<T>, IReferrable, IConstructionTemplate
	{
		public ConstructionOrder()
		{
			Owner = Empire.Current;
		}

		/// <summary>
		/// The construction template.
		/// </summary>
		[DoNotSerialize]
		public TTemplate Template { get { return template; } set { template = value; } }

		IConstructionTemplate IConstructionOrder.Template { get { return template.Value; } }

		private Reference<TTemplate> template { get; set; }

		/// <summary>
		/// The item being built.
		/// </summary>
		public T Item { get; set; }

		/// <summary>
		/// Does 1 turn's worth of building.
		/// </summary>
		public void Execute(ConstructionQueue queue)
		{
			var errors = GetErrors(queue);
			foreach (var error in errors)
				queue.Owner.Log.Add(error);

			if (!errors.Any())
			{
				// create item if needed
				if (Item == null)
				{
					Item = Template.Instantiate();
					Item.Owner = queue.Owner;
				}

				// apply build rate
				var costLeft = Item.Cost - Item.ConstructionProgress;
				var spending = ResourceQuantity.Min(costLeft, queue.UnspentRate);
				if (spending < queue.Owner.StoredResources)
				{
					spending = ResourceQuantity.Min(spending, queue.Owner.StoredResources);
					queue.SpaceObject.CreateLogMessage("Construction of " + Template + " at " + queue.SpaceObject + " was delayed due to lack of resources.");
				}
				queue.Owner.StoredResources -= spending;
				queue.UnspentRate -= spending;
				Item.ConstructionProgress += spending;
			}
		}
		
		IConstructable IConstructionOrder.Item
		{
			get { return Item; }
		}

		public void Dispose()
		{
			// TODO - remove from queue, but we don't know which object we're on...
			Galaxy.Current.UnassignID(this);
		}

		private Reference<Empire> owner { get; set; }

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get { return owner; } set { owner = value; } }

		/// <summary>
		/// Orders are visible only to their owners.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Visible;
			return Visibility.Unknown;
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			template.ReplaceClientIDs(idmap);
		}

		public long ID { get; set; }


		public IEnumerable<LogMessage> GetErrors(ConstructionQueue queue)
		{
			// validate that what's being built is unlocked
			if (!queue.Owner.HasUnlocked(Template))
				yield return Template.CreateLogMessage(Template + " cannot be built at " + queue.SpaceObject + " because we have not yet researched it.");

		}

		public bool CheckCompletion(ConstructionQueue queue)
		{
			isComplete = Item.ConstructionProgress >= Item.Cost || GetErrors(queue).Any();
			return IsComplete;
		}

		[DoNotSerialize]
		private bool? isComplete
		{
			get;
			set;
		}

		public bool IsComplete
		{
			get
			{
				if (isComplete == null)
					return false; // haven't checked completion yet, so it's probably safe to say it's incomplete
				return isComplete.Value;
			}
		}


		public ResourceQuantity Cost
		{
			get { return Template.Cost; }
		}
	}
}
