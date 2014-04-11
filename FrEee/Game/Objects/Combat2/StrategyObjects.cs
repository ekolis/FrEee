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
        StrategyBaseObj[] waypointObjs;
        StrategyBaseObj[] targetObjs;

        public StrategyObjects(StrategyBaseObj[] waypointstratObjs, StrategyBaseObj[] targetstratObjs)
        {
            this.waypointObjs = waypointstratObjs;
            this.targetObjs = targetstratObjs;
        }

        public combatWaypoint calcWaypiont()
        {
            combatWaypoint wp = (combatWaypoint)waypointObjs[0].getOutput;  
            return wp;
        }

        public CombatObject calcTarget()
        {
            CombatObject tgt = (CombatObject)targetObjs[0].getOutput;           
            return tgt;
        }

    }

 

    class StrategyBaseObj
    {
        protected Object[] inputs;
        protected Type[] inputtypes;
        protected StrategyBaseObj[] inputLnks { get; set; }
        protected bool hasvalidInputs = false;

        protected Object output = null;
        protected Type outputType;
        protected StrategyBaseObj[] outputLnks { get; set; }

        public StrategyBaseObj(Type[] inputtypes, Type outputtype)
        {
            this.inputs = new object[1];
            this.inputtypes = inputtypes;
            this.outputType = outputtype;
        }

        public Object[] Inputs
        {
            get { return this.inputs; }
        }

        public Object getOutput
        {
            get
            {
                if (output == null)
                    calc();
                return this.output;
            }
        }

        public virtual void calc()
        {
            foreach (StrategyBaseObj lnk in inputLnks)
            {
                if (lnk.output == null)
                {
                    lnk.calc();
                }
            }
        }
        
    }

    class StrategyWayPoint : StrategyBaseObj
    {
        public StrategyWayPoint():base(new Type[2]{typeof(PointXd), typeof(PointXd)}, typeof(combatWaypoint))
        {
        }
        public override void calc()
        {
            base.calc();
            output = new combatWaypoint((PointXd)inputs[0], (PointXd)inputs[1]);
        }
    }

    class StrategyComObj : StrategyBaseObj
    {
        public StrategyComObj()
            : base(new Type[1] { typeof(CombatObject) }, typeof(CombatObject))
        {
        }

    }

    class StrategyClosest:StrategyBaseObj
    {
        Type filter = typeof(CombatObject);
        public StrategyClosest(CombatObject fromObj, List<CombatObject> comObjList, Type filter = null):
            base (new Type[2]{typeof(CombatObject), typeof(List<CombatObject>)}, typeof(CombatObject))
        {
            if (filter != null)
            {
                this.filter = filter; //because I cant do Type filter = typeof() in the constuctor perameters.
            }
        }

        public override void calc()
        {
            base.calc();
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
                output = closest;
            }
        }
    }
}
