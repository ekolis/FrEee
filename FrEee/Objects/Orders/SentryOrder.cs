using FrEee.Enumerations;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;
using FrEee.Utility;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Objects.Orders
{
	/// <summary>
	/// An order for a mobile space object to hold position until enemies are sighted in the system.
	/// </summary>
	[Serializable]
	public class SentryOrder : IOrder
	{
		public SentryOrder()
		{
			Owner = Empire.Current;
		}

		public bool ConsumesMovement
		{
			get { return true; }
		}

		public long ID { get; set; }

		public bool IsComplete
		{
			get;
			set;
		}

		public bool IsDisposed { get; set; }

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get { return owner; } set { owner = value; } }

		private GalaxyReference<Empire> owner { get; set; }

		public bool CheckCompletion(IOrderable v)
		{
			return IsComplete;
		}

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

		public void Dispose()
		{
			if (IsDisposed)
				return;
			foreach (var v in Galaxy.Current.Referrables.OfType<SpaceVehicle>())
				v.Orders.Remove(this);
			Galaxy.Current.UnassignID(this);
		}

		public void Execute(IOrderable ord)
		{
			if (ord is IMobileSpaceObject sobj)
			{
				// if hostiles in system, we are done sentrying
				if (sobj.FindStarSystem().FindSpaceObjects<ICombatSpaceObject>(s => s.IsHostileTo(sobj.Owner)).Any())
					IsComplete = true;

				// spend time
				sobj.SpendTime(sobj.TimePerMove);
			}
			else
				ord.Owner.RecordLog(ord, $"{ord} cannot sentry because it is not a mobile space object.", LogMessageType.Error);
		}

		public IEnumerable<LogMessage> GetErrors(IOrderable executor)
		{
			// this order doesn't error
			yield break;
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// This type does not use client objects, so nothing to do here.
		}

		public override string ToString()
		{
			return "Sentry";
		}
	}
}