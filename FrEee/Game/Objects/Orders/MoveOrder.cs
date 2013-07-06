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
	/// An order to move a mobile space object to a new location.
	/// </summary>
	[Serializable]
	public class MoveOrder<T> : IMobileSpaceObjectOrder<T>
		where T : IMobileSpaceObject<T>, IReferrable
	{
		public MoveOrder(Sector destination, bool avoidEnemies)
		{
			Destination = destination;
			AvoidEnemies = avoidEnemies;
			// TODO - add flag for "avoid damaging sectors"? but how to specify in UI?
		}

		/// <summary>
		/// The sector we are moving to.
		/// </summary>
		[DoNotSerialize]
		public Sector Destination { get { return destination; } set { destination = value; } }

		private Reference<Sector> destination {get; set;}

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
			return Pathfinder.Pathfind(me, start, Destination, AvoidEnemies);
		}

		public void Execute(T sobj)
		{
			// TODO - movement logs
			if (sobj.FindSector() == Destination)
				IsComplete = true;
			else
			{
				var gotoSector = Pathfind(sobj, sobj.FindSector()).FirstOrDefault();
				if (gotoSector != null)
				{
					// move
					sobj.FindSector().SpaceObjects.Remove(sobj);
					gotoSector.SpaceObjects.Add(sobj);

					// apply damage from damaging sectors
					foreach (var damager in gotoSector.SpaceObjects.Where(dsobj => dsobj.HasAbility("Sector - Damage")))
					{
						var damage = damager.GetAbilityValue("Sector - Damage").ToInt();
						// TODO - let sector damage have special damage types?
						// TODO - use a static damage type instance for normal damage
						sobj.TakeDamage(new Combat.DamageType { Name = "Normal" }, damage, null);
						sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " took " + damage + " damage from entering " + damager + "'s sector."));
						sobj.ReplenishShields();
					}
				}
				else if (!LoggedPathfindingError)
				{
					// log pathfinding error
					sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " could not move to " + Destination + " because there is no available path available leading toward " + Destination + "."));
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
			var coords = Destination.Coordinates;
			if (AvoidEnemies)
				return "Move to " + Destination.FindStarSystem().Name + " (" + coords.X + ", " + coords.Y + ")";
			else
				return "Attack " + Destination.FindStarSystem().Name + " (" + coords.X + ", " + coords.Y + ")";
		}

		/// <summary>
		/// Did we already log a pathfinding error this turn?
		/// </summary>
		[DoNotSerialize]
		public bool LoggedPathfindingError { get; private set; }
	}
}
