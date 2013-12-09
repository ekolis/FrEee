using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat2
{
	public class CombatReplayLog
	{
		private Dictionary<double, CombatShipsLogs> Logs { get; set; }
		public CombatReplayLog()
		{
			Logs = new Dictionary<double, CombatShipsLogs>();
		}

		public CombatShipsLogs logsforturn(double tic)
		{

			if (Logs.ContainsKey(tic))
				return Logs[tic];

			else
				return null;
		}

		public void addEvent(double tic, long shipID, CombatshipEvent comevnt)
		{
			if (Logs.ContainsKey(tic))
			{
				if (Logs[tic].ContainsKey(shipID))
				{
					Logs[tic][shipID].Add(comevnt);
				}
				else
				{
					Logs[tic].Add(shipID, new List<CombatshipEvent>() { comevnt });
				}
			}
			else
			{
				CombatShipsLogs shipslogs = new CombatShipsLogs(shipID, comevnt);
				Logs.Add(tic, shipslogs);
			}
		}
	}

	public class CombatShipsLogs : Dictionary<long, List<CombatshipEvent>>
	{
		public CombatShipsLogs(long shipID, CombatshipEvent shipevent)
		{
			if (this.ContainsKey(shipID))
			{
				this[shipID].Add(shipevent);
			}
			else
			{
				this.Add(shipID, new List<CombatshipEvent>() { shipevent });
			}
		}
		public List<CombatshipEvent> eventsforship(long shipID)
		{
			if (this.ContainsKey(shipID))
				return this[shipID];
			else
				return null;
		}
	}

	public class CombatshipEvent
	{
		public string EventType { get; protected set; }

		public CombatshipEvent(string type)
		{
			this.EventType = type;
		}
	}
	public class CombatEventLoc : CombatshipEvent
	{
		public Point3d Location { get; private set; }
		public CombatEventLoc(Point3d cmbt_loc)
			: base("Location")
		{
			this.Location = cmbt_loc;
		}
	}
	public class CombatEventFireWeapon : CombatEventLoc
	{
		public CombatEventTakeFire TakeFireEvent { get; private set; }
		public Technology.Component Weapon { get; private set; }
		public CombatEventFireWeapon(Point3d loc, Technology.Component weapon, CombatEventTakeFire targetevent)
			: base(loc)
		{
			this.EventType = "FireWeapon";
			this.Weapon = weapon;
			this.TakeFireEvent = targetevent;
		}
	}
	public class CombatEventTakeFire : CombatEventLoc
	{
		double EndpointTick { get; set; }
		bool IsHit { get; set; }
		public CombatEventTakeFire(double endtic, Point3d endpoint, bool hit)
			: base(endpoint)
		{
			this.EventType = "TakeFire";
			this.EndpointTick = endtic;
			this.IsHit = hit;
		}
	}
}
