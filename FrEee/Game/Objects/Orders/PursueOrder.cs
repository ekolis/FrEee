using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using System.Drawing;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Enumerations;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to move a mobile space object toward another space object.
	/// </summary>
	[Serializable]
	public class PursueOrder<T> : IMobileSpaceObjectOrder<T>
		where T : IMobileSpaceObject<T>, IReferrable
	{
		public PursueOrder(ISpaceObject target, bool avoidEnemies)
		{
			Owner = Empire.Current;
			Target = target;
			AvoidEnemies = avoidEnemies;
			if (Galaxy.Current != null && Galaxy.Current.PlayerNumber > 0)
				Galaxy.Current.Register(this);
			// TODO - add flag for "avoid damaging sectors"? but how to specify in UI?
		}

		/// <summary>
		/// The target we are pursuing.
		/// </summary>
		[DoNotSerialize]
		public ISpaceObject Target { get { return target.Value; } set { target = value.Reference(); } }

		private Reference<ISpaceObject> target {get; set;}

		/// <summary>
		/// Should pathfinding avoid enemies?
		/// </summary>
		public bool AvoidEnemies { get; set; }

		/// <summary>
		/// Finds the path for executing this order.
		/// </summary>
		/// <param name="sobj">The space object executing the order.</param>
		/// <returns></returns>
		public IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start)
		{
			if (AvoidEnemies && Target.IsHostileTo(me.Owner))
			{
				// don't avoid the target!
				return Pathfinder.Pathfind(me, start, Target.FindSector(), AvoidEnemies, true).Concat(new Sector[]{Target.FindSector()});
			}
			else
				return Pathfinder.Pathfind(me, start, Target.FindSector(), AvoidEnemies, true);
		}

		public void Execute(T sobj)
		{
			// TODO - movement logs
			if (sobj.FindSector() == Target.FindSector())
				IsComplete = true;
			else
			{
				var gotoSector = Pathfind(sobj, sobj.FindSector()).FirstOrDefault();
				if (gotoSector != null)
				{
					// move
					sobj.FindSector().SpaceObjects.Remove(sobj);
					gotoSector.SpaceObjects.Add(sobj);
				}
				else if (!LoggedPathfindingError)
				{
					// log pathfinding error
					sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " could not pursue " + Target + " because there is no available path available leading toward " + Target + "."));
					LoggedPathfindingError = true;
				}
			}

			// spend time
			sobj.TimeToNextMove += sobj.TimePerMove;
		}

		public bool IsComplete
		{
			get;
			private set;
		}

		public override string ToString()
		{
			if (AvoidEnemies)
				return "Escort " + Target;
			else
				return "Pursue " + Target;
		}

		/// <summary>
		/// Did we already log a pathfinding error this turn?
		/// </summary>
		[DoNotSerialize]
		public bool LoggedPathfindingError { get; private set; }

		public void Dispose()
		{
			Galaxy.Current.Unregister(this);
		}

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		public Empire Owner
		{
			get;
			private set;
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

		public void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			// This type does not use client objects, so nothing to do here.
		}
	}
}
