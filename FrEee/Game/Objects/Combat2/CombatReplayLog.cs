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
    }

    public class CombatshipEvent
    {
        string evnttype;
        //something component/weapon type
        CombatObject target;
        bool hit;
        double endpoint_tic;
        Point3d endpoint;
        CombatshipEvent targetevent;
        public CombatshipEvent(string type)
        { 
            this.evnttype = type;
        }

        public void CombatEventLoc(Point3d loc)
        {
 
            this.evnttype = "loc";

        }
        public void CombatEventFireWeapon(Point3d loc,   CombatshipEvent targetevent)
        { 
            this.evnttype = "loc";
            this.targetevent = targetevent;
        }

        public void targetEvent(double endtic, Point3d endpoint, bool hit)
        {
            this.endpoint_tic = endtic;
            this.endpoint = endpoint;
            this.hit = hit;
        }
    }
}
