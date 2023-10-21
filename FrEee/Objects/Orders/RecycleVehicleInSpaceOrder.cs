using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;
using FrEee.Utility;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Objects.Orders
{
	public class RecycleVehicleInSpaceOrder : IOrder
	{
		public RecycleVehicleInSpaceOrder(IRecycleBehavior behavior)
		{
			Behavior = behavior;
		}

		public IRecycleBehavior Behavior { get; private set; }

		public bool ConsumesMovement
		{
			get { return false; }
		}

		public long ID
		{
			get;
			set;
		}

		public bool IsComplete
		{
			get;
			set;
		}

		public bool IsDisposed
		{
			get;
			set;
		}

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get { return owner; } set { owner = value; } }

		private GalaxyReference<Empire> owner { get; set; }

		public bool CheckCompletion(IOrderable executor)
		{
			return IsComplete;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			foreach (var v in The.Game.Referrables.OfType<SpaceVehicle>())
				v.Orders.Remove(this);
			The.ReferrableRepository.Remove(this);
		}

		public void Execute(IOrderable executor)
		{
			var errors = GetErrors(executor);
			if (errors.Any() && Owner != null)
			{
				foreach (var e in errors)
					Owner.Log.Add(e);
				return;
			}

			Behavior.Execute((IRecyclable)executor);
			IsComplete = true;
		}

		public IEnumerable<LogMessage> GetErrors(IOrderable executor)
		{
			return Behavior.GetErrors(executor as IMobileSpaceObject, executor as IRecyclable);
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// This type does not use client objects, so nothing to do here.
		}

		public override string ToString()
		{
			return Behavior.Verb;
		}
	}
}