using FrEee.Game.Objects.Technology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NewtMath.f16;

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
			return Events.Where(e => e.Object != null && e.Object.ID == obj.ID).OrderBy(e => e.Tick);
		}

		public IEnumerable<CombatEvent> EventsForObjectAtTick(CombatObject obj, int tick)
		{
			return Events.Where(e => e.Object != null && e.Object.ID == obj.ID && e.Tick == tick);
		}

		public IEnumerable<CombatTakeFireEvent> HitsAgainst(CombatObject obj)
		{
			return Events.OfType<CombatTakeFireEvent>().Where(e => e.Object == obj && e.IsHit);
		}
	}

	public abstract class CombatEvent
	{
		public int Tick { get; set; }
		public CombatObject Object { get; private set; }

		protected CombatEvent(int tick, CombatObject obj)
		{
			Tick = tick;
			Object = obj;
		}
	}

	public class CombatLocationEvent : CombatEvent
	{
		public PointXd Location { get; protected set; }
		public CombatLocationEvent(int tick, CombatObject obj, PointXd cmbt_loc)
			: base(tick, obj)
		{
			this.Location = cmbt_loc;
		}
	}
	public class CombatFireOnTargetEvent : CombatLocationEvent
	{
		public CombatTakeFireEvent TakeFireEvent { get; private set; }
		public CombatWeapon Weapon { get; private set; }
        

        /// <summary>
        /// this event is for the object that is firing apon something else
        /// </summary>
        /// <param name="tick"></param>
        /// <param name="obj"></param>
        /// <param name="loc"></param>
        /// <param name="weapon"></param>
        /// <param name="targetevent">the event for the target ship</param>
		public CombatFireOnTargetEvent(int tick, CombatObject obj, PointXd loc, CombatWeapon weapon, CombatTakeFireEvent targetevent)
			: base(tick, obj, loc)
		{
			this.Weapon = weapon;
			this.TakeFireEvent = targetevent;
		}
	}

    public class CombatTakeFireEvent : CombatLocationEvent
	{
		public bool IsHit { get; set; }
        public CombatNode BulletNode { get; set; }
        public CombatFireOnTargetEvent fireOnEvent { get; set; }
        /// <summary>
        /// This event is for the object under fire.
        /// </summary>
        /// <param name="tick"></param>
        /// <param name="obj"></param>
        /// <param name="endpoint"></param>
        /// <param name="hit">whether this ship is hit or if the shot is a miss</param>
		public CombatTakeFireEvent(int tick, CombatObject obj, PointXd endpoint, bool hit)
			: base(tick, obj, endpoint)
		{
			this.IsHit = hit;
		}

        public void setLocation(PointXd location)
        {
            base.Location = location;
        }
	}
   

    public class CombatDestructionEvent : CombatLocationEvent
    {
        public CombatDestructionEvent(int tick, CombatObject obj, PointXd point)
            : base(tick, obj, point)
        {}
    }

    public class CombatEndBattleEvent : CombatEvent
    {
        public CombatEndBattleEvent(int tick)
            : base(tick, null)
        { }
    }
}
