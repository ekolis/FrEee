using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat2
{
    public class CombatReplayLog
    {
        Dictionary<double, CombatShipsLogs> logs;
        public CombatReplayLog()
        {

        }

        public CombatShipsLogs logsforturn(double tic)
        {

            if (logs.ContainsKey(tic))            
                return logs[tic];
            
            else 
                return null;
        }

        public void addEvent(double tic, long shipID ,CombatshipEvent comevnt)
        {
            if (logs.ContainsKey(tic))
            {
                if (logs[tic].ContainsKey(shipID))
                {
                    logs[tic][shipID].Add(comevnt);
                }
                else
                {
                    logs[tic].Add(shipID, new List<CombatshipEvent>() { comevnt });
                }               
            }
            else
            {
                CombatShipsLogs shipslogs = new CombatShipsLogs(shipID, comevnt);
                logs.Add(tic, shipslogs);
            }
        }
    }

    public class CombatShipsLogs : Dictionary<long , List<CombatshipEvent>>
    {
        public CombatShipsLogs(long shipID, CombatshipEvent shipevent)
        {
            if (this.ContainsKey(shipID))
            {
                this[shipID].Add(shipevent);
            }
            else
            {
                this.Add(shipID, new List<CombatshipEvent>() {shipevent});
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
        protected string evnttype;
                
        public CombatshipEvent(string type)
        { 
            this.evnttype = type;
        }

        public string type()
        {
            return evnttype;
        }
    }
    public class CombatEventLoc : CombatshipEvent
    {
        Point3d loc;
        public CombatEventLoc(Point3d cmbt_loc) : base("Location")
        {
            this.loc = cmbt_loc;
        }
    }
    public class CombatEventFireWeapon : CombatEventLoc
    {
        CombatEventTakeFire takefireevent;
        Technology.Component weapon;
        public CombatEventFireWeapon(Point3d loc, Technology.Component weapon, CombatEventTakeFire targetevent) : base(loc)
        {
            this.evnttype = "FireWeapon";
            this.weapon = weapon;         
            this.takefireevent = targetevent;  
        }
    }
    public class CombatEventTakeFire : CombatEventLoc
    {

        double endpoint_tic;
        bool hit;
        public CombatEventTakeFire(double endtic, Point3d endpoint, bool hit) : base(endpoint)
        {
            this.evnttype = "TakeFire";
            this.endpoint_tic = endtic;            
            this.hit = hit;
        }
    }
}
