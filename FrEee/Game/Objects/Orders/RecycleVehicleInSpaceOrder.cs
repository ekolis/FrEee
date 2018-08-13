using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Orders
{
	public class RecycleVehicleInSpaceOrder : IOrder<SpaceVehicle>
	{
		public RecycleVehicleInSpaceOrder(IRecycleBehavior behavior)
		{
			Behavior = behavior;
		}

		public IRecycleBehavior Behavior { get; private set; }

		public void Execute(SpaceVehicle executor)
		{
			var errors = GetErrors(executor);
			if (errors.Any())
			{
				foreach (var e in errors)
					Owner.Log.Add(e);
				return;
			}

			Behavior.Execute(executor);
			IsComplete = true;
		}

		public IEnumerable<LogMessage> GetErrors(SpaceVehicle executor)
		{
			return Behavior.GetErrors(executor, executor);
		}

		public bool CheckCompletion(SpaceVehicle executor)
		{
			return IsComplete;
		}

		public bool IsComplete
		{
			get;
			private set;
		}

		public long ID
		{
			get;
			set;
		}

		public bool IsDisposed
		{
			get;
			set;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			foreach (var v in Galaxy.Current.Referrables.OfType<SpaceVehicle>())
				v.Orders.Remove(this);
			Galaxy.Current.UnassignID(this);
		}

		private GalaxyReference<Empire> owner { get; set; }

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get { return owner; } set { owner = value; } }

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// This type does not use client objects, so nothing to do here.
		}

		public override string ToString()
		{
			return Behavior.Verb;
		}

		public bool ConsumesMovement
		{
			get { return false; }
		}
	}
}
