using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewtMath.f16;
using FixMath.NET;

namespace FrEee.Game.Objects.Combat2
{
    class StrategyObjects
    {
        StrategyBaseObj[] StratObjs;

        public StrategyObjects(StrategyBaseObj[] stratObjs)
        {
            this.StratObjs = stratObjs;
        }

        public combatWaypoint calcWaypiont()
        { 
            combatWaypoint wp = new combatWaypoint();
            Object obj = null;
            foreach (StrategyBaseObj stratobj in StratObjs)
            {
                obj = stratobj.get;
            }
            if (obj.GetType() == typeof(combatWaypoint))
                wp = (combatWaypoint)obj;
            return wp;
        }

    }


    class StrategyBaseObj
    {
        protected Object[] inputs;
        protected Type[] inputtypes;

        protected Object output = null;
        protected Type outputType;

        public StrategyBaseObj(Object[] inputs, Type[] inputtypes, Type outputtype)
        {
            this.inputs = inputs;
            this.inputtypes = inputtypes;
            this.outputType = outputtype;
        }

        public Object[] Inputs
        {
            get { return this.inputs; }
        }

        public Object get
        {
            get
            {
                if (output == null)
                    calc();
                return this.output;
            }
        }

        public void calc()
        {}
        
    }

    class StrategyClosest:StrategyBaseObj
    {
        Type filter = typeof(CombatObject);
        public StrategyClosest(CombatObject fromObj, List<CombatObject> comObjList, Type filter = null):base (new Object[2]{fromObj, comObjList}, new Type[2]{typeof(CombatObject), typeof(List<CombatObject>)}, typeof(CombatObject))
        {
            if (filter != null)
            {
                this.filter = filter; //because I cant do Type filter = typeof() in the constuctor perameters.
            }
        }

        public override void calc()
        {
            List<CombatObject> comObjects = (List<CombatObject>)inputs[1];
            CombatObject thisObj = (CombatObject)inputs[0];
            Fix16 distance = Fix16.MaxValue;
            CombatObject closest = comObjects[0];

            if (comObjects != null)
            {
                
                foreach (CombatObject comObj in comObjects)
                {
                    Fix16 thisdist = NewtMath.f16.Trig.distance(thisObj.cmbt_loc, comObj.cmbt_loc);
                    if (closest.GetType() == filter && thisdist < distance)
                    {
                        distance = thisdist;
                        closest = comObj;
                    }
                }
            }
        }
    }
}
