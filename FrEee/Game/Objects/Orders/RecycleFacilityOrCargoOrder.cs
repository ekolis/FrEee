using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Orders
{
	public class RecycleFacilityOrCargoOrder : IOrder<IMobileSpaceObject>
	{
		public RecycleFacilityOrCargoOrder(IRecycleBehavior behavior, IRecyclable target)
		{
			Behavior = behavior;
			Target = target;
		}

		public IRecycleBehavior Behavior { get; private set; }

		private GalaxyReference<IRecyclable> target { get; set; }

		/// <summary>
		/// The facility or unit in cargo to recycle.
		/// </summary>
		[DoNotSerialize]
		public IRecyclable Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

		public void Execute(IMobileSpaceObject executor)
		{
			var errors = GetErrors(executor);
			if (errors.Any())
			{
				foreach (var e in errors)
					Owner.Log.Add(e);
				return;
			}

			Behavior.Execute(Target);
			IsComplete = true;
		}

		public IEnumerable<LogMessage> GetErrors(IMobileSpaceObject executor)
		{
			return Behavior.GetErrors(executor, Target);
		}

		public bool CheckCompletion(IMobileSpaceObject executor)
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
			foreach (var v in Galaxy.Current.Referrables.OfType<IMobileSpaceObject>())
				v.RemoveOrder(this);
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
			return Behavior.Verb + " " + Target;
		}

		public bool ConsumesMovement
		{
			get { return false; }
		}
	}
}
