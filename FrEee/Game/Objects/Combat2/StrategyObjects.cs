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
        protected StrategyBaseBlock waypointObj {get;set;}
        protected StrategyBaseBlock[] targetObjs {get;set;}
        public StrategyBaseBlock[] blocks { get; set; }
        
        public StrategyObjects()
        {}
        public StrategyObjects(StrategyBaseBlock waypointstratObj, StrategyBaseBlock[] targetstratObjs)
        {
            this.waypointObj = waypointstratObj;
            this.targetObjs = targetstratObjs;
        }

        public combatWaypoint calcWaypiont(CombatObject comObj)
        {
            combatWaypoint wp = (combatWaypoint)waypointObj.getOutput(comObj);  
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



            waypointObj = wpnt;
            blocks = new StrategyBaseBlock[3] { closest1, enloc, envel };

            targetObjs = new StrategyBaseBlock[1] { closest1 };


        }
    }

    public class StrategyBaseBlock
    {
        protected Object[] inputs;
        public Type[] inputtypes { get; protected set; }

        /// <summary>
        /// the string name of this strategy object. 
        /// </summary>
        public string name { get; protected set; }
        
        /// <summary>
        /// the linked input strategy objecs.
        /// </summary>
        public StrategyBaseBlock[] inputLnks { get; set; }

        protected Object output = null;
        public Type outputType { get; protected set; }
        public List<StrategyBaseBlock> outputLnks { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputtypes">the types of inputs this block will receve as an array.</param>
        /// <param name="outputtype">the return type of this block</param>
        public StrategyBaseBlock(Type[] inputtypes, Type outputtype)
        {

            if (inputtypes != null)
            {
                this.inputs = new object[inputtypes.Length]; //inputlist.ToArray();
                this.inputLnks = new StrategyBaseBlock[inputtypes.Length];
            }
            this.inputtypes = inputtypes;
            this.outputType = outputtype;
            outputLnks = new List<StrategyBaseBlock>();
        }

        /// <summary>
        /// the actual data. 
        /// </summary>
        public Object[] Inputs
        {
            get { return this.inputs; }
        }

        /// <summary>
        /// links thisblock to another blocks output.
        /// currently does nothing if the otherblocks output Type does not equal this inputs index Type.
        /// </summary>
        /// <param name="myinputIndx">the input link</param>
        /// <param name="otherblock">the other block</param>
        public void makelink(int myinputIndx, StrategyBaseBlock otherblock)
        {
            if (inputtypes[myinputIndx] == otherblock.outputType)
            {
                this.inputLnks[myinputIndx] = otherblock;
                if (!otherblock.outputLnks.Contains(this))
                    otherblock.outputLnks.Add(this);
            }
            else
            {
                //throw an exception? 
            }
        }

        public void breaklink(int myinputIndx)
        {
            StrategyBaseBlock otherblock = this.inputLnks[myinputIndx];            
            int dex = otherblock.outputLnks.FindIndex(item => item == this);

            otherblock.outputLnks[dex] = null;
            this.inputLnks[myinputIndx] = null;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<StrategyBaseBlock> getlistoflinks()
        {
            List<StrategyBaseBlock> links = new List<StrategyBaseBlock>();
            List<StrategyBaseBlock> links1 = new List<StrategyBaseBlock>();
            List<StrategyBaseBlock> links2 = new List<StrategyBaseBlock>();
            if (this.inputLnks != null)
            {
                foreach (StrategyBaseBlock lnk in this.inputLnks)
                {
                    links1.Add(lnk);
                    links2 = lnk.getlistoflinks();
                    links = links1.Union(links2).ToList();

                }
            }
            return links;
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
                StrategyBaseBlock lnk = inputLnks[i];
                if (lnk.output == null)
                {
                    lnk.calc(comObj);                   
                }
                inputs[i] = lnk.output;
            }
        }
        
    }

    public class StrategyWayPoint : StrategyBaseBlock
    {
        public StrategyWayPoint():base(new Type[2]{typeof(PointXd), typeof(PointXd)}, typeof(combatWaypoint))
        {
            name = "Waypoint";
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            output = new combatWaypoint((PointXd)inputs[0], (PointXd)inputs[1]);
        }
    }

    public class StrategyLocdata : StrategyBaseBlock
    {
        public StrategyLocdata() : base(new Type[1] { typeof(CombatObject) }, typeof(PointXd)) 
        {
            name = "Location";
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            CombatObject obj = (CombatObject)inputs[0];
            output = obj.cmbt_loc;
        }
    }

    public class StrategyVeldata : StrategyBaseBlock
    {
        public StrategyVeldata()
            : base(new Type[1] { typeof(CombatObject) }, typeof(PointXd))
        {
            name = "Velocity";
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            CombatObject obj = (CombatObject)inputs[0];
            output = obj.cmbt_vel;
        }
    }

    public class StrategyMassdata : StrategyBaseBlock
    {
        public StrategyMassdata()
            : base(new Type[1] { typeof(CombatObject) }, typeof(Fix16))
        {
            name = "Mass";
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            CombatObject obj = (CombatObject)inputs[0];
            output = obj.cmbt_mass;
        }
    }

    public class StrategyComObj : StrategyBaseBlock
    {
        public StrategyComObj()
            : base(new Type[1] { typeof(CombatObject) }, typeof(CombatObject))
        {
        }

    }

    public class StrategyThisObj : StrategyBaseBlock
    {
        public StrategyThisObj()
            : base(null, typeof(CombatObject))
        {
            name = "This";
        }

        public override void calc(CombatObject comObj)
        {
            //base.calc(comObj); dont need this, should be at the top of the chain - no inputs.
            output = comObj;
        }
    }

    public class StrategyThisEnemys : StrategyBaseBlock
    {
        public StrategyThisEnemys()
            : base(null, typeof(List<CombatObject>))
        {
            name = "Enemeys of This";
        }

        public override void calc(CombatObject comObj)
        {
            //base.calc(comObj); dont need this, should be at the top of the chain - no inputs.
            output = comObj.empire.hostile;
        }
    }

    public class StrategyThisEmpireObj : StrategyBaseBlock
    {
        public StrategyThisEmpireObj()
            : base(null, typeof(List<CombatObject>))
        {
            name = "Objects in This Empire";
        }

        public override void calc(CombatObject comObj)
        {
            //base.calc(comObj); dont need this, should be at the top of the chain - no inputs.
            output = comObj.empire.ownships;
        }
    }

    public class StrategyRange : StrategyBaseBlock
    {
        public StrategyRange()
            : base(new Type[2] { typeof(CombatObject), typeof(CombatObject) }, typeof(Fix16))
        {
            name = "Range";
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            CombatObject obj0 = (CombatObject)inputs[0];
            CombatObject obj1 = (CombatObject)inputs[1];

            output = Trig.distance(obj0.cmbt_loc, obj1.cmbt_loc);
        }
    }

    public class StrategyClosest:StrategyBaseBlock
    {
        Type filter = typeof(CombatObject);
        public StrategyClosest(CombatObject fromObj, List<CombatObject> comObjList, Type filter = null):
            base (new Type[2]{typeof(CombatObject), typeof(List<CombatObject>)}, typeof(CombatObject))
        {
            name = "Closest Object to:";
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
