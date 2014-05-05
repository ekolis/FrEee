using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewtMath.f16;
using FixMath.NET;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Enumerations;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat2
{
    public class StrategyBaseBlock
    {
        protected Object[] inputs;
        public SafeType[] inputtypes { get; protected set; }
		public object[] DefaultValues { get; protected set; }

        /// <summary>
        /// the string name of this strategy object. 
        /// </summary>
        public string name { get; protected set; }
        
        /// <summary>
        /// the linked input strategy objecs.
        /// </summary>
        public StrategyBaseBlock[] inputLnks { get; set; }

        protected Object output = null;
        public SafeType outputType { get; protected set; }
        public List<StrategyBaseBlock> outputLnks { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputtypes">the types of inputs this block will receve as an array.</param>
        /// <param name="outputtype">the return type of this block</param>
        public StrategyBaseBlock(IEnumerable<Type> inputtypes, IEnumerable<object> defaultValues, Type outputtype)
        {

            if (inputtypes != null)
            {
                this.inputs = new object[inputtypes.Count()]; //inputlist.ToArray();
                this.inputLnks = new StrategyBaseBlock[inputtypes.Count()];
            }
            this.inputtypes = inputtypes.Select(t => (SafeType)t).ToArray();
			DefaultValues = defaultValues.ToArray();
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
            if (inputtypes != null && inputtypes[myinputIndx] == otherblock.outputType)
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
                    if (lnk != null)
                    {
                        links1.Add(lnk);
                        links2 = lnk.getlistoflinks();
                        links = links1.Union(links2).ToList();
                    }

                }
            }
            return links;
        }

        public Object getNewOutput(CombatObject comObj)
        {
            
            reCalc(comObj);
            return this.output;
        }

        public Object getOutput(CombatObject comObj)
        {
            if (output == null)
                calc(comObj);
            return this.output;
        }

        public void zeroize()
        {
            if (inputLnks != null)
            {
                for (int i = 0; i < inputLnks.Length; i++)
                {
                    StrategyBaseBlock lnk = inputLnks[i];
                    if (lnk != null)
                    {
                        lnk.zeroize();
                        lnk = null;
                        output = null;
                        inputs[i] = null;
                    }
                }
            }
        }

        public void reCalc(CombatObject comObj)
        {
            zeroize();
            calc(comObj);
        }

        public virtual void calc(CombatObject comObj)
        {
            for(int i = 0; i < inputLnks.Length; i++)
            {
                StrategyBaseBlock lnk = inputLnks[i];
				if (lnk == null)
					inputs[i] = DefaultValues[i];
                else	
				{
					if (lnk.output == null)
					{
						lnk.calc(comObj);                   
					}
					inputs[i] = lnk.output;
				}
            }
        }       
    }

    public class StrategyWayPoint : StrategyBaseBlock
    {
        public StrategyWayPoint():base(new Type[2]{typeof(PointXd), typeof(PointXd)}, new object[]{new PointXd(), new PointXd()}, typeof(combatWaypoint))
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
        public StrategyLocdata() : base(new Type[1] { typeof(CombatObject) }, new object[]{null}, typeof(PointXd)) 
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
            : base(new Type[1] { typeof(CombatObject) }, new object[]{null}, typeof(PointXd))
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
            : base(new Type[1] { typeof(CombatObject) }, new object[]{null}, typeof(Fix16))
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
            : base(new Type[1] { typeof(CombatObject) }, new object[]{null}, typeof(CombatObject))
        {
        }

    }

    public class StrategyThisObj : StrategyBaseBlock
    {
        public StrategyThisObj()
            : base(new Type[0], new object[0], typeof(CombatObject))
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
			: base(new Type[0], new object[0], typeof(List<CombatObject>))
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
			: base(new Type[0], new object[0], typeof(List<CombatObject>))
        {
            name = "Our Objects";
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
            : base(new Type[2] { typeof(CombatObject), typeof(CombatObject) }, new object[]{null, null}, typeof(Fix16))
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
        //public StrategyClosest(CombatObject fromObj, List<CombatObject> comObjList, Type filter = null):
        public StrategyClosest():
            base (new Type[3]{typeof(CombatObject), typeof(List<CombatObject>), typeof(SafeType)}, new object[]{null, new List<CombatObject>(), new SafeType(typeof(CombatObject))}, typeof(CombatObject))
        {
            name = "Closest Object to:";
            //if (filter != null)
            //    this.filter = filter; //because I cant do Type filter = typeof() in the constuctor perameters.
            //else
            //    this.filter = typeof(CombatObject);
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
                    if (filter.IsAssignableFrom(closest.GetType()) && thisdist < distance)
                    {
                        distance = thisdist;
                        closest = othercomObj;
                    }
                }
                output = closest;
            }
        }
    }

    public class StrategyWeapons : StrategyBaseBlock
    {
        Type filter = typeof(CombatWeapon);

        //public StrategyWeapons(CombatObject fromObj, Type filter = null) :
        public StrategyWeapons():
            base(new Type[2] { typeof(CombatObject), typeof(CombatWeapon) }, new object[]{null, null}, typeof(List<CombatWeapon>))
        {
            name = "List of Weapons";
            //if (filter != null)
            //{
            //    this.filter = filter; //because I cant do Type filter = typeof() in the constuctor perameters.
            //}
        }

        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            
            CombatControlledObject ccobj = (CombatControlledObject)comObj;
            List<CombatWeapon> comWpn = ccobj.Weapons.ToList();

        }
    }

    public class Strategyinput_fix16 : StrategyBaseBlock
    {
        public Fix16 inputnum { get; set; }
        public Strategyinput_fix16()
            : base(new Type[1]{typeof(Fix16)}, null, typeof(Fix16))
        {
            name = "Input";
            inputnum = 0;
        }

        public override void calc(CombatObject comObj)
        {
            //base.calc(comObj); dont need this, should be at the top of the chain - no inputs.
            output = inputnum;
        }
    }

    public class StrategyWeaponRange : StrategyBaseBlock
    {
        public StrategyWeaponRange() : base(new Type[1] { typeof(List<CombatWeapon>) }, null, typeof(Fix16)) 
        {
 
        }
    }

}
