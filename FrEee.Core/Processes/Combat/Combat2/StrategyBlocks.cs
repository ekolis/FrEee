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

        public System.Drawing.Point GUIloc { get; set; }


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
		/// </summary>
		/// <param name="myinputIndx">the input link</param>
		/// <param name="otherblock">the other block</param>
		public void makelink(int myinputIndx, StrategyBaseBlock otherblock)
		{
			if (inputtypes == null)
				throw new InvalidOperationException("Input types for strategy block not initialized.");
			if (inputtypes.Length <= myinputIndx)
				throw new ArgumentOutOfRangeException("This strategy block does not have that many inputs.");
			if (!inputtypes[myinputIndx].Type.IsAssignableFrom(otherblock.outputType.Type))
				throw new ArgumentException("Cannot link an output of type " + otherblock.outputType + " to an input of type " + inputtypes[myinputIndx] + ".");

			this.inputLnks[myinputIndx] = otherblock;
			if (!otherblock.outputLnks.Contains(this))
				otherblock.outputLnks.Add(this);
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

		/// <summary>
		/// Resets all in
		/// </summary>
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
			for (int i = 0; i < inputLnks.Length; i++)
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
		public StrategyWayPoint()
			: base(new Type[2] { typeof(PointXd), typeof(PointXd) }, new object[] { new PointXd(), new PointXd() }, typeof(CombatWaypoint))
		{
			name = "Waypoint";
		}
		public override void calc(CombatObject comObj)
		{
			base.calc(comObj);
			output = new CombatWaypoint((PointXd)inputs[0], (PointXd)inputs[1]);
		}
	}

	public class StrategyLocdata : StrategyBaseBlock
	{
		public StrategyLocdata()
			: base(new Type[1] { typeof(CombatObject) }, new object[] { null }, typeof(PointXd))
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
			: base(new Type[1] { typeof(CombatObject) }, new object[] { null }, typeof(PointXd))
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
			: base(new Type[1] { typeof(CombatObject) }, new object[] { null }, typeof(Fix16))
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
			: base(new Type[1] { typeof(CombatObject) }, new object[] { null }, typeof(CombatObject))
		{
		}
		public override void calc(CombatObject comObj)
		{
			base.calc(comObj);
			var cobj = (CombatObject)inputs[0];
			output = cobj;
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

    public class Strategyfleet : StrategyBaseBlock
    {
        public Strategyfleet()
            : base(new Type[1]{typeof(CombatObject)}, new object[0], typeof(List<CombatObject>))
        {
            name = "Fleet Objects";
        }

        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            CombatControlledObject targetObj = inputs[0] as CombatControlledObject;
            output = targetObj.combatfleet.combatObjects;
        }
    }

	public class StrategyRange : StrategyBaseBlock
	{
		public StrategyRange()
			: base(new Type[2] { typeof(CombatObject), typeof(CombatObject) }, new object[] { null, null }, typeof(Fix16))
		{
			name = "Range";
		}
		public override void calc(CombatObject comObj)
		{
			base.calc(comObj);
			CombatObject obj0 = (CombatObject)inputs[0];
			CombatObject obj1 = (CombatObject)inputs[1];

			output = NMath.distance(obj0.cmbt_loc, obj1.cmbt_loc);
		}
	}

	public class StrategyClosest : StrategyBaseBlock
	{
		Type filter = typeof(CombatObject);
		//public StrategyClosest(CombatObject fromObj, List<CombatObject> comObjList, Type filter = null):
		public StrategyClosest() :
			base(new Type[3] { typeof(CombatObject), typeof(List<CombatObject>), typeof(SafeType) }, new object[] { null, new List<CombatObject>(), new SafeType(typeof(CombatObject)) }, typeof(CombatObject))
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
            filter = inputs[2].GetType(); 
			Fix16 distance = Fix16.MaxValue;


			if (comObjects != null && comObjects.Count > 0)
			{
				CombatObject closest = comObjects[0];
				foreach (CombatObject othercomObj in comObjects)
				{
					Fix16 thisdist = NewtMath.f16.NMath.distance(thisObj.cmbt_loc, othercomObj.cmbt_loc);
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
		public StrategyWeapons() :
			base(new Type[2] { typeof(CombatObject), typeof(CombatWeapon) }, new object[] { null, null }, typeof(List<CombatWeapon>))
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
			: base(new Type[1] { typeof(Fix16) }, new object[] {null}, typeof(Fix16))
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

    public class Strategyinput_Types : StrategyBaseBlock
    {
        public SafeType inputtype { get; set; }
        public Strategyinput_Types()
            : base(new Type[1] { typeof(SafeType) }, new object[] { (SafeType)typeof(CombatControlledObject) }, typeof(SafeType))
        {
            name = "Type";
            inputtype = typeof(CombatControlledObject);
        }

        public override void calc(CombatObject comObj)
        {
            //base.calc(comObj); dont need this, should be at the top of the chain - no inputs.
            output = inputtype;
        }
    }

    public class StrategyFilter_shieldhitpoints : StrategyBaseBlock
    {
        /// <summary>
        /// input[0] = list of objects to filter
        /// input[1] = Fix16 min
        /// input[2] = Fix16 max
        /// </summary>
        public StrategyFilter_shieldhitpoints()
            : base(new Type[3] { typeof(List<CombatObject>), typeof(Fix16), typeof(Fix16) }, new object[3] { null, 0, Fix16.MaxValue }, typeof(List<CombatObject>))
        {
            name = "Filter";

        }

        public override void calc(CombatObject comObj)
        {
            base.calc(comObj); //get inputs
            List<CombatObject> comObjects = (List<CombatObject>)inputs[0];
            
            Fix16 min = (Fix16)inputs[1];
            Fix16 max = (Fix16)inputs[2];
            List<CombatObject> filteredObjects = new List<CombatObject>();
            foreach (CombatObject loopcomobj in comObjects)
            {
                Fix16 checkitem = loopcomobj.WorkingObject.ShieldHitpoints;
                if (checkitem > min && checkitem < max)
                    filteredObjects.Add(loopcomobj);
            }
            output = filteredObjects;
        }
    }

    public class StrategyFilter_HullSize : StrategyBaseBlock
    {
        /// <summary>
        ///input[0] = list of objects to filter
        ///input[1] = Fix16 min
        ///input[2] = Fix16 max
        /// </summary>
        public StrategyFilter_HullSize()
            : base(new Type[3] { typeof(List<CombatObject>), typeof(Fix16), typeof(Fix16) }, new object[3] { null, 0, Fix16.MaxValue }, typeof(List<CombatObject>))
        {
            name = "Filter Hull Size";

        }

        public override void calc(CombatObject comObj)
        {
            base.calc(comObj); //get inputs
            List<CombatObject> comObjects = (List<CombatObject>)inputs[0];
            Fix16 min = (Fix16)inputs[1];
            Fix16 max = (Fix16)inputs[2];
            List<CombatObject> filteredObjects = new List<CombatObject>();
            foreach (CombatObject comobj in comObjects)
            {
                if (comobj.cmbt_mass > min && comobj.cmbt_mass < max)
                    filteredObjects.Add(comobj);
            }
            output = filteredObjects;
        }
    }

	public class StrategyWeaponMaxRange : StrategyBaseBlock
	{
		public StrategyWeaponMaxRange()
            : base(new Type[1] { typeof(List<CombatWeapon>) }, new object[] { null }, typeof(Fix16))
		{

		}
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            Fix16 rangecurrent = 0;
            List<CombatWeapon> weapons = inputs[0] as List<CombatWeapon>;
            foreach (CombatWeapon wpn in weapons)
            {
                
            }
        }
	}

    public class StrategyFormate : StrategyBaseBlock
    {
        public StrategyFormate()
            : base(new Type[3] { typeof(CombatObject), typeof(Fix16), typeof(Compass) }, new object[] { null }, typeof(PointXd))
        {
            name = "Formate";
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            CombatObject formateObj = (CombatObject)inputs[0];
            Fix16 range = (Fix16)inputs[1];
            Compass angle = (Compass)inputs[2];
            //angle.Degrees += formateObj.cmbt_head.Degrees;
            PointXd formatePoint = Trig.sides_ab(range, angle.Radians);
            formatePoint += formateObj.cmbt_loc;
            output = formatePoint;
        }
    }

    public class StrategyVelAngle : StrategyBaseBlock
    {
        /// <summary>
        /// outputs an angle of the target objects velocity vector. 
        /// </summary>
        public StrategyVelAngle()
            : base(new Type[1] { typeof(CombatObject) }, new object[] { null }, typeof(Compass))
        {
            name = "VelocityAngle";
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            CombatObject targetObj = (CombatObject)inputs[0];
            Compass angle = new Compass(Trig.angleA(targetObj.cmbt_vel));
            output = angle;
        }
    }

    public class StrategyHeading : StrategyBaseBlock
    {
        /// <summary>
        /// outputs an angle of the target objects heading. 
        /// </summary>
        public StrategyHeading()
            : base(new Type[1] { typeof(CombatObject) }, new object[] { null }, typeof(Compass))
        {
            name = "Heading";
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            CombatObject targetObj = (CombatObject)inputs[0];
            Compass angle = new Compass(targetObj.cmbt_head.Degrees, false);
            output = angle;
        }
    }

    public class StrategyAdd_Compass : StrategyBaseBlock
    {
        public StrategyAdd_Compass()
            : base(new Type[2] { typeof(Compass), typeof(Compass) }, new object[] { null, null }, typeof(Compass))
        {
            name = "Add Compass";
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            Compass c1 = (Compass)inputs[0];
            Compass c2 = (Compass)inputs[1];
            output = c1 + c2;
        }
    }

    public class StrategyAdd_angletocompass : StrategyBaseBlock
    {
        public StrategyAdd_angletocompass()
            : base(new Type[2] { typeof(Compass), typeof(Fix16) }, new object[] { null, null }, typeof(Compass))
        {
            name = "Add Angle";
        }
        public override void calc(CombatObject comObj)
        {
            base.calc(comObj);
            Compass c1 = (Compass)inputs[0];
            Fix16 c2 = (Fix16)inputs[1];
            output = c1.Degrees + c2;
        }
    }

}
