﻿using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
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
		public UpgradeFacilityOrder(FacilityUpgrade fu)
		{
			Owner = Empire.Current;
			Upgrade = fu;
		}

		/// <summary>
		/// The upgrade to perform.
		/// </summary>
		public FacilityUpgrade Upgrade { get; set; }

		IConstructionTemplate IConstructionOrder.Template { get { return Upgrade.New; } }

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
					NewFacility = Upgrade.New.Instantiate();

				// apply build rate
				var costLeft = Cost - NewFacility.ConstructionProgress;
				var spending = ResourceQuantity.Min(costLeft, queue.UnspentRate);
				if (spending < queue.Owner.StoredResources)
				{
					spending = ResourceQuantity.Min(spending, queue.Owner.StoredResources);
					queue.Container.CreateLogMessage("Construction of " + Upgrade.New + " at " + queue.Container + " was delayed due to lack of resources.");
				}
				queue.Owner.StoredResources -= spending;
				queue.UnspentRate -= spending;
				NewFacility.ConstructionProgress += spending;

				// if we're done, delete the old facility and replace it with this one
				if (CheckCompletion(queue))
				{
					var planet = (Planet)queue.Container;
					planet.Colony.Facilities.Where(f => f.Template == Upgrade.Old).First().Dispose();
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
			if (IsDisposed)
				return;
			foreach (var v in Galaxy.Current.Referrables.OfType<ConstructionQueue>())
				v.Orders.Remove(this);
			Galaxy.Current.UnassignID(this);
		}

		private GalaxyReference<Empire> owner { get; set; }

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

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				Upgrade.ReplaceClientIDs(idmap, done);
			}
		}

		public long ID { get; set; }

		public IEnumerable<LogMessage> GetErrors(ConstructionQueue queue)
		{
			// validate that new facility is unlocked
			if (!queue.Owner.HasUnlocked(Upgrade.New))
				yield return Upgrade.Old.CreateLogMessage(Upgrade.Old + " on " + queue.Container + " could not be upgraded to a " + Upgrade.New + " because we have not yet researched the " + Upgrade.New + ".");

			// validate that new and old facilities are in the same family
			if (Upgrade.New.Family != Upgrade.Old.Family)
				yield return Upgrade.Old.CreateLogMessage(Upgrade.Old + " on " + queue.Container + " could not be upgraded to a " + Upgrade.New + " because facilities cannot be upgraded to facilities of a different family.");

			// validate that there is a facility to upgrade
			var planet = (Planet)queue.Container;
			var colony = planet.Colony;
			if (!colony.Facilities.Any(f => f.Template == Upgrade.Old))
				yield return planet.CreateLogMessage("There are no " + Upgrade.Old + "s on " + planet + " to upgrade.");
		}

		public bool CheckCompletion(ConstructionQueue queue)
		{
			isComplete = NewFacility.ConstructionProgress >= Cost || GetErrors(queue).Any();
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
			get { return Upgrade.Cost; }
		}

		public bool IsDisposed { get; set; }

		public bool ConsumesMovement
		{
			get { return false; }
		}

		public string Name
		{
			get
			{
				return "Upgrade to " + Upgrade.New.Name;
			}
		}
	}
}
