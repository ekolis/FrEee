using FrEee.Game.Objects.Technology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat2
{
	public class CombatReplayLog
	{
		public ICollection<CombatEvent> Events { get; private set; }

		public CombatReplayLog()
		{
			Events = new List<CombatEvent>();
		}

		public IEnumerable<CombatEvent> EventsForTick(int tick)
		{
			return Events.Where(e => e.Tick == tick);
		}

		public IEnumerable<CombatEvent> EventsForObject(CombatObject obj)
		{
			return Events.Where(e => e.Object == obj).OrderBy(e => e.Tick);
		}

		public IEnumerable<CombatEvent> EventsForObjectAtTick(CombatObject obj, int tick)
		{
			return Events.Where(e => e.Object == obj && e.Tick == tick);
		}
	}

	public abstract class CombatEvent
	{
		public int Tick { get; private set; }
		public CombatObject Object { get; private set; }

		protected CombatEvent(int tick, CombatObject obj)
		{
			Tick = tick;
			Object = obj;
		}
	}

	public class CombatLocationEvent : CombatEvent
	{
		public Point3d Location { get; private set; }
		public CombatLocationEvent(int tick, CombatObject obj, Point3d cmbt_loc)
			: base(tick, obj)
		{
			this.Location = cmbt_loc;
		}
	}
	public class CombatFireEvent : CombatLocationEvent
	{
		public CombatTakeFireEvent TakeFireEvent { get; private set; }
		public Technology.Component Weapon { get; private set; }

		public CombatFireEvent(int tick, CombatObject obj, Point3d loc, Component weapon, CombatTakeFireEvent targetevent)
			: base(tick, obj, loc)
		{
			this.Weapon = weapon;
			this.TakeFireEvent = targetevent;
		}
	}
	public class CombatTakeFireEvent : CombatLocationEvent
	{
		bool IsHit { get; set; }
		public CombatTakeFireEvent(int tick, CombatObject obj, Point3d endpoint, bool hit)
			: base(tick, obj, endpoint)
		{
			this.IsHit = hit;
		}
	}
}
