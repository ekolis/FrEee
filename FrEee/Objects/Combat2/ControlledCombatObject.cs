using FixMath.NET;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using NewtMath.f16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat2
{
	/// <summary>
	/// A combat object which can be controlled by an empire.
	/// </summary>
	public abstract class CombatControlledObject : CombatObject
	{
		protected CombatControlledObject(ICombatant start, ICombatant working, PointXd position, PointXd vector, int battleseed, string IDprefix)
			: base(working, position, vector, start.ID, IDprefix)
		{
			//StartCombatant = working; wtf?
            StartCombatant = start;
			WorkingCombatant = working;
			newDice(battleseed);
			RefreshWeapons();
		}

        /// <summary>
        /// the fleet to which this object belongs.
        /// </summary>
        public CombatFleet combatfleet { get; set; }

		/// <summary>
		/// The object's state at the start of combat.
		/// </summary>
		[DoNotAssignID]
		public ICombatant StartCombatant { get; protected set; }

		/// <summary>
		/// Weapons equipped by this combatant.
		/// </summary>
		public IEnumerable<CombatWeapon> Weapons { get; protected set; }

		/// <summary>
		/// The current state of the object.
		/// </summary>
		public ICombatant WorkingCombatant { get { return (ICombatant)WorkingObject; } private set { WorkingObject = value; } }

		/// <summary>
		/// endgoal for helm is for the  ship to get to and match speed with the comObj.targetWaypiont as fast as possible.
		/// the strategic AI script should be responsible for setting where the waypoint is and where thet waypoint is going. 
		/// </summary>
		/// <param name="comObj"></param>
		public override void helm()
		{

			if (waypointTarget == null)
				return; // nothing to do
            Compass angletoWaypoint = new Compass(this.cmbt_loc, this.waypointTarget.cmbt_loc); //relitive to me. 

            Tuple<Compass, bool?> nav = Nav(angletoWaypoint);
            Compass angletoturn = nav.Item1;
            bool? thrustToWaypoint = nav.Item2;

			turnship(angletoturn, angletoWaypoint);

			thrustship(angletoturn, thrustToWaypoint);

			//this.lastVectortoWaypoint = vectortowaypoint;
		}

        protected override Tuple<Compass, bool?> Nav(Compass angletoWaypoint)
        {
#if DEBUG
			Console.WriteLine("NAV FUNCTION FOR " + this.WorkingObject);
#endif

            Compass angletoturn = new Compass();
            bool? thrustToWaypoint = true;
            

            this.debuginfo += "HelmInfo:" + "\r\n";
            var ship = this.WorkingCombatant;
            string name = ship.Name;
            //rotate ship
            Fix16 timetoturn = (Fix16)0;
            //Compass angletoturn = new Compass(Trig.angleto(comObj.cmbt_face, comObj.waypointTarget.cmbt_loc));
            CombatWaypoint wpt = this.waypointTarget;

#if DEBUG
			Console.WriteLine("Waypoint is " + wpt);
#endif
            
            angletoturn.Degrees = angletoWaypoint.Degrees - this.cmbt_head.Degrees;
			var bearingToTurn = angletoturn.Degrees <= 180 ? angletoturn.Degrees : angletoturn.Degrees - 360;
            PointXd vectortowaypoint = this.cmbt_loc - this.waypointTarget.cmbt_loc;

#if DEBUG
			Console.WriteLine("Current location is " + cmbt_loc);
			Console.WriteLine("Waypoint location is " + waypointTarget.cmbt_loc);
			Console.WriteLine("So the vector to waypoint is " + vectortowaypoint);
			Console.WriteLine("And the waypoint is " + vectortowaypoint.Length + " away.");

			Console.WriteLine("Current velocity is " + cmbt_vel);
			Console.WriteLine("Current speed is " + cmbt_vel.Length);
			Console.WriteLine("Waypoint velocity is " + waypointTarget.cmbt_vel);
			Console.WriteLine("So we need to change our velocity by " + (waypointTarget.cmbt_vel - cmbt_vel));
#endif

            //if (comObj.lastVectortoWaypoint != null)
            //    angletoturn.Radians = Trig.angleA(vectortowaypoint - comObj.lastVectortoWaypoint);

            timetoturn = (angletoturn.Degrees / this.maxRotate.Degrees); // seconds
            Fix16 oneEightytime = (180 / this.maxRotate.Degrees); // seconds

#if DEBUG
			Console.WriteLine("Current heading is " + cmbt_head);
			Console.WriteLine("Angle to waypoint is " + angletoWaypoint);
			Console.WriteLine("So we need to turn by " + bearingToTurn + " degrees");
			Console.WriteLine("We can turn at " + this.maxRotate + " per second.");
			Console.WriteLine("So we can rotate to face the target in " + timetoturn + " seconds.");
			Console.WriteLine("And we can do a 180 in " + oneEightytime + " seconds.");
#endif

            //PointXd offsetVector = comObj.waypointTarget.cmbt_vel - comObj.cmbt_vel; // O = a - b
            //PointXd combinedVelocity = comObj.cmbt_vel - comObj.waypointTarget.cmbt_vel;
            //PointXd distancePnt = comObj.waypointTarget.cmbt_loc - comObj.cmbt_loc;
            //double closingSpeed = Trig.dotProduct(combinedVelocity, distancePnt);
            Fix16 closingSpeed = NMath.closingRate(this.cmbt_loc, this.cmbt_vel, this.waypointTarget.cmbt_loc, this.waypointTarget.cmbt_vel);

#if DEBUG
			Console.WriteLine("We are approaching/departing our target at a relative speed of " + closingSpeed);
#endif

            Fix16 myspeed = this.cmbt_vel.Length; //seconds

            Fix16 maxFowardAcceleration = this.maxfowardThrust / this.cmbt_mass; //seconds

            Fix16 timetokill_ClosingSpeed = closingSpeed / maxFowardAcceleration; //t = v / a in seconds. 
            

            Fix16 maxStrafeAcceleration = this.maxStrafeThrust / this.cmbt_mass;

            Fix16 timetokill_ClosingSpeed_strafe = closingSpeed / maxStrafeAcceleration; //in seconds. 
            Fix16 timetokill_MySpeed = myspeed / (this.maxfowardThrust / this.cmbt_mass); //in seconds.

#if DEBUG
			Console.WriteLine("Forward/strafe accel is " + maxFowardAcceleration + "/" + maxStrafeAcceleration);
			Console.WriteLine("Can kill closing speed in this many seconds using forward/strafe accel: " + timetokill_ClosingSpeed + "/" + timetokill_ClosingSpeed_strafe);
			Console.WriteLine("Can come to a complete stop in " + timetokill_MySpeed + " seconds.");
#endif
            
            Fix16 distance = vectortowaypoint.Length;


			// TODO - should we divide by ticks per second here, so we can close in one tick?
            Fix16 nominaldistance = maxStrafeAcceleration * timetokill_ClosingSpeed_strafe; //this.maxStrafeThrust; (I think this should be acceleration, not thrust. 

            //Fix16 nominaltime = strafetimetokill_ClosingSpeed
            Fix16 timetowpt = distance / closingSpeed;
#if DEBUG
            Console.WriteLine("Time to waypoint (if we were in a 1D universe): " + timetowpt);
#endif
            
            string helmdo = "";

            if (closingSpeed > (Fix16)0) //getting closer?
            {
#if DEBUG
				Console.WriteLine("***Approaching target!***");
#endif
                /*if (timetowpt <= timetokill_ClosingSpeed_strafe)  // close to the waypoint (within strafe thrust range)
                {
                    thrustToWaypoint = null; // should attempt to match speed using strafe thrust
                }*/
                if (timetowpt <= timetokill_ClosingSpeed + oneEightytime) // if/when we're going to overshoot the waypoint.
                {
                    if (timetowpt < timetokill_ClosingSpeed_strafe) //if time to waypoint is less than time to kill speed with strafe thrusters
                    {
#if DEBUG
						Console.WriteLine("***Going too fast! Slow down!***");
#endif
                        thrustToWaypoint = false; // thrust AWAY from the waypoint! slow down!
                    }
                    else
                    {
#if DEBUG
						Console.WriteLine("***Going just the right speed...***");
#endif
                        helmdo = "null" + "\r\n";
                        thrustToWaypoint = null; // driiift! iiiin! spaaaace! should use only strafe thrust to match speed
                    }
                }
				else
				{
#if DEBUG
					Console.WriteLine("***We can go faster! Speed it up!***");
#endif
					// accelerate toward the target, since we still have time to slow down later
					thrustToWaypoint = true;
				}
            }
            else
            {
#if DEBUG
				Console.WriteLine("***Not approaching target! Need to get closer!***");
#endif
                thrustToWaypoint = true;// getting farther away or maintaining distance, just thrust toward the target
            }

            if (thrustToWaypoint == false)
            {
                helmdo = "Initiating Turnaround" + "\r\n"; //turn around and thrust the other way
                angletoturn.Degrees = (angletoWaypoint.Degrees - (Fix16)180) - this.cmbt_head.Degrees; //turn around and thrust the other way
                angletoturn.normalize();
            }
            else if (thrustToWaypoint == null)
            {
				// TODO - shouldn't this be using delta V between target and ship instead of target V?
				// and shouldn't the angle be relative?
                angletoturn.Radians = Trig.angleA(this.waypointTarget.cmbt_vel);
            }

            this.debuginfo += "timetowpt:\t" + timetowpt.ToString() + "\r\n";
            this.debuginfo += "strafetime:\t" + timetokill_ClosingSpeed_strafe.ToString() + "\r\n";
            this.debuginfo += "speedkilltime:\t" + timetokill_ClosingSpeed.ToString() + "\r\n";
            this.debuginfo += "180time:\t" + oneEightytime.ToString() + "\r\n";
            this.debuginfo += "ThrustTo:\t" + thrustToWaypoint.ToString() + "\r\n";
            this.debuginfo += helmdo + "\r\n";

            return new Tuple<Compass, bool?>(angletoturn, thrustToWaypoint);
        }

		/// <summary>
		/// Refreshes the list of available weapons that this combat object can fire.
		/// </summary>
		protected abstract void RefreshWeapons();

		public override string ToString()
		{
			return WorkingCombatant.Name;
		}

		/*/// <summary>
        /// attempt to move firecontrol to the combatObject
        /// </summary>
        /// <param name="tic_countr"></param>
        /// <param name="IsReplay"></param>
        /// <param name="ReplayLog"></param>

        public override void firecontrol(int tic_countr, bool IsReplay, CombatReplayLog ReplayLog)
        {
            //is a ship, base, unit, or planet
            //ControlledCombatObject ccobj = (ControlledCombatObject)comObj;
            foreach (var wpn in Weapons)
            {
                ICombatant ship = (ICombatant)WorkingObject;

                if (weaponTarget.Count() > 0 && //if there ARE targets
                    wpn.CanTarget(weaponTarget[0].WorkingObject) && //if we CAN target 
                    tic_countr >= wpn.nextReload) //if the weapon is ready to fire.
                {
                    if (wpn.isinRange(this, weaponTarget[0]))
                    {
                        //this function figures out if there's a hit, deals the damage, and creates an event.

                        //first create the event for the target ship
                        CombatTakeFireEvent targets_event = FireWeapon(tic_countr, this, wpn, weaponTarget[0]);
                        //then create teh event for this ship firing on the target
                        CombatFireOnTargetEvent attack_event = new CombatFireOnTargetEvent(tic_countr, this, cmbt_loc, wpn, targets_event);
                        targets_event.fireOnEvent = attack_event;

                        if (!IsReplay)
                        {
                            ReplayLog.Events.Add(targets_event);
                            ReplayLog.Events.Add(attack_event);
                        }

                    }
                }
            }
            //update any events where this ship has taken fire, and set the location. 
            if (!IsReplay)
            {
                foreach (CombatEvent comevnt in ReplayLog.EventsForObjectAtTick(this, tic_countr))
                {
                    if (comevnt.GetType() == typeof(CombatTakeFireEvent))
                    {
                        CombatTakeFireEvent takefire = (CombatTakeFireEvent)comevnt;
                        takefire.setLocation(cmbt_loc);
                    }
                }
            }
        }
        */
	}
}
