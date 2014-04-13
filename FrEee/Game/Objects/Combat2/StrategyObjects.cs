using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewtMath.f16;
using FixMath.NET;

namespace FrEee.Game.Objects.Combat2
{
    public class StrategyObjects
    {
        protected StrategyBaseObj[] waypointObjs {get;set;}
        protected StrategyBaseObj[] targetObjs {get;set;}

        public StrategyObjects()
        {}
        public StrategyObjects(StrategyBaseObj[] waypointstratObjs, StrategyBaseObj[] targetstratObjs)
        {
            this.waypointObjs = waypointstratObjs;
            this.targetObjs = targetstratObjs;
        }

        public combatWaypoint calcWaypiont(CombatObject comObj)
        {
            combatWaypoint wp = (combatWaypoint)waypointObjs[0].getOutput(comObj);  
            return wp;
        }

        public CombatObject calcTarget(CombatObject comObj)
        {
            CombatObject tgt = (CombatObject)targetObjs[0].getOutput(comObj);     
            return tgt;
        }

    }

    public class StragegyObject_Default:StrategyObjects
    {
        public StragegyObject_Default()
            : base()
        {          
            StrategyWayPoint wpnt = new StrategyWayPoint();
            StrategyClosest closest1 = new StrategyClosest(null, null);  
            StrategyThisObj thisobj = new StrategyThisObj(); 
            StrategyThisEnemys thisobjEnemys = new StrategyThisEnemys(); 
            StrategyLocdata enloc = new StrategyLocdata();
            StrategyVeldata envel = new StrategyVeldata();
            
            wpnt.inputLnks[0] = enloc;
            wpnt.inputLnks[1] = envel;

            closest1.inputLnks[0] = thisobj;
            closest1.inputLnks[1] = thisobjEnemys;
            closest1.outputLnks.Add(enloc);
            closest1.outputLnks.Add(envel);

            enloc.inputLnks[0] = closest1;
            enloc.outputLnks.Add(wpnt);

            envel.inputLnks[0] = closest1;
            envel.outputLnks.Add(wpnt);




            waypointObjs = new StrategyBaseObj[4] { wpnt, closest1, enloc, envel };

            targetObjs = new StrategyBaseObj[1] { closest1 };


        }
    }

    public class StrategyBaseObj
    {
        protected Object[] inputs;
        protected Type[] inputtypes;

        /// <summary>
        /// the linked input strategy objecs.
        /// </summary>
        public StrategyBaseObj[] inputLnks { get; set; }
        protected bool hasvalidInputs = false;

        protected Object output = null;
        protected Type outputType;
        public List<StrategyBaseObj> outputLnks { get; set; }

        public StrategyBaseObj(Type[] inputtypes, Type outputtype)
        {

            //List<Object> inputlist = new List<object>();
            //for (int i = 0; i < inputtypes.Length; i++)
            //{
            //    inputlist.Add(null);
            //}
            if (inputtypes != null)
            {
                this.inputs = new object[inputtypes.Length]; //inputlist.ToArray();
                this.inputLnks = new StrategyBaseObj[inputtypes.Length];
            }
            this.inputtypes = inputtypes;
            this.outputType = outputtype;
            outputLnks = new List<StrategyBaseObj>();
        }

        /// <summary>
        /// the actual data. 
        /// </summary>
        public Object[] Inputs
        {
            get { return this.inputs; }
        }

        public Object getOutput(CombatObject comObj)
        {
            if (output == null)
                calc(comObj);
            return this.output;
        }

        public virtual void calc(CombatObject comObj)
        {
            for(int i = 0; i < inputLnks.Length; i++)
            {
                StrategyBaseObj lnk = inputLnks[i];
                if (lnk.output == null)
                {
                    lnk.calc(comObj);                   
                }
                inputs[i] = lnk.output;
            }
        }
        
    }

    public class StrategyWayPoint : StrategyBaseObj
    {
        public StrategyWayPoint():base(new Type[2]{typeof(PointXd), typeof(PointXd)}, typeof(combatWaypoint))
        {
            
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            output = new combatWaypoint((PointXd)inputs[0], (PointXd)inputs[1]);
        }
    }

    public class StrategyLocdata : StrategyBaseObj
    {
        public StrategyLocdata() : base(new Type[1] { typeof(CombatObject) }, typeof(PointXd)) 
        {
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            CombatObject obj = (CombatObject)inputs[0];
            output = obj.cmbt_loc;
        }
    }

    public class StrategyVeldata : StrategyBaseObj
    {
        public StrategyVeldata()
            : base(new Type[1] { typeof(CombatObject) }, typeof(PointXd))
        {
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            CombatObject obj = (CombatObject)inputs[0];
            output = obj.cmbt_vel;
        }
    }

    public class StrategyMassdata : StrategyBaseObj
    {
        public StrategyMassdata()
            : base(new Type[1] { typeof(CombatObject) }, typeof(Fix16))
        {
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            CombatObject obj = (CombatObject)inputs[0];
            output = obj.cmbt_mass;
        }
    }

    public class StrategyComObj : StrategyBaseObj
    {
        public StrategyComObj()
            : base(new Type[1] { typeof(CombatObject) }, typeof(CombatObject))
        {
        }

    }

    public class StrategyThisObj : StrategyBaseObj
    {
        public StrategyThisObj()
            : base(null, typeof(CombatObject))
        { }

        public override void calc(CombatObject comObj)
        {
            //base.calc(comObj); dont need this, should be at the top of the chain - no inputs.
            output = comObj;
        }
    }

    public class StrategyThisEnemys : StrategyBaseObj
    {
        public StrategyThisEnemys()
            : base(null, typeof(List<CombatObject>))
        { }

        public override void calc(CombatObject comObj)
        {
            //base.calc(comObj); dont need this, should be at the top of the chain - no inputs.
            output = comObj.empire.hostile;
        }
    }

    public class StrategyClosest:StrategyBaseObj
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

        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            List<CombatObject> comObjects = (List<CombatObject>)inputs[1];
            CombatObject thisObj = (CombatObject)inputs[0];
            Fix16 distance = Fix16.MaxValue;
            CombatObject closest = comObjects[0];

            if (comObjects != null)
            {
                
                foreach (CombatObject othercomObj in comObjects)
                {
                    Fix16 thisdist = NewtMath.f16.Trig.distance(thisObj.cmbt_loc, othercomObj.cmbt_loc);
                    if (closest.GetType() == filter && thisdist < distance)
                    {
                        distance = thisdist;
                        closest = othercomObj;
                    }
                }
                output = closest;
            }
        }
    }
}
