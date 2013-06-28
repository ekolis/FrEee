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
			Target = target;
			AvoidEnemies = avoidEnemies;
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
				return Pathfinder.Pathfind(me, start, Target.FindSector(), AvoidEnemies).Concat(new Sector[]{Target.FindSector()});
			}
			else
				return Pathfinder.Pathfind(me, start, Target.FindSector(), AvoidEnemies);
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
				else
				{
					// TODO - log a message for the player that pathfinding failed, but only once per space object per turn
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
	}
}
