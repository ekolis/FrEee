using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.LogMessages;
using FrEee.Modding;
using FrEee.Game.Objects.Combat;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to warp a mobile space object via a warp point.
	/// </summary>
	[Serializable]
	public class WarpOrder<T> : IMovementOrder<T>
		where T : IMobileSpaceObject, IReferrable
	{
		public WarpOrder(WarpPoint warpPoint)
		{
			Owner = Empire.Current;
			WarpPoint = warpPoint;
		}

		/// <summary>
		/// The warp point we are using.
		/// </summary>
		[DoNotSerialize]
		public WarpPoint WarpPoint { get { return warpPoint; } set { warpPoint = value; } }

		private Reference<WarpPoint> warpPoint {get; set;}

		public void Execute(T sobj)
		{
			var errors = GetErrors(sobj);
			foreach (var error in errors)
				Owner.Log.Add(error);

			if (!errors.Any())
			{
				var here = sobj.FindSector();
				if (here == WarpPoint.FindSector())
				{
					// warp now!!!
					here.Remove(sobj);

					// warp point turbulence damage?
					if (WarpPoint.HasAbility("Warp Point - Turbulence"))
					{
						var dmg = WarpPoint.GetAbilityValue("Warp Point - Turbulence").ToInt();
						sobj.TakeDamage(new Hit(new Shot(null, null, sobj, 0), sobj, dmg));
						if (sobj.IsDestroyed)
						{
							sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " was destroyed by turbulence when traversing " + WarpPoint + "."));
							return;
						}
						else
							sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " took " + dmg + " points of damage from turbulence when traversing " + WarpPoint + "."));
					}

					WarpPoint.Target.Place(sobj);
					sobj.RefreshDijkstraMap();

					// mark system explored
					if (!WarpPoint.TargetStarSystemLocation.Item.ExploredByEmpires.Contains(((ISpaceObject)sobj).Owner))
						WarpPoint.TargetStarSystemLocation.Item.ExploredByEmpires.Add(((ISpaceObject)sobj).Owner);

					// done warping
					IsComplete = true;
				}
				else
				{
					// can't warp here, maybe the GUI should have issued a move order?
					((ISpaceObject)sobj).Owner.Log.Add(sobj.CreateLogMessage(sobj + " cannot warp via " + WarpPoint + " because it is not currently located at the warp point."));
				}
			}

			// spend time
			sobj.SpendTime(sobj.TimePerMove);
		}

		public bool IsComplete
		{
			get;
			private set;
		}

		public override string ToString()
		{
			return "Warp via " + WarpPoint.Name + " in " + WarpPoint.FindStarSystem();
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			foreach (var v in Galaxy.Current.Referrables.OfType<IMobileSpaceObject>())
				v.RemoveOrder(this);
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

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// This type does not use client objects, so nothing to do here.
		}

		public long ID { get; set; }

		public Sector Destination
		{
			get { return WarpPoint.Target; }
		}

		public IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start)
		{
			return Pathfinder.Pathfind(me, start, Destination, false, true, me.DijkstraMap);
		}

		public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> CreateDijkstraMap(IMobileSpaceObject me, Sector start)
		{
			return Pathfinder.CreateDijkstraMap(me, start, Destination, false, true);
		}

		[DoNotSerialize]
		public bool LoggedPathfindingError
		{
			get;
			private set;
		}

		public bool CheckCompletion(T v)
		{
			return IsComplete;
		}

		public IEnumerable<LogMessage> GetErrors(T executor)
		{
			// this order doesn't error
			yield break;
		}

		public bool IsDisposed { get; set; }

		public bool ConsumesMovement
		{
			get { return true; }
		}
	}
}
