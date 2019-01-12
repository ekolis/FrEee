using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to warp a mobile space object via a warp point.
	/// TODO - make this also pursue the warp point first
	/// </summary>
	[Serializable]
	public class WarpOrder : IOrder<IMobileSpaceObject>
	{
		public WarpOrder(WarpPoint warpPoint)
		{
			Owner = Empire.Current;
			WarpPoint = warpPoint;
		}

		public bool ConsumesMovement
		{
			get { return true; }
		}

		public Sector Destination
		{
			get { return WarpPoint.Target; }
		}

		public long ID { get; set; }

		public bool IsComplete
		{
			get;
			set;
		}

		public bool IsDisposed { get; set; }

		[DoNotSerialize]
		public bool LoggedPathfindingError
		{
			get;
			private set;
		}

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get { return owner; } set { owner = value; } }

		/// <summary>
		/// The warp point we are using.
		/// </summary>
		[DoNotSerialize]
		public WarpPoint WarpPoint { get { return warpPoint; } set { warpPoint = value; } }

		private GalaxyReference<Empire> owner { get; set; }
		private GalaxyReference<WarpPoint> warpPoint { get; set; }

		public bool CheckCompletion(IMobileSpaceObject v)
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

		public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> CreateDijkstraMap(IMobileSpaceObject me, Sector start)
		{
			return Pathfinder.CreateDijkstraMap(me, start, Destination, false, true);
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			foreach (var v in Galaxy.Current.Referrables.OfType<IMobileSpaceObject>())
				v.RemoveOrder(this);
			Galaxy.Current.UnassignID(this);
		}

		public void Execute(IMobileSpaceObject sobj)
		{
			var errors = GetErrors(sobj);
			foreach (var error in errors)
				Owner.Log.Add(error);

			if (!errors.Any())
			{
				var here = sobj.Sector;
				if (here == WarpPoint.Sector)
				{
					// warp now!!!
					here.Remove(sobj);

					// warp point turbulence damage?
					if (WarpPoint.HasAbility("Warp Point - Turbulence"))
					{
						var dmg = WarpPoint.GetAbilityValue("Warp Point - Turbulence").ToInt();
						sobj.TakeNormalDamage(dmg);
						if (sobj.IsDestroyed)
						{
							sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " was destroyed by turbulence when traversing " + WarpPoint + "."));
							return;
						}
						else
							sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " took " + dmg + " points of damage from turbulence when traversing " + WarpPoint + "."));
					}

					sobj.Sector = WarpPoint.Target;
					sobj.RefreshDijkstraMap();

					// mark system explored
					WarpPoint.TargetStarSystemLocation.Item.MarkAsExploredBy(((ISpaceObject)sobj).Owner);

					// done warping
					IsComplete = true;
				}
				else
				{
					// can'IMobileSpaceObject warp here, maybe the GUI should have issued a move order?
					((ISpaceObject)sobj).Owner.Log.Add(sobj.CreateLogMessage(sobj + " cannot warp via " + WarpPoint + " because it is not currently located at the warp point."));
				}
			}

			// spend time
			sobj.SpendTime(sobj.TimePerMove);
		}

		public IEnumerable<LogMessage> GetErrors(IMobileSpaceObject executor)
		{
			// this order doesn'IMobileSpaceObject error
			yield break;
		}

		public IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start)
		{
			return Pathfinder.Pathfind(me, start, Destination, false, true, me.DijkstraMap);
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// This type does not use client objects, so nothing to do here.
		}

		public override string ToString()
		{
			if (WarpPoint == null)
				return "Invalid Warp Order";
			return "Warp via " + WarpPoint.Name + " in " + WarpPoint.FindStarSystem();
		}
	}
}