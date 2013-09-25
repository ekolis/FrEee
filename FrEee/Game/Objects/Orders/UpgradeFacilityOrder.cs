using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order for a construction queue to upgrade a facility.
	/// </summary>
	[Serializable]
	public class UpgradeFacilityOrder : IConstructionOrder
	{
		public UpgradeFacilityOrder()
		{
			Owner = Empire.Current;
		}

		/// <summary>
		/// The template of the facility being upgraded to.
		/// </summary>
		[DoNotSerialize]
		public FacilityTemplate Template { get { return template; } set { template = value; } }

		IConstructionTemplate IConstructionOrder.Template { get { return template.Value; } }

		private Reference<FacilityTemplate> template { get; set; }

		[DoNotSerialize]
		public Facility OldFacility { get { return oldFacility; } set { oldFacility = value; } }

		private Reference<Facility> oldFacility;

		/// <summary>
		/// The facility being built.
		/// </summary>
		public Facility NewFacility { get; set; }

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
				if (NewFacility == null)
				{
					NewFacility = Template.Instantiate();
					NewFacility.Owner = queue.Owner;
				}

				// apply build rate
				var costLeft = NewFacility.Cost - NewFacility.ConstructionProgress;
				var spending = ResourceQuantity.Min(costLeft, queue.UnspentRate);
				if (spending < queue.Owner.StoredResources)
				{
					spending = ResourceQuantity.Min(spending, queue.Owner.StoredResources);
					queue.SpaceObject.CreateLogMessage("Construction of " + Template + " at " + queue.SpaceObject + " was delayed due to lack of resources.");
				}
				queue.Owner.StoredResources -= spending;
				queue.UnspentRate -= spending;
				NewFacility.ConstructionProgress += spending;

				// if we're done, delete the old facility and replace it with this one
				if (IsComplete)
				{
					var planet = OldFacility.Container;
					OldFacility.Dispose();
					planet.Colony.Facilities.Add(NewFacility);
				}
			}
		}
		
		IConstructable IConstructionOrder.Item
		{
			get { return NewFacility; }
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
			// validate that new facility is unlocked and is in the same family as the old facility
			if (!queue.Owner.HasUnlocked(Template))
				yield return OldFacility.CreateLogMessage(OldFacility + " on " + OldFacility.Container + " could not be upgraded to a " + Template + " because we have not yet researched the " + Template + ".");
			if (Template.Family != OldFacility.Template.Family)
				yield return OldFacility.CreateLogMessage(OldFacility + " on " + OldFacility.Container + " could not be upgraded to a " + Template + " because facilities cannot be upgraded to facilities of a different family.");
		}

		public bool CheckCompletion(ConstructionQueue queue)
		{
			isComplete = NewFacility.ConstructionProgress >= NewFacility.Cost || GetErrors(queue).Any();
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
					throw new InvalidOperationException("Cannot call IsComplete on a ConstructionOrder without first calling CheckCompletion with a construction queue.");
				return isComplete.Value;
			}
		}
	}
}
