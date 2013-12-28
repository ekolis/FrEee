using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FrEee.Game.Objects.Combat2
{
	public class Battle_Space : INamed, ILocated, IPictorial
	{
		/// <summary>
		/// Standard battlespace constructor.
		/// </summary>
		/// <param name="location"></param>
		/// <param name="isreplay"></param>
		public Battle_Space(Sector location)
		{
			if (location == null)
				throw new ArgumentNullException("location", "Battles require a sector location.");
			Initialize(location, location.SpaceObjects.OfType<ICombatant>().Where(o => o.Owner != null).Union(location.SpaceObjects.OfType<Fleet>().SelectMany(f => f.Combatants)));
		}

		/// <summary>
		/// Battlespace constructor for unit tests (doesn't need a sector; will create a fake sector).
		/// </summary>
		/// <param name="combatants"></param>
		/// <param name="isreplay"></param>
		public Battle_Space(IEnumerable<ICombatant> combatants)
		{
			Initialize(null, combatants);
		}

		private void Initialize(Sector sector, IEnumerable<ICombatant> combatants)
		{
			if (sector != null)
				Sector = sector;
			else
				Sector = new Sector(new StarSystem(0), new System.Drawing.Point());


			EmpiresArray = combatants.Select(c => c.Owner).Where(emp => emp != null).Distinct().ToArray();
			Empires = new Dictionary<Empire, CombatEmpire> { };

			StartCombatants = new HashSet<ICombatant>();
			foreach (ICombatant obj in combatants)
			{
				// TODO - deal with planets in combat
				ICombatant copy = obj.Copy();
				SpaceVehicle scopy = (SpaceVehicle)copy;

				// copy over the components individually so they can take damage without affecting the starting state
				scopy.Components.Clear();
				foreach (var comp in ((SpaceVehicle)obj).Components)
					scopy.Components.Add(comp.Copy());

				if (scopy.Owner != obj.Owner)
					scopy.Owner.Dispose(); // don't need extra empires!
				scopy.Owner = obj.Owner;
				StartCombatants.Add(scopy);
			}
			WorkingCombatants = new HashSet<ICombatant>();
			ActualCombatants = new HashSet<ICombatant>(combatants);

			CombatNodes = new HashSet<CombatNode>();
			Fleets = new List<Fleet> { };

			foreach (var fleet in Sector.SpaceObjects.OfType<Fleet>())
			{
				Fleets.Add(fleet);
			}
			ReplayLog = new CombatReplayLog();


			double stardate = Galaxy.Current.Timestamp;
			int starday = (int)(Galaxy.Current.CurrentTick * 10);

			int moduloID = (int)(Sector.StarSystem.ID % 100000);
			this.battleseed = (int)(moduloID / stardate * 10);
		}

		static Battle_Space()
		{
			Current = new HashSet<Battle_Space>();
			Previous = new HashSet<Battle_Space>();
		}

		/// <summary>
		/// whether or not this is a processing(false) or a replay(true)
		/// </summary>
		public bool IsReplay { get; set; }

		/// <summary>
		/// seed for this battle.
		/// </summary>
		private int battleseed { get; set; }

		/// <summary>
		/// Any battles that are currently ongoing.
		/// This is a collection so we can multithread battle resolution if so desired.
		/// </summary>
		public static ICollection<Battle_Space> Current { get; private set; }

		/// <summary>
		/// Any battles that have completed this turn.
		/// </summary>
		public static ICollection<Battle_Space> Previous { get; private set; }

		/// <summary>
		/// The sector in which this battle took place.
		/// </summary>
		public Sector Sector { get; private set; }

		/// <summary>
		/// The star system in which this battle took place.
		/// </summary>
		public StarSystem StarSystem { get { return Sector.StarSystem; } }

		/// <summary>
		/// The empires engagaed in battle.
		/// </summary>
		public IEnumerable<Empire> EmpiresArray { get; private set; }
		public Dictionary<Empire, CombatEmpire> Empires { get; private set; }

		/// <summary>
		/// The combatants at the start of this battle.
		/// </summary>
		public ISet<ICombatant> StartCombatants { get; private set; }

		/// <summary>
		/// The working list of combatants in this battle.
		/// </summary>
		public ISet<ICombatant> WorkingCombatants { get; private set; }

		/// <summary>
		/// The REAL combatants objects.
		/// </summary>
		public ISet<ICombatant> ActualCombatants { get; private set; }

		/// <summary>
		/// All combat nodes in this battle, including ships, fighters, seekers, projectiles, etc.
		/// </summary>
		public ISet<CombatNode> CombatNodes { get; private set; }

		/// <summary>
		/// Combat nodes that have an AI attached to them.
		/// This includes ships, bases, units, and seekers.
		/// </summary>
		public IEnumerable<CombatObject> CombatObjects
		{
			get
			{
				return CombatNodes.OfType<CombatObject>();
			}
		}

		/// <summary>
		/// the Fleets in this battle
		/// </summary>
		public ICollection<Fleet> Fleets { get; private set; }

		public CombatReplayLog ReplayLog { get; private set; }

		public Sector sectoratStart { get; private set; }

		private const double ticlen = 0.1; //physics tick length
		public const double CommandFrequency = 10;   //new commands (move, new targets etc) are given every 10 ticks.

		/// <summary>
		/// Battles are named after any stellar objects in their sector; failing that, they are named after the star system and sector coordinates.
		/// </summary>
		/// 
		public string Name
		{
			get
			{
				if (Sector.SpaceObjects.OfType<StellarObject>().Any())
					return "Battle at " + Sector.SpaceObjects.OfType<StellarObject>().Largest();
				var coords = Sector.Coordinates;
				return "Battle at " + Sector.StarSystem + " sector (" + coords.X + ", " + coords.Y + ")";
			}
		}

		private void FirstSetup()
		{
			foreach (Empire empire in EmpiresArray.Where(e => !Empires.ContainsKey(e)))
			{
				Empires.Add(empire, new CombatEmpire());
			}

			foreach (var shipObj in ActualCombatants)
			{

				WorkingCombatants.Add(shipObj);
				CombatObject comObj;
				if (shipObj is SpaceVehicle)
				{
					comObj = new CombatObject((SpaceVehicle)shipObj, battleseed);
				}
				else
					comObj = new CombatObject(shipObj, battleseed); // for unit tests
				CombatNodes.Add(comObj);
				Empires[shipObj.Owner].ownships.Add(comObj);

			}
		}
		private void ReplaySetup()
		{

			//this time WorkingCombatants is filled with a copy of the shipObj instead of the actual one. 
			//this is one place where the sim could go slightly different from the actual. the lists *should* be in the same order however
			//and the prng should have the same seed and be the same. 
			WorkingCombatants = new HashSet<ICombatant>();
			CombatNodes = new HashSet<CombatNode>();
			foreach (var shipObj in StartCombatants)
			{
				var ship = shipObj.Copy();
				if (ship.Owner != shipObj.Owner)
					ship.Owner.Dispose(); // don't need extra empires!

				// copy over the components individually so they can take damage without affecting the starting state
				// TODO - deal with planets in combat
				((SpaceVehicle)ship).Components.Clear();
				foreach (var comp in ((SpaceVehicle)shipObj).Components)
					((SpaceVehicle)ship).Components.Add(comp.Copy());

				WorkingCombatants.Add(ship);
				CombatObject comObj;
				if (ship is SpaceVehicle)
				{
					SpaceVehicle sobj = (SpaceVehicle)ship;
					sobj.Owner = shipObj.Owner;
					comObj = new CombatObject(sobj, battleseed);
				}
				else
					comObj = new CombatObject(ship, battleseed); // for unit tests
				CombatNodes.Add(comObj);
				Empires[ship.Owner].ownships.Add(comObj);

			}
		}

		public void SetUpPieces()
		{
			int startrange = 1500; //TODO check longest range weapon. startrange should be half this.
			Point3d[] startpoints = new Point3d[EmpiresArray.Count()];

			Compass angle = new Compass(360 / EmpiresArray.Count(), false);

			for (int i = 0; i <= EmpiresArray.Count() - 1; i++)
			{
				double angleoffset = angle.Radians * i;
				startpoints[i] = new Point3d(Trig.sides_ab(startrange, angleoffset));

			}

			if (!IsReplay)
				FirstSetup();
			else
				ReplaySetup();
			//setup the game peices
			foreach (CombatObject comObj in CombatObjects)
			{
				foreach (KeyValuePair<Empire, CombatEmpire> empire in Empires)
				{
					var ship = comObj.icomobj;
					if (ship.IsHostileTo(empire.Key))
						empire.Value.hostile.Add(comObj);
					else if (ship.Owner != empire.Key)
						empire.Value.friendly.Add(comObj);
				}

				int empindex = EmpiresArray.IndexOf(comObj.icomobj.Owner);
				comObj.cmbt_loc = new Point3d(startpoints[empindex]); //todo add offeset from this for each ship put in a formation (atm this is just all ships in one position) ie + point3d(x,y,z)
				//thiscomobj.cmbt_face = new Point3d(0, 0, 0); // todo have the ships face the other fleet if persuing or towards the sector they were heading if not persuing. 
				comObj.cmbt_head = new Compass(comObj.cmbt_loc, new Point3d(0, 0, 0));
				comObj.cmbt_att = new Compass(0);
				int speed = 0;
				if (comObj.icomobj is Vehicle)
					speed = ((Vehicle)comObj.icomobj).Speed / 2;
				//thiscomobj.cmbt_vel = Trig.sides_ab(speed, (Trig.angleto(thiscomobj.cmbt_loc, thiscomobj.cmbt_face)));
				comObj.cmbt_vel = Trig.sides_ab(speed, comObj.cmbt_head.Radians);

				comObj.newDice(battleseed);

			}
			foreach (CombatObject comObj in CombatObjects)
				commandAI(comObj, 1);
		}

		public void Start()
		{
			//start combat
			Current.Add(this);

			SetUpPieces();
		}

		public void End()
		{
			//end combat
			Current.Remove(this);
			Previous.Add(this);
		}

		/// <summary>
		/// Processes a tick of combat
		/// </summary>
		/// <param name="tick">The tick number</param>
		/// <param name="cmdfreqCounter">Counter to keep track of when the ship AI can issue comamnds.</param>
		/// <returns>True if the battle should continue; false if it should end.</returns>
		public bool ProcessTick(ref int tick, ref int cmdfreqCounter)
		{
			//unleash the dogs of war!
			foreach (CombatObject comObj in CombatObjects)
			{
				comObj.debuginfo = ""; //cleardebuginfo txt.

				//heading and thrust
				helm(comObj);

				//fire ready weapons.
				firecontrol(tick, comObj);

				//physicsmove objects.
				SimNewtonianPhysics(comObj);
			}
			if (cmdfreqCounter >= Battle_Space.CommandFrequency)
			{
				foreach (CombatObject comObj in CombatObjects)
				{
					commandAI(comObj, tick);
				}
				cmdfreqCounter = 0;
			}

			bool ships_persuing = true; // TODO - check if ships are actually pursuing
			bool ships_inrange = true; //ships are in skipdrive interdiction range of enemy ships TODO - check if ships are in range
			bool hostiles = CombatObjects.Any(o => !o.icomobj.IsDestroyed && CombatObjects.Any(o2 => !o2.icomobj.IsDestroyed && o.icomobj.IsHostileTo(o2.icomobj.Owner)));

			bool cont;
			if (!ships_persuing && !ships_inrange)
				cont = false;
			else if (!hostiles)
				cont = false;
			else if (tick > 10000) // TODO - put max battle tick in Settings.txt or something
				cont = false;
			else
				cont = true;
			cmdfreqCounter++;
			tick++;
			return cont;
		}

		public void Resolve()
		{
			Start();


			int tick = 1, cmdFreqCounter = 0;
			while (ProcessTick(ref tick, ref cmdFreqCounter))
			{
				// keep on truckin'
			}

			End();
		}


		/// <summary>
		/// endgoal for helm is for the  ship to get to and match speed with the comObj.targetWaypiont as fast as possible.
		/// the strategic AI should be responsible for setting where the waypoint is and where thet waypoint is going. 
		/// </summary>
		/// <param name="comObj"></param>
		public void helm(CombatObject comObj)
		{
			comObj.debuginfo += "HelmInfo:" + "\r\n";
			var ship = comObj.icomobj;
			string name = ship.Name;
			//rotate ship
			double timetoturn = 0;
			//Compass angletoturn = new Compass(Trig.angleto(comObj.cmbt_face, comObj.waypointTarget.cmbt_loc));
			combatWaypoint wpt = comObj.waypointTarget;
			Compass angletoWaypoint = new Compass(comObj.cmbt_loc, comObj.waypointTarget.cmbt_loc); //relitive to me. 
			Compass angletoturn = new Compass(angletoWaypoint.Radians - comObj.cmbt_head.Radians);
			Point3d vectortowaypoint = comObj.cmbt_loc - comObj.waypointTarget.cmbt_loc;
			//if (comObj.lastVectortoWaypoint != null)
			//    angletoturn.Radians = Trig.angleA(vectortowaypoint - comObj.lastVectortoWaypoint);

			timetoturn = angletoturn.Radians / comObj.maxRotate;
			double oneEightytime = 3.14159265 / comObj.maxRotate;
			//Point3d offsetVector = comObj.waypointTarget.cmbt_vel - comObj.cmbt_vel; // O = a - b
			//Point3d combinedVelocity = comObj.cmbt_vel - comObj.waypointTarget.cmbt_vel;
			//Point3d distancePnt = comObj.waypointTarget.cmbt_loc - comObj.cmbt_loc;
			//double closingSpeed = Trig.dotProduct(combinedVelocity, distancePnt);
			double closingSpeed = GravMath.closingrate(comObj.cmbt_loc, comObj.cmbt_vel, comObj.waypointTarget.cmbt_loc, comObj.waypointTarget.cmbt_vel);

			double myspeed = Trig.hypotinuse(comObj.cmbt_vel);

			double timetokill_ClosingSpeed = closingSpeed / (comObj.maxfowardThrust / comObj.cmbt_mass); //t = v / a
			double strafetimetokill_ClosingSpeed = closingSpeed / (comObj.maxStrafeThrust / comObj.cmbt_mass);
			double timetokill_MySpeed = myspeed / (comObj.maxfowardThrust / comObj.cmbt_mass);


			double distance = Trig.distance(comObj.waypointTarget.cmbt_loc, comObj.cmbt_loc);


			double nominaldistance = comObj.maxStrafeThrust;
			double timetowpt = distance / closingSpeed;

			bool? thrustToWaypoint = true;
			string helmdo = "";

			if (closingSpeed > 0) //getting closer
			{
				if (distance <= nominaldistance)  //close to the waypoint.
				{
					thrustToWaypoint = null;//should attempt to match speed
				}
				if (timetowpt <= timetokill_ClosingSpeed + oneEightytime)//if/when we're going to overshoot teh waypoint.
				{
					if (timetowpt < strafetimetokill_ClosingSpeed) //if time to waypoint is less than time to kill speed with strafe thrusters
					{

						thrustToWaypoint = false;
					}
					else
					{ //use strafe thrust to get close to the waypoint. 

						helmdo = "null" + "\r\n";
						thrustToWaypoint = null; //else match speed and use thrusters to get closer
					}
				}
			}
			else
			{
			}

			if (thrustToWaypoint == false)
			{
				helmdo = "Initiating Turnaround" + "\r\n"; //turn around and thrust the other way
				angletoturn.Degrees = (angletoWaypoint.Degrees - 180) - comObj.cmbt_head.Degrees; //turn around and thrust the other way
				angletoturn.normalize();
			}
			else if (thrustToWaypoint == null)
			{
				angletoturn.Radians = Trig.angleA(comObj.waypointTarget.cmbt_vel);
			}

			comObj.debuginfo += "timetowpt:\t" + timetowpt.ToString() + "\r\n";
			comObj.debuginfo += "strafetime:\t" + strafetimetokill_ClosingSpeed.ToString() + "\r\n";
			comObj.debuginfo += "speedkilltime:\t" + timetokill_ClosingSpeed.ToString() + "\r\n";
			comObj.debuginfo += "180time:\t" + oneEightytime.ToString() + "\r\n";
			comObj.debuginfo += "ThrustTo:\t" + thrustToWaypoint.ToString() + "\r\n";
			comObj.debuginfo += helmdo + "\r\n";



			turnship(comObj, angletoturn, angletoWaypoint);

			thrustship(comObj, angletoturn, thrustToWaypoint);

			comObj.lastVectortoWaypoint = vectortowaypoint;

		}

		private void turnship(CombatObject comObj, Compass angletoturn, Compass angleToTarget)
		{
			if (angletoturn.Degrees <= 180) //turn clockwise
			{
				if (angletoturn.Radians > comObj.maxRotate)
				{
					//comObj.cmbt_face += comObj.Rotate;
					comObj.cmbt_head.Radians += comObj.maxRotate;
				}
				else
				{
					//comObj.cmbt_face = comObj.waypointTarget.cmbt_loc;
					comObj.cmbt_head.Degrees += angletoturn.Degrees;
				}
			}
			else //turn counterclockwise
			{
				if ((360 - angletoturn.Radians) > comObj.maxRotate)
				{
					//comObj.cmbt_face -= comObj.maxRotate;
					comObj.cmbt_head.Radians -= comObj.maxRotate;
				}
				else
				{
					//comObj.cmbt_face = comObj.waypointTarget.cmbt_loc;
					// subtract 360 minus the angle
					comObj.cmbt_head.Degrees += angletoturn.Degrees;
				}
			}
		}

		private void strafeship(CombatObject comObj, bool? thrustToWaypoint)
		{
			//thrust ship using strafe
			if (thrustToWaypoint == true) //(if we want to accelerate towards the target, not away from it)
			{
				comObj.cmbt_thrust = Trig.intermediatePoint(comObj.cmbt_loc, comObj.waypointTarget.cmbt_loc, comObj.maxStrafeThrust);
			}
			else if (thrustToWaypoint == false)
			{
				comObj.cmbt_thrust = Trig.intermediatePoint(comObj.cmbt_loc, comObj.waypointTarget.cmbt_loc, -comObj.maxStrafeThrust);
			}
			else //if null
			{
				//comObj.cmbt_thrust = Trig.
			}
		}

		private void thrustship(CombatObject comObj, Compass angletoturn, bool? thrustToWaypoint)
		{
			comObj.cmbt_thrust.ZEROIZE();
			strafeship(comObj, thrustToWaypoint);
			//main foward thrust - still needs some work, ie it doesnt know when to turn it off when close to a waypoint.
			double thrustby = 0;
			if (thrustToWaypoint != null)
			{
				if (angletoturn.Degrees >= 0 && angletoturn.Degrees < 90)
				{

					thrustby = (double)comObj.maxfowardThrust / (Math.Max(1, angletoturn.Degrees / 0.9));
				}
				else if (angletoturn.Degrees > 270 && angletoturn.Degrees < 360)
				{
					Compass angle = new Compass(360 - angletoturn.Degrees);
					angle.normalize();
					thrustby = (double)comObj.maxfowardThrust / (Math.Max(1, angle.Degrees / 0.9));
				}

				//Point3d fowardthrust = new Point3d(comObj.cmbt_face + thrustby);
				Point3d fowardthrust = new Point3d(Trig.sides_ab(thrustby, comObj.cmbt_head.Radians));
				comObj.cmbt_thrust += fowardthrust;
			}
			else
			{
				//match velocity with waypoint
				Point3d wayptvel = comObj.waypointTarget.cmbt_vel;
				Point3d ourvel = comObj.cmbt_vel;

				thrustby = (double)comObj.maxfowardThrust / (Math.Max(1, angletoturn.Degrees / 0.9));

				Point3d fowardthrust = new Point3d(Trig.intermediatePoint(ourvel, wayptvel, thrustby));

			}

		}

		public void firecontrol(int tic_countr, CombatObject comObj)
		{
			foreach (var weapon in comObj.weaponList)
			{
				Vehicle ship = (Vehicle)comObj.icomobj;
				//ship.Weapons
				CombatWeapon wpn = (CombatWeapon)weapon;



				if (comObj.weaponTarget.Count() > 0 &&
					wpn.CanTarget(comObj.weaponTarget[0].icomobj) &&
					tic_countr >= wpn.nextReload)
				{
					//wpn.Attack(comObj.weaponTarget[0].icomobj, (int)Trig.distance(comObj.cmbt_loc, comObj.weaponTarget[0].cmbt_loc), this);
					//combatEvent evnt = new combatEvent(tic_countr, "", comObj, comObj.weaponTarget[0]);
					//replaylog.addEvent(tic_countr, evnt);
					//MountedComponentTemplate tmplt = wpn.Template;
					//int maxRange = tmplt.WeaponMaxRange * 100;
					//int minRange = tmplt.WeaponMinRange * 100;
					//int accuracy = tmplt.WeaponAccuracy;
					//int damage = tmplt.WeaponDamage;
					if (isinRange(comObj, wpn, comObj.weaponTarget[0]))
					{
						//this function figures out if there's a hit, deals the damage, and creates an event.

						//first create the event for the target ship
						CombatTakeFireEvent targets_event = FireWeapon(tic_countr, comObj, wpn, comObj.weaponTarget[0]);
						//then create teh event for this ship firing on the target
						CombatFireOnTargetEvent attack_event = new CombatFireOnTargetEvent(tic_countr, comObj, comObj.cmbt_loc, weapon, targets_event);
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
				foreach (CombatEvent comevnt in ReplayLog.EventsForObjectAtTick(comObj, tic_countr))
				{
					if (comevnt.GetType() == typeof(CombatTakeFireEvent))
					{
						CombatTakeFireEvent takefire = (CombatTakeFireEvent)comevnt;
						takefire.setLocation(comObj.cmbt_loc);
					}
				}
			}
		}

		public Point3d SimNewtonianPhysics(CombatNode comObj)
		{
			if (comObj is CombatObject)
			{
				CombatObject comObjo = (CombatObject)comObj;
				comObjo.cmbt_accel = (GravMath.accelVector(comObjo.cmbt_mass, comObjo.cmbt_thrust));
				comObj.cmbt_vel += comObjo.cmbt_accel;
			}

			comObj.cmbt_loc += comObj.cmbt_vel * ticlen;

			return comObj.cmbt_loc;
		}

		public Point3d InterpolatePosition(CombatNode comObj, double fractionalTick)
		{
			return comObj.cmbt_loc + comObj.cmbt_vel * fractionalTick;
		}

		public void commandAI(CombatObject comObj, int tick)
		{
			//do AI decision stuff.
			//pick a primary target to persue, use AI script from somewhere.  this could also be a formate point. and could be a vector rather than a static point. 
            string comAI = "";
			CombatObject tgtObj;
			if (Empires[comObj.icomobj.Owner].hostile.Any())
			{
				tgtObj = Empires[comObj.icomobj.Owner].hostile[0];
				combatWaypoint wpt = new combatWaypoint(tgtObj);
				comObj.waypointTarget = wpt;
				//pick a primary target to fire apon from a list of enemy within weapon range
				comObj.weaponTarget = new List<CombatObject>();
				comObj.weaponTarget.Add(Empires[comObj.icomobj.Owner].hostile[0]);
			}
            if (IsReplay && tick < 1000)
            {
                List<CombatEvent> evnts = ReplayLog.EventsForObjectAtTick(comObj, tick).ToList<CombatEvent>();                
                CombatLocationEvent locevnt = (CombatLocationEvent)evnts.Where(e => e.GetType() is CombatLocationEvent);
                comAI = "Location ";
                if (locevnt.Location == comObj.cmbt_loc)
                    comAI += "Does match \r\n";
                else
                    comAI += "Not matched \r\n";
            }
            else if (!IsReplay && tick < 1000)
            {
                CombatLocationEvent locevnt = new CombatLocationEvent(tick, comObj, comObj.cmbt_loc);
                ReplayLog.Events.Add(locevnt);
            }
             
            comObj.debuginfo += comAI;
		}



		private bool isinRange(CombatObject attacker, CombatWeapon weapon, CombatObject target)
		{
			bool inrange = false;
			var wpninfo = weapon.weapon.Template.ComponentTemplate.WeaponInfo;
			double distance_toTarget = Trig.distance(attacker.cmbt_loc, target.cmbt_loc);
			int weaponMaxRange = weapon.maxRange;
			int weaponMinRange = weapon.minRange;
			string weaponRangeinfo = "RangeInfo:\r\n ";


			if (weapon.weaponType == "Beam")          //beam
			{
				if (distance_toTarget <= weaponMaxRange && distance_toTarget >= weaponMinRange)
				{
					inrange = true;
					weaponRangeinfo += "Range for Beam is good \r\n";
				}
			}
			else if (weapon.weaponType == "Bolt") //projectile
			{
				double boltTTT = boltTimeToTarget(attacker, weapon, target);
				//remember, maxRange is bolt lifetime. 
				if (boltTTT <= weaponMaxRange && boltTTT >= weaponMinRange)
				{
					inrange = true;
					weaponRangeinfo += "Range for Projectile is good \r\n";
				}
			}
			else if (wpninfo.DisplayEffect.GetType() == typeof(Combat.SeekerWeaponDisplayEffect))       //seeker
			{
				if (distance_toTarget <= weaponMaxRange && distance_toTarget >= weaponMinRange)
					inrange = true;
			}

			attacker.debuginfo += weaponRangeinfo;
			return inrange;
		}

		public static double boltClosingSpeed(CombatObject attacker, CombatWeapon weapon, CombatObject target)
		{
			double shotspeed = weapon.boltSpeed; //speed of bullet when ship is at standstill
			double shotspeed_actual = shotspeed + GravMath.closingrate(attacker.cmbt_loc, attacker.cmbt_vel, target.cmbt_loc, target.cmbt_vel);
			return shotspeed_actual * ticlen;
		}

		public static double boltTimeToTarget(CombatObject attacker, CombatWeapon weapon, CombatObject target)
		{
			double distance_toTarget = Trig.distance(attacker.cmbt_loc, target.cmbt_loc);
			double boltTimetoTarget = distance_toTarget / boltClosingSpeed(attacker, weapon, target);
			return boltTimetoTarget;
		}

		private CombatTakeFireEvent FireWeapon(int tic, CombatObject attacker, CombatWeapon weapon, CombatObject target)
		{


			var wpninfo = weapon.weapon.Template.ComponentTemplate.WeaponInfo;
			double rangeForDamageCalcs = 0;
			double rangetotarget = Trig.distance(attacker.cmbt_loc, target.cmbt_loc);
			int targettic = tic;

			//reset the weapon nextReload.
			weapon.nextReload = tic + (int)(weapon.reloadRate * 10);



			ICombatant target_icomobj = target.icomobj;
			//Vehicle defenderV = (Vehicle)target_icomobj;

			if (!weapon.CanTarget(target_icomobj))
				return null;

			// TODO - check range too
			var tohit = Mod.Current.Settings.WeaponAccuracyPointBlank + weapon.weapon.Template.WeaponAccuracy + weapon.weapon.Container.Accuracy - target_icomobj.Evasion;
			// TODO - moddable min/max hit chances with per-weapon overrides
			if (tohit > 99)
				tohit = 99;
			if (tohit < 1)
				tohit = 1;

			//bool hit = RandomHelper.Range(0, 99) < tohit;
			PRNG dice = attacker.getDice();
			bool hit = dice.Range(0, 99) < tohit;

			CombatTakeFireEvent target_event = null;
			//for bolt calc, need again for adding to list.
			if (weapon.weaponType == "Bolt")
			{
				double boltTTT = boltTimeToTarget(attacker, weapon, target);
				double boltSpeed = boltClosingSpeed(attacker, weapon, target);
				double rThis_distance = boltSpeed * boltTTT;

				double rMax_distance = boltSpeed * weapon.maxRange; //s * t = d
				double rMin_distance = boltSpeed * weapon.minRange; //s * t = d

				double rMax_distance_standstill = weapon.boltSpeed * weapon.maxRange;
				double rMin_distance_standstill = weapon.boltSpeed * weapon.minRange;

				double scaler = rMax_distance_standstill / rMax_distance;

				rangeForDamageCalcs = rThis_distance * scaler * 0.001;

				//set target tick for the future.
				targettic += (int)boltTTT;
				if (IsReplay)
				{
					//read the event
					target_event = ReplayLog.EventsForObjectAtTick(target, targettic).OfType<CombatTakeFireEvent>().ToList<CombatTakeFireEvent>()[0];
				}
				else
				{
					//*write* the event
					target_event = new CombatTakeFireEvent(targettic, target, target.cmbt_loc, hit);
				}

			}
			else
			{
				if (IsReplay)
				{ //read the replay... nothing to do if a beam. 
				}
				else
				{ //write the event.
					rangeForDamageCalcs = rangetotarget / 1000;
					target_event = new CombatTakeFireEvent(targettic, target, target.cmbt_loc, hit);
				}
			}

			rangeForDamageCalcs = Math.Max(1, rangeForDamageCalcs); //don't be less than 1.

			long ID = target_icomobj.ID;
			if (ID == -1)
			{

			}

			if (hit && !target_icomobj.IsDestroyed)
			{
				var shot = new Combat.Shot(weapon.weapon, target_icomobj, (int)rangeForDamageCalcs);
				//defender.TakeDamage(weapon.Template.ComponentTemplate.WeaponInfo.DamageType, shot.Damage, battle);
				int damage = shot.Damage;
				combatDamage(target, weapon, damage, attacker.getDice());
				if (target_icomobj.MaxNormalShields < target_icomobj.NormalShields)
					target_icomobj.NormalShields = target_icomobj.MaxNormalShields;
				if (target_icomobj.MaxPhasedShields < target_icomobj.PhasedShields)
					target_icomobj.PhasedShields = target_icomobj.MaxPhasedShields;
				//if (defender.IsDestroyed)
				//battle.LogTargetDeath(defender);
			}
			return target_event;
		}

		private void combatDamage(CombatObject target, CombatWeapon weapon, int damage, PRNG attackersdice)
		{

			Combat.DamageType damageType = weapon.weapon.Template.ComponentTemplate.WeaponInfo.DamageType;
			Vehicle targetV = (Vehicle)target.icomobj;
			if (targetV.IsDestroyed)
				return; //damage; // she canna take any more!

			// TODO - worry about damage types, and make sure we have components that are not immune to the damage type so we don't get stuck in an infinite loop
			int shieldDmg = 0;
			if (targetV.NormalShields > 0)
			{
				var dmg = Math.Min(damage, targetV.NormalShields);
				targetV.NormalShields -= dmg;
				damage -= dmg;
				shieldDmg += dmg;
			}
			if (targetV.PhasedShields > 0)
			{
				var dmg = Math.Min(damage, targetV.PhasedShields);
				targetV.NormalShields -= dmg;
				damage -= dmg;
				shieldDmg += dmg;
			}
			if (shieldDmg > 0)// && battle != null)
			{
				//battle.LogShieldDamage(this, shieldDmg);
			}
			while (damage > 0 && !targetV.IsDestroyed)
			{
				var comps = targetV.Components.Where(c => c.Hitpoints > 0);
				var armor = comps.Where(c => c.HasAbility("Armor"));
				var internals = comps.Where(c => !c.HasAbility("Armor"));
				var canBeHit = armor.Any() ? armor : internals;
				var comp = canBeHit.ToDictionary(c => c, c => c.HitChance).PickWeighted(attackersdice);

				//comp.
				//SpaceVehicle actualShip =

				damage = comp.TakeDamage(damageType, damage, null);// battle);
			}

			if (targetV.IsDestroyed)
			{
				//battle.LogTargetDeath(this);
				targetV.Dispose();
				foreach (KeyValuePair<Empire, CombatEmpire> empireKVP in Empires)
				{
					CombatEmpire empire = empireKVP.Value;
					if (empire.ownships.Contains(target))
						empire.ownships.Remove(target);
					else if (empire.hostile.Contains(target))
						empire.hostile.Remove(target);
					else if (empire.friendly.Contains(target))
						empire.friendly.Remove(target);
				}
			}

			// update memory sight
			targetV.UpdateEmpireMemories();
		}

		public System.Drawing.Image Icon
		{
			get { return WorkingCombatants.OfType<ISpaceObject>().Largest().Icon; }
		}

		public System.Drawing.Image Portrait
		{
			get { return WorkingCombatants.OfType<ISpaceObject>().Largest().Portrait; }
		}

		public ICombatant FindStartCombatant(ICombatant c)
		{
			return StartCombatants.SingleOrDefault(c2 => c2.ID == c.ID);
		}

		public ICombatant FindWorkingCombatant(ICombatant c)
		{
			return WorkingCombatants.SingleOrDefault(c2 => c2.ID == c.ID);
		}

		public ICombatant FindActualCombatant(ICombatant c)
		{
			return ActualCombatants.SingleOrDefault(c2 => c2.ID == c.ID);
		}
	}
}
